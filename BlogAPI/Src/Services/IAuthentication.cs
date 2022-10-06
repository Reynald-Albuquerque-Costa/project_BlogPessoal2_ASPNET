using BlogAPI.Src.Models;
using System.Threading.Tasks;

namespace BlogAPI.Src.Services
{
    public interface IAuthentication
    {
        string CodificarSenha(string password);
        Task CriarUsuarioSemDuplicarAsync(User user);
        string GerarToken(User user);

    }
}
