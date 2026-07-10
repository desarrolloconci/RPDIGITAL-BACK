namespace ValorModels.Dtos
{
    public class LogFileInfoDto
    {
        public string FileName { get; set; }
        public DateOnly Date { get; set; }
        public long SizeBytes { get; set; }
    }

    public class LogEntryDto
    {
        public DateTime Timestamp { get; set; }
        public string Level { get; set; }
        public string Message { get; set; }
    }

    public class LogQueryResultDto
    {
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public List<LogEntryDto> Entries { get; set; } = new();
    }

    public class LogEndpointStatDto
    {
        public string Method { get; set; }
        public string Path { get; set; }
        public int Count { get; set; }
        public double AvgMs { get; set; }
        public double MaxMs { get; set; }
        public double MinMs { get; set; }
        public int SlowCount { get; set; }
    }
}
