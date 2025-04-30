namespace UserService.Dtos.User
{
    public class ResultUserDto
    {
        public bool IsLogin { get; set; }

        public string Token { get; set; }

        public int UserId { get; set; }

        public int TeacherId { get; set; }

        public string Role { get; set; }

    }
}
