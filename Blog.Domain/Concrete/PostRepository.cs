using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blog.Domain.Abstract;
using Blog.Domain.Entities;

namespace Blog.Domain.Concrete
{
    /// <summary>
    /// Implementation of IPostRepository
    /// </summary>
    public class PostRepository : IPostRepository
    {
        #region PrivateFields

        private ApplicationDbContext dbContext;
        #endregion

        #region Constructors
        public PostRepository()
        {
            this.dbContext = new ApplicationDbContext();
        }

        #endregion

        /// <summary>
        /// Adds new Post to the repository
        /// </summary>
        /// <param name="entity">Post to be added</param>
        public void Add(Post entity)
        {
            Update(entity);
        }//END of Add method

        /// <summary>
        /// Removes post from the repository
        /// </summary>
        /// <param name="id">Post id</param>
        public void Delete(int id)
        {
            Post post = GetById(id);
            Delete(post);
            
        }//END of Delete method

        /// <summary>
        /// Removes post from the repository
        /// </summary>
        /// <param name="entity">Post to be removed</param>
        public void Delete(Post entity)
        {
            if (entity != null)
            {
                if (dbContext.Posts.Contains(entity))
                {
                    dbContext.PostComments.RemoveRange(entity.Comments);
                    dbContext.Posts.Remove(entity);
                }
            }
            dbContext.SaveChanges();
        }//END of Delete method

        /// <summary>
        /// Gets all posts form repository
        /// </summary>
        /// <returns>Returns all posts from repository</returns>
        public IQueryable<Post> GetAll()
        {
            return dbContext.Posts.AsQueryable();
        }//END of GetAll method

        /// <summary>
        /// Gets post from repository
        /// </summary>
        /// <param name="id">Post id</param>
        /// <returns>Returns post with passed id</returns>
        public Post GetById(int id)
        {
            return dbContext.Posts.SingleOrDefault(post => post.Id == id);
        }//END of GetById method

        /// <summary>
        /// Updates post in the repository
        /// </summary>
        /// <param name="entity">Post to be updated</param>
        public void Update(Post entity)
        {
            if (entity.Id == 0)
            {
                dbContext.Posts.Add(entity);
                
            }
            else
            {
                Post post = dbContext.Posts.Find(entity.Id);
                if (post != null)
                {
                    post.Content = entity.Content;
                    post.CreationDateTime = entity.CreationDateTime;
                    post.Comments = entity.Comments;
                }
            }
            dbContext.SaveChanges();
        }//END of Update method
    }// END of class PostRepository
}
