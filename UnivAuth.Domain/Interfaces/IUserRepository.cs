using UnivAuth.Domain.Entities;

namespace UnivAuth.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> LoginAsync(string usuario, string pwd);
        Task<User?> Login2faAsync(string usuario, string secreto);
    }
}
