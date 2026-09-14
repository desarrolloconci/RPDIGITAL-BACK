namespace ValorModels.Dtos.HistoriasClinicasDto
{
    // Request de alta de una nueva evolucion clinica (Epic 4). Fecha/Hora quedan opcionales:
    // si no vienen, se usa la hora del servidor (igual que panet_FechaServer en la traza
    // original, para no depender del reloj del cliente).
    public class AltaEvolucionRequestDto
    {
        public int FichaId { get; set; }
        public int PreId { get; set; }
        public string Texto { get; set; } = string.Empty;
        public string UsuAlta { get; set; } = string.Empty;
        public int? MeId { get; set; }
        public int? EspId { get; set; }
        public int? NpId { get; set; }
        public DateTime? Fecha { get; set; }
        public int? Hora { get; set; } // formato HHMM empaquetado (ej. 1057 = 10:57), igual que Hc_Hs
    }
}
