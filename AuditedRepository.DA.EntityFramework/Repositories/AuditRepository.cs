using AuditedRepository.DA.EntityFramework.Interfaces.Contexts;
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
    /// <typeparam name="T">Entity extending Entity</typeparam>
    public class AuditRepository<T> : RepositoryBase<T> where T : Entity
    {
        public AuditRepository(IDbContext<T> context): base(context)
        {

        }

        /// <summary>
        /// Insert a entity into the database and write an insert audit
        /// </summary>
        /// <param name="entity">Entity to be inserted</param>
        /// <returns>Successful</returns>
        public override bool Insert(T entity)
        {
            return base.Insert(entity);
        }

        /// <summary>
        /// Update a entity and write an update audit
        /// </summary>
        /// <param name="entity">Entity to update</param>
        /// <returns>Successful</returns>
        public override bool Update(T entity)
        {
            return base.Update(entity);
        }

        /// <summary>
        /// Update an entity if it exists or insert it. Write an audit of the changes.
        /// </summary>
        /// <param name="entity">Entity to update or insert</param>
        /// <returns>Successful</returns>
        public override bool InsertOrUpdate(T entity)
        {
            return base.InsertOrUpdate(entity);
        }
        
        /// <summary>
        /// Delete an entity and write a delete audit
        /// </summary>
        /// <param name="entity">Entity to delete</param>
        /// <param name="archive">Perform a soft delete</param>
        /// <returns>Successful</returns>
        public override bool Delete(T entity, bool archive = true)
        {
            return base.Delete(entity, archive);
        }
    }
}
