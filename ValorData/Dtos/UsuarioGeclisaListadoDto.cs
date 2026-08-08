namespace ValorModels.Dtos
{
    // Listado liviano de la tabla Usuarios de GECLISA, para el picker de "vincular usuario Natanet
    // con su Usuario_id de GECLISA" al crear/editar un medico en BH_USERS.
    public class UsuarioGeclisaListadoDto
    {
        public int Usuario_id { get; set; }
        public string? Usuario_nom { get; set; }
        public string? NombreCompleto { get; set; }
    }
}
