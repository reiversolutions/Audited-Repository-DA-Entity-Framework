using AuditedRepository.DA.EntityFramework.Interfaces.Contexts;
using AuditedRepository.Interfaces.Models;
using AuditedRepository.Interfaces.Repositories;
using AuditedRepository.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AuditedRepository.DA.EntityFramework.Repositories
{
    /// <summary>
    /// Entity framework implementation of IRepository
    /// </summary>
    /// <typeparam name="T">Etnity extending Etnity</typeparam>
    public abstract class RepositoryBase<T> : IRepository<T> where T : class, IEntity
    {
        private IDbContext _context;

        public RepositoryBase(IDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Check if a query returns any entities
        /// </summary>
        /// <param name="filter">Filter query</param>
        /// <returns>Any entities returned</returns>
        public virtual bool Any(Expression<Func<T, bool>> filter = null)
        {
            IQueryable<T> query = _context.Set<T>();
            
            if (query != null)
            {
                query = query.Where(filter);
            }
            query = query.Where(x => !x.IsArchived);

            return query.Any();
        }

        /// <summary>
        /// Find all relevant entities
        /// </summary>
        /// <param name="filter">Filter query</param>
        /// <param name="orderBy">Order by options</param>
        /// <param name="offset">Pagination offset</param>
        /// <param name="take">Pagination amount</param>
        /// <param name="includeProperties">Included properties</param>
        /// <returns>List of entities</returns>
        public virtual IEnumerable<T> Find(
            Expression<Func<T, bool>> filter = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, 
            long offset = 0, 
            long take = long.MaxValue, 
            string includeProperties = "")
        {
            IQueryable<T> query = _context.Set<T>();

            if (filter != null)
            {
                query = query.Where(filter);
            }
            query = query.Where(x => !x.IsArchived);

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        /// <summary>
        /// Find specific entity
        /// </summary>
        /// <param name="id">Primary key identifier</param>
        /// <param name="bypassArchived">Ignore the archive filter</param>
        /// <returns>Entity</returns>
        public virtual T FindById(string id, bool bypassArchived = false)
        {
            return _context.Set<T>().FirstOrDefault(x => (bypassArchived || !x.IsArchived) && x.Id == id);
        }
        
        /// <summary>
        /// Insert a entity into the database
        /// </summary>
        /// <param name="entity">Entity to be inserted</param>
        /// <returns>Successful</returns>
        public virtual bool Insert(T entity)
        {
            DateTime now = DateTime.Now;
            entity.ModifiedDate = now;
            entity.CreatedDate = now;

            T result = _context.Set<T>().Add(entity);

            return result != null;
        }

        /// <summary>
        /// Update a entity
        /// </summary>
        /// <param name="entity">Entity to update</param>
        /// <returns>Successful</returns>
        public virtual bool Update(T entity)
        {
            entity.ModifiedDate = DateTime.Now;
            T result = _context.Set<T>().Attach(entity);

            return result != null;
        }

        /// <summary>
        /// Update an entity if it exists or insert it
        /// </summary>
        /// <param name="entity">Entity to update or insert</param>
        /// <returns>Successful</returns>
        public bool InsertOrUpdate(T entity)
        {
            T result = FindById(entity.Id, true);
            if (result != null)
            {
                return Update(entity);
            } else
            {
                if (result.IsArchived)
                {
                    return false;
                }
                return Insert(entity);
            }
        }

        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="id">Primary key identifier</param>
        /// <param name="archive">Perform a soft delete</param>
        /// <returns>Successful</returns>
        public virtual bool Delete(string id, bool archive = true)
        {
            return Delete(FindById(id), archive);
        }

        /// <summary>
        /// Delete an entity
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        /// <param name="archive">Perform a soft delete</param>
        /// <returns>Successful</returns>
        public virtual bool Delete(T entity, bool archive = true)
        {
            if (archive)
            {
                entity.IsArchived = true;
                entity.ModifiedDate = DateTime.Now;

                return Update(entity);
            } else
            {
                T result = _context.Set<T>().Remove(entity);

                return result != null;
            }
        }

        /// <summary>
        /// Save all previous data modifications
        /// </summary>
        /// <returns>Successful</returns>
        public virtual bool Save()
        {
            int result = _context.SaveChanges();
            return result >= 0;
        }
    }
}