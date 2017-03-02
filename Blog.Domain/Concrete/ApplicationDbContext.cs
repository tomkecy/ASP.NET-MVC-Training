using System.Data.Entity;
using Blog.Domain.Entities;

namespace Blog.Domain.Concrete
{
    class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<PostComment> PostComments { get; set; }

    }// END of class ApplicationDbContext
}
