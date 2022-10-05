using BlogAPI.Src.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BlogAPI.Src.Repositories
{
    public interface IPost
    {
        Task<List<Post>> PegarTodasPostagensAsync();
        Task<Post> PegarPostagemPeloIdAsync(int id);
        Task NovaPostagemAsync(Post post);
        Task AtualizarPostagemAsync(Post post);
        Task DeletarPostagemAsync(int id);

    }
}
