namespace ValorModels.Dtos.LoginDto
{
    public class UpdateUserDto
    {
        public string Name { get; set; }
        public string Last_name { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
        public string? Password { get; set; }
        public string? Matricula { get; set; }
    }
}
