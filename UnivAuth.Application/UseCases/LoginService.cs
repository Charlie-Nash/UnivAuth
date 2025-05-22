using UnivAuth.Application.DTOs;
using UnivAuth.Domain.Entities;
using UnivAuth.Domain.Interfaces;

namespace UnivAuth.Application.UseCases
{
    public class LoginService
    {
        private readonly IUserRepository _userRepository;

        public LoginService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User?> LoginAsync(LoginRequest request)
        {
            return await _userRepository.LoginAsync(request.Usuario, request.Pwd);
        }

        public async Task<User?> Login2faAsync(TFaRequest request)
        {
            return await _userRepository.Login2faAsync(request.Usuario, request.Secreto);
        }
    }
}
