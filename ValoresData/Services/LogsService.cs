using Microsoft.AspNetCore.Hosting;
using System.Globalization;
using System.Text.RegularExpressions;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Dtos;

namespace ValoresData.Services
{
    public class LogsService : ILogsService
    {
        private readonly string _logsDirectory;

        private static readonly Regex LineRegex = new(
            @"^(?<ts>\d{4}-\d{2}-\d{2} \d{2}:\d{2}:\d{2}\.\d{3})\s\[(?<level>\w{3})\]\s(?<message>.*)$",
            RegexOptions.Compiled);

        private static readonly Regex RequestRegex = new(
            @"^HTTP (?<method>\S+) (?<path>\S+) respondió (?<status>\d+) en (?<elapsed>[\d.,]+) ms$",
            RegexOptions.Compiled);

        public LogsService(IWebHostEnvironment env)
        {
            _logsDirectory = Path.Combine(env.ContentRootPath, "Logs");
        }

        public IEnumerable<LogFileInfoDto> GetLogFiles()
        {
            if (!Directory.Exists(_logsDirectory))
            {
                return Enumerable.Empty<LogFileInfoDto>();
            }

            return Directory.GetFiles(_logsDirectory, "api-*.txt")
                .Select(f => new FileInfo(f))
                .Select(fi => new LogFileInfoDto
                {
                    FileName = fi.Name,
                    Date = ParseDateFromFileName(fi.Name),
                    SizeBytes = fi.Length
                })
                .OrderByDescending(f => f.Date)
                .ToList();
        }

        public LogQueryResultDto GetLogEntries(DateOnly? date, string? level, string? search, int page, int pageSize)
        {
            page = page < 1 ? 1 : page;
            pageSize = pageSize is < 1 or > 1000 ? 200 : pageSize;

            var filePath = GetFilePathForDate(date);
            if (!File.Exists(filePath))
            {
                return new LogQueryResultDto { Page = page, PageSize = pageSize, TotalCount = 0 };
            }

            IEnumerable<LogEntryDto> entries = ReadEntries(filePath);

            if (!string.IsNullOrWhiteSpace(level))
            {
                entries = entries.Where(e => e.Level.Equals(level, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrWhiteSpace(search))
            {
                entries = entries.Where(e => e.Message.Contains(search, StringComparison.OrdinalIgnoreCase));
            }

            var ordered = entries.OrderByDescending(e => e.Timestamp).ToList();

            return new LogQueryResultDto
            {
                TotalCount = ordered.Count,
                Page = page,
                PageSize = pageSize,
                Entries = ordered.Skip((page - 1) * pageSize).Take(pageSize).ToList()
            };
        }

        public IEnumerable<LogEndpointStatDto> GetEndpointStats(DateOnly? date)
        {
            var filePath = GetFilePathForDate(date);
            if (!File.Exists(filePath))
            {
                return Enumerable.Empty<LogEndpointStatDto>();
            }

            var requests = ReadEntries(filePath)
                .Select(e => RequestRegex.Match(e.Message))
                .Where(m => m.Success)
                .Select(m => new
                {
                    Method = m.Groups["method"].Value,
                    Path = m.Groups["path"].Value,
                    Elapsed = ParseElapsed(m.Groups["elapsed"].Value)
                });

            return requests
                .GroupBy(r => new { r.Method, r.Path })
                .Select(g => new LogEndpointStatDto
                {
                    Method = g.Key.Method,
                    Path = g.Key.Path,
                    Count = g.Count(),
                    AvgMs = Math.Round(g.Average(x => x.Elapsed), 2),
                    MaxMs = Math.Round(g.Max(x => x.Elapsed), 2),
                    MinMs = Math.Round(g.Min(x => x.Elapsed), 2),
                    SlowCount = g.Count(x => x.Elapsed > 3000)
                })
                .OrderByDescending(s => s.AvgMs)
                .ToList();
        }

        private string GetFilePathForDate(DateOnly? date)
        {
            var targetDate = date ?? DateOnly.FromDateTime(DateTime.Now);
            return Path.Combine(_logsDirectory, $"api-{targetDate:yyyyMMdd}.txt");
        }

        private static IEnumerable<LogEntryDto> ReadEntries(string filePath)
        {
            LogEntryDto current = null;

            foreach (var line in File.ReadLines(filePath))
            {
                var match = LineRegex.Match(line);
                if (match.Success)
                {
                    if (current != null)
                    {
                        yield return current;
                    }

                    current = new LogEntryDto
                    {
                        Timestamp = DateTime.ParseExact(match.Groups["ts"].Value, "yyyy-MM-dd HH:mm:ss.fff", CultureInfo.InvariantCulture),
                        Level = match.Groups["level"].Value,
                        Message = match.Groups["message"].Value
                    };
                }
                else if (current != null)
                {
                    // Líneas de stack trace / excepción que Serilog escribe debajo de la línea principal: se agrupan en la misma entrada.
                    current.Message += Environment.NewLine + line;
                }
            }

            if (current != null)
            {
                yield return current;
            }
        }

        private static double ParseElapsed(string raw)
        {
            return double.Parse(raw.Replace(',', '.'), CultureInfo.InvariantCulture);
        }

        private static DateOnly ParseDateFromFileName(string fileName)
        {
            var digits = new string(fileName.Where(char.IsDigit).ToArray());
            return DateOnly.TryParseExact(digits, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var d)
                ? d
                : DateOnly.MinValue;
        }
    }
}
