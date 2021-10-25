namespace Trsys.BackOffice
{
    public class UserDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Nickname { get; set; }
        public string Role { get; set; }
    }
}