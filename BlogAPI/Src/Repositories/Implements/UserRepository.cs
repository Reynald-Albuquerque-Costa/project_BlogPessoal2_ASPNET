using BlogAPI.Src.Context;
using BlogAPI.Src.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace BlogAPI.Src.Repositories.Implements
{
    public class UserRepository : IUser
    {
        #region Attributes

        private readonly BlogPessoalContext _context;

        #endregion


        #region Constructors
        public UserRepository(BlogPessoalContext context)
        {
            _context = context;
        }

        #endregion


        #region Methods
        public async Task<User> PegarUsuarioPeloEmailAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task NovoUsuarioAsync(User user)
        {
            await _context.Users.AddAsync(
                new User
                {
                    Email = user.Email,
                    Name = user.Name,
                    Password = user.Password,
                    Photo = user.Photo
                });
            await _context.SaveChangesAsync();

        }

        #endregion
    }
}
