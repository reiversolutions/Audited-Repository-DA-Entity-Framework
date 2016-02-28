using AuditedRepository.DA.EntityFramework.Interfaces.Contexts;
using AuditedRepository.Interfaces.Models;
using AuditedRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditedRepository.DA.EntityFramework.Repositories
{
    /// <summary>
    /// Entity framework implementation of IRepository which audits changes
    /// </summary>
    /// <typeparam name="T">Entity extending AuditEntity</typeparam>
    public class AuditRepository<T> : RepositoryBase<T> where T : class, IEntity
    {
        private IDbContext _context;

        public AuditRepository(IDbContext context): base(context)
        {
            _context = context;
        }

        /// <summary>
        /// Insert a entity into the database and write an insert audit
        /// </summary>
        /// <param name="entity">Entity to be inserted</param>
        /// <returns>Successful</returns>
        public override bool Insert(T entity)
        {
            var result = base.Insert(entity);

            if (result)
            {
                // Audit insert

            }

            return result;
        }

        /// <summary>
        /// Update a entity and write an update audit
        /// </summary>
        /// <param name="entity">Entity to update</param>
        /// <returns>Successful</returns>
        public override bool Update(T entity)
        {
            var result = base.Update(entity);

            if (result)
            {
                // Audit update

            }

            return result;
        }

        /// <summary>
        /// Update an entity if it exists or insert it. Write an audit of the changes.
        /// </summary>
        /// <param name="entity">Entity to update or insert</param>
        /// <returns>Successful</returns>
        public override bool InsertOrUpdate(T entity)
        {
            var result = base.InsertOrUpdate(entity);

            if (result)
            {
                // Audit insert || update

            }

            return result;
        }
        
        /// <summary>
        /// Delete an entity and write a delete audit
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        /// <param name="archive">Perform a soft delete</param>
        /// <returns>Successful</returns>
        public override bool Delete(T entity, bool archive = true)
        {
            var result = base.Delete(entity, archive);

            if (result)
            {
                // Audit delete

            }

            return result;
        }
    }
}
