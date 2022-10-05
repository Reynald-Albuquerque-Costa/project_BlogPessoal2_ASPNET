using BlogAPI.Src.Context;
using BlogAPI.Src.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Src.Repositories.Implements
{

    public class PostRepository : IPost
    {
        #region Attributes

        private readonly BlogPessoalContext _context;

        #endregion


        #region Constructors
        public PostRepository(BlogPessoalContext context)
        {
            _context = context;
        }

        #endregion


        #region Methods

        public async Task<List<Post>> PegarTodasPostagensAsync()
        {
            return await _context.Posts
                .Include(p => p.Creator)
                .Include(p => p.Theme)
                .ToListAsync();
        }

        public async Task<Post> PegarPostagemPeloIdAsync(int id)
        {
            if (!ExisteId(id)) throw new Exception("Post id not found");

            return await _context.Posts
                .Include(p => p.Creator)
                .Include(p => p.Theme)
                .FirstOrDefaultAsync(p => p.Id == id);
            bool ExisteId(int id)
            {
                var auxiliar = _context.Posts.FirstOrDefault(u => u.Id == id);
                return auxiliar != null;
            }
        }

        public async Task NovaPostagemAsync(Post post)
        {
            if (!ExisteUsuarioId(post.Creator.Id)) throw new Exception("User id not found");

            if (!ExisteTemaId(post.Theme.Id)) throw new Exception("Theme id not found");

            await _context.Posts.AddAsync(
                new Post
                {
                    Title = post.Title,
                    Description = post.Description,
                    Photo = post.Photo,
                    Creator = await _context.Users.FirstOrDefaultAsync(u => u.Id == post.Creator.Id),
                    Theme = await _context.Themes.FirstOrDefaultAsync(t => t.Id == post.Theme.Id)
                });
            await _context.SaveChangesAsync();

            // auxiliary functions
            bool ExisteUsuarioId(int id)
            {
                var auxiliar = _context.Users.FirstOrDefault(u => u.Id == id);
                return auxiliar != null;
            }
            bool ExisteTemaId(int id)
            {
                var auxiliar = _context.Themes.FirstOrDefault(t => t.Id == id);
                return auxiliar != null;
            }
        }

        public async Task AtualizarPostagemAsync(Post post)
        {
            if (!ExisteTemaId(post.Theme.Id)) throw new Exception("Theme id not found");

            var Existingpost = await PegarPostagemPeloIdAsync(post.Id);
                Existingpost.Title = post.Title;
                Existingpost.Description = post.Description;
                Existingpost.Photo = post.Photo;
                Existingpost.Theme = await _context.Themes.FirstOrDefaultAsync(t => t.Id == post.Theme.Id);

            _context.Posts.Update(Existingpost);
            await _context.SaveChangesAsync();

            // auxiliary functions
            bool ExisteTemaId(int id)
            {
                var auxiliar = _context.Themes.FirstOrDefault(t => t.Id == id);
                return auxiliar != null;
            }
        }

        public async Task DeletarPostagemAsync(int id)
        {
            _context.Posts.Remove(await PegarPostagemPeloIdAsync(id));
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
