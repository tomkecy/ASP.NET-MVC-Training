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
        IQueryable<T> GetAll();
        T GetById(int id);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(int id);

    }//end of interface IRepository<T>
}
