using BlogAPI.Src.Models;
using System.Threading.Tasks;

namespace BlogAPI.Src.Repositories
{
    public interface IUser
    {
        Task<User> PegarUsuarioPeloEmailAsync(string email);
        Task NovoUsuarioAsync(User user);
    }
}
