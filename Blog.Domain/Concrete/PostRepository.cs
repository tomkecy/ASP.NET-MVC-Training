using System;
using System.Collections.Generic;
using System.Data.Entity;
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
        /// <returns>Removed post</returns>
        public Post Delete(int id)
        {
            Post dbEntry = dbContext.Posts.Find(id);
            if (dbEntry != null)
            {
                dbContext.PostComments.RemoveRange(dbEntry.PostComments);
                dbContext.Posts.Remove(dbEntry);
            }
            dbContext.SaveChanges();
            return dbEntry;
        }//END of Delete method

        /// <summary>
        /// Removes post from the repository
        /// </summary>
        /// <param name="entity">Post to be removed</param>
        /// <returns>Removed post</returns>
        public Post Delete(Post entity)
        {
            return Delete(entity.Id);
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
        /// <returns>Returns post with passed id or null if post with given id don't exist in repository</returns>
        public Post GetById(int id)
        {
            return dbContext.Posts.Include(m => m.PostComments).SingleOrDefault(post => post.Id == id);
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
                dbContext.Posts.Attach(entity);
                var dbEntry = dbContext.Entry(entity);
                dbEntry.State = EntityState.Modified;
                if (entity.PostComments != null)
                {
                    foreach (var postComment in entity.PostComments)
                    {
                        dbContext.PostComments.Attach(postComment);
                        var entry = dbContext.Entry(postComment);
                        entry.State = postComment.Id == 0 ? EntityState.Added :
                            EntityState.Modified;
                    }
                }
            }
            dbContext.SaveChanges();
        }//END of Update method
    }// END of class PostRepository
}
