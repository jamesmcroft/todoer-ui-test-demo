namespace ToDoer.API.Features.Authentication.Domain.Login
{
    using MediatR;

    public class LoginRequest : IRequest
    {
        public LoginRequest(LoginRequestDto request)
        {
            Email = request.Email;
            Password = request.Password;
        }

        public string Email { get; set; }

        public string Password { get; set; }
    }
}