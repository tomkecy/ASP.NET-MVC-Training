using System.Data.Entity;
using Blog.Domain.Entities;

namespace Blog.Domain.Concrete
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() : base("BlogContext")
        {
        }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostComment> PostComments { get; set; }

    }// END of class ApplicationDbContext
}
