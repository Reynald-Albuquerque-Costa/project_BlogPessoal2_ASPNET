using BlogAPI.Src.Models;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Src.Context
{
    public class BlogPessoalContext : DbContext
    {
        #region Attributes
        public DbSet<User> Users { get; set; }
        public DbSet<Theme> Themes { get; set; }
        public DbSet<Post> Posts { get; set; }

        #endregion


        #region Constructors

        public BlogPessoalContext(DbContextOptions<BlogPessoalContext> opt) : base(opt)
        {

        }

        #endregion

    }
}
