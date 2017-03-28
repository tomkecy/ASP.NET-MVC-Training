using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Abstract
{
    /// <summary>
    /// Defines methods to manipulate repositories
    /// </summary>
    /// <typeparam name="T">Represents datatype kept in repository</typeparam>
    public interface IRepository<T> where T: class
    {
        /// <summary>
        /// Gets all items from repository.
        /// </summary>
        /// <returns>Returns IQueryable<list type="T"> containing all items in repository</list></returns>
        IQueryable<T> GetAll();
        /// <summary>
        /// Gets item with given id property from repository.
        /// </summary>
        /// <param name="id">Item id</param>
        /// <returns>Item with given id.</returns>
        T GetById(int id);
        /// <summary>
        /// Adds given entity to the repository.
        /// </summary>
        /// <param name="entity">Entity to be added.</param>
        void Add(T entity);
        /// <summary>
        /// Updates given entity in the repository.
        /// </summary>
        /// <param name="entity">Entity to be updated.</param>
        void Update(T entity);
        /// <summary>
        /// Deletes given entity from the repository.
        /// </summary>
        /// <param name="entity">Entity to be deleted.</param>
        /// <returns>Deleted entity.</returns>
        T Delete(T entity);
        /// <summary>
        /// Deletes entity with given id from the repository
        /// </summary>
        /// <param name="id">Entity id.</param>
        /// <returns>Deleted entity.</returns>
        T Delete(int id);

    }//end of interface IRepository<T>
}
