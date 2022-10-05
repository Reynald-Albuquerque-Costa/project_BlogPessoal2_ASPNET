using BlogAPI.Src.Context;
using BlogAPI.Src.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BlogAPI.Src.Repositories.Implements
{
    public class ThemeRepository : ITheme
    {
        #region Attributes

        private readonly BlogPessoalContext _context;

        #endregion


        #region Constructors
        public ThemeRepository(BlogPessoalContext context)
        {
            _context = context;
        }

        #endregion


        #region Methods

        public async Task<List<Theme>> PegarTodosTemasAsync()
        {
            return await _context.Themes.ToListAsync();
        }

        public async Task<Theme> PegarTemaPeloIdAsync(int id)
        {
            if (!ExisteId(id)) throw new Exception("Theme id not found");

            return await _context.Themes.FirstOrDefaultAsync(t => t.Id == id);

            // auxiliary functions
            bool ExisteId(int id)
            {
                var auxiliar = _context.Themes.FirstOrDefault(u => u.Id == id);
                return auxiliar != null;
            }
        }

        public async Task NovoTemaAsync(Theme theme)
        {
            await _context.Themes.AddAsync(
                new Theme
                {
                    Description = theme.Description
                });
            await _context.SaveChangesAsync();

        }

        public async Task AtualizarTemaAsync(Theme theme)
        {
            var Existingtheme = await PegarTemaPeloIdAsync(theme.Id);
            Existingtheme.Description = theme.Description;
            _context.Themes.Update(Existingtheme);
            await _context.SaveChangesAsync();

        }

        public async Task DeletarTemaAsync(int id)
        {
            _context.Themes.Remove(await PegarTemaPeloIdAsync(id));
            await _context.SaveChangesAsync();

        }


        #endregion
    }
}
