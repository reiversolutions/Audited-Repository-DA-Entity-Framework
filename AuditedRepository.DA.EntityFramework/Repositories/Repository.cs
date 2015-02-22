using AuditedRepository.Interfaces.Models;
using AuditedRepository.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AuditedRepository.DA.EntityFramework.Repositories
{
    public class Repository<T> : IRepository<T> where T : IEntity
    {
        public IEnumerable<T> Find(
            Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, 
            IOrderedQueryable<T>> orderBy = null, 
            long offset = 0, 
            long take = long.MaxValue, 
            string includeProperties = "")
        {
            throw new NotImplementedException();
        }

        public T FindById(string id)
        {
            throw new NotImplementedException();
        }

        public bool Insert(T entity)
        {
            throw new NotImplementedException();
        }

        public bool Update(T entity)
        {
            throw new NotImplementedException();
        }

        public bool InsertOrUpdate(T entity)
        {
            throw new NotImplementedException();
        }

        public bool Delete(string id, bool archive = true)
        {
            throw new NotImplementedException();
        }

        public bool Delete(T entity, bool archive = true)
        {
            throw new NotImplementedException();
        }

        public bool Save()
        {
            throw new NotImplementedException();
        }
    }
}