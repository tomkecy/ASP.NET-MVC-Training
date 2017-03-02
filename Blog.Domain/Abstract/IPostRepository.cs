using Blog.Domain.Entities;

namespace Blog.Domain.Abstract
{
    /// <summary>
    /// Defines method to manipulate Posts repositories
    /// </summary>
    public interface IPostRepository : IRepository<Post>
    {
    }//interface IPostRepository : IRepository<Post>
}
