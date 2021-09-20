namespace ToDoer.API.Features.Authentication.Domain.Login
{
    public class LoginRequestDto
    {
        public string Email { get; set; }

        public string Password { get; set; }
    }
}