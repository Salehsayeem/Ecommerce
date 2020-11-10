using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Ecommerce.DataAccess.Repository.IRepository
{
    //Definitons
    public interface IRepository<T> where T:class
    {
        T Get(int id); //Retrieve data by id
        IEnumerable<T> GetAll(
            Expression<Func<T,bool>> filter = null, //For filtering Data
            Func<IQueryable<T>,IOrderedQueryable<T>> orderBy = null, //For Ordering Data
            string includeProperties = null
            ); //There are 3 parameters, It is used to Retrieve all the data by using those parameters
        T GetFirstOrDefault(
            Expression<Func<T, bool>> filter = null, //For filtering Data
            string includeProperties = null
            ); //Retrieve Queryable Data
        void Add(T entity); // Add data to Database
        void Remove(int id); //Get the data which we want to remove
        void Remove(T entity); //Remove the whole data from database
    }
}
