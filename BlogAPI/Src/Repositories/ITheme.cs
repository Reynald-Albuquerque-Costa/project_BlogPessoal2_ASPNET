using BlogAPI.Src.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogAPI.Src.Repositories
{
    public interface ITheme
    {
        Task<List<Theme>> PegarTodosTemasAsync();
        Task<Theme> PegarTemaPeloIdAsync(int id);
        Task NovoTemaAsync(Theme theme);
        Task AtualizarTemaAsync(Theme theme);
        Task DeletarTemaAsync(int id);
    }
}
