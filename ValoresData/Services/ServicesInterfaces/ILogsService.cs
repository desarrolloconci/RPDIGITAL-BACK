using ValorModels.Dtos;

namespace ValoresData.Services.ServicesInterfaces
{
    public interface ILogsService
    {
        IEnumerable<LogFileInfoDto> GetLogFiles();
        LogQueryResultDto GetLogEntries(DateOnly? date, string? level, string? search, int page, int pageSize);
        IEnumerable<LogEndpointStatDto> GetEndpointStats(DateOnly? date);
    }
}
