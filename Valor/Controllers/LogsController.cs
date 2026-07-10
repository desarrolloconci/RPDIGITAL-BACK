using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Dtos;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogsController : ControllerBase
    {
        private readonly ILogsService _logsService;

        public LogsController(ILogsService logsService)
        {
            _logsService = logsService;
        }

        // GET api/Logs/files
        [HttpGet("files")]
        public ActionResult<IEnumerable<LogFileInfoDto>> GetFiles()
        {
            return Ok(_logsService.GetLogFiles());
        }

        // GET api/Logs?date=2026-07-06&level=WRN&search=SolPract&page=1&pageSize=200
        [HttpGet]
        public ActionResult<LogQueryResultDto> GetEntries(
            [FromQuery] DateOnly? date,
            [FromQuery] string? level,
            [FromQuery] string? search,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 200)
        {
            return Ok(_logsService.GetLogEntries(date, level, search, page, pageSize));
        }

        // GET api/Logs/stats?date=2026-07-06
        [HttpGet("stats")]
        public ActionResult<IEnumerable<LogEndpointStatDto>> GetStats([FromQuery] DateOnly? date)
        {
            return Ok(_logsService.GetEndpointStats(date));
        }
    }
}
