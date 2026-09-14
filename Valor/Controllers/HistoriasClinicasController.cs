using Microsoft.AspNetCore.Mvc;
using ValoresData.Services.ServicesInterfaces;
using ValorModels.Dtos.HistoriasClinicasDto;

namespace Valor.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistoriasClinicasController : ControllerBase
    {
        private readonly IHistoriasClinicasService _historiasClinicasService;

        public HistoriasClinicasController(IHistoriasClinicasService historiasClinicasService)
        {
            _historiasClinicasService = historiasClinicasService;
        }

        // Fase 1 de la traza: carga inicial de la HC (header + listado), 3 SPs en paralelo.
        [HttpGet("CargaInicial")]
        public async Task<ActionResult<CargaInicialHistoriaClinicaDto>> GetCargaInicial(
            [FromQuery] int fichaId,
            [FromQuery] DateTime? fechaInicio = null,
            [FromQuery] DateTime? fechaFin = null,
            [FromQuery] string? tipoHc = null,
            [FromQuery] string? tevCod = null,
            [FromQuery] int? preId = null,
            [FromQuery] int? panId = null,
            [FromQuery] int? usuarioIdQueConsulta = null)
        {
            var result = await _historiasClinicasService.GetCargaInicialAsync(
                fichaId, fechaInicio, fechaFin, tipoHc, tevCod, preId, panId, usuarioIdQueConsulta);

            return Ok(result);
        }

        // Detalle de la nota/evolucion seleccionada (BuscarPorID + FirmaDigital + BuscarPanId).
        [HttpGet("{hcId}/Detalle")]
        public async Task<ActionResult<NotaSeleccionadaDetalleDto>> GetDetalleNota(int hcId)
        {
            var result = await _historiasClinicasService.GetDetalleNotaAsync(hcId);

            if (result.Detalle == null)
            {
                return NotFound("Historia clinica no encontrada");
            }

            return Ok(result);
        }

        // Info del usuario logueado en GECLISA (firma, permisos). No depende de ninguna HC
        // puntual: el frontend deberia cachear esto por usuarioId en vez de re-pedirlo por cada click.
        [HttpGet("UsuarioLogueado/{usuarioId}")]
        public async Task<ActionResult<UsuarioGeclisaDto>> GetUsuarioLogueado(int usuarioId)
        {
            var result = await _historiasClinicasService.GetUsuarioLogueadoAsync(usuarioId);

            if (result == null)
            {
                return NotFound("Usuario no encontrado");
            }

            return Ok(result);
        }

        // Epic 4: alta de nueva evolucion clinica. Flujo secuencial (validar internacion ->
        // insertar -> loguear), sin paralelismo - el orden importa.
        [HttpPost("Evolucion")]
        public async Task<ActionResult<AltaEvolucionResponseDto>> PostEvolucion([FromBody] AltaEvolucionRequestDto request)
        {
            if (string.IsNullOrWhiteSpace(request.Texto))
            {
                return BadRequest("El texto de la evolucion es obligatorio");
            }

            var result = await _historiasClinicasService.InsertarEvolucionAsync(request);

            return Ok(result);
        }

        // Epic 5: tab Estudios. En la traza el rango por defecto es de 6 anios (2020->2026,
        // 24ms) - no es un N+1, pero si esta pantalla se usa seguido vale la pena preguntar
        // si el rango por defecto deberia acortarse (decision de producto, no de backend).
        [HttpGet("Estudios")]
        public async Task<ActionResult<IEnumerable<EstudioDto>>> GetEstudios(
            [FromQuery] int fichaId,
            [FromQuery] DateTime fechaDesde,
            [FromQuery] DateTime fechaHasta,
            [FromQuery] string meArea = "D")
        {
            var result = await _historiasClinicasService.GetEstudiosAsync(fichaId, fechaDesde, fechaHasta, meArea);

            return Ok(result);
        }

        // Resuelve el fichaId de GECLISA a partir del DNI, consultando dbo.Ficha directamente
        // (fuente de verdad), para pantallas que solo tienen el documento del paciente (ej. la
        // agenda de turnos de /rp/turnos).
        [HttpGet("FichaPorDni/{dni}")]
        public async Task<ActionResult<int>> GetFichaIdPorDni(string dni)
        {
            var fichaId = await _historiasClinicasService.GetFichaIdPorDniAsync(dni);

            if (fichaId == null)
            {
                return NotFound("No se encontro ficha para el DNI indicado");
            }

            return Ok(fichaId);
        }
    }
}
