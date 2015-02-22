using AuditedRepository.Interfaces.Models;
using AuditedRepository.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditedRepository.DA.EntityFramework.Interfaces.Contexts
{
    /// <summary>
    /// Interface for DbContext
    /// </summary>
    /// <typeparam name="T">Enity extending Enity</typeparam>
    public interface IDbContext<T> where T : Entity
    {
        /// <summary>
        /// Dispose of context
        /// </summary>
        void Dispose();
        /// <summary>
        /// Save all modified data
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
        /// <summary>
        /// Get DbSet from context
        /// </summary>
        /// <returns></returns>
        DbSet<T> Set();
        /// <summary>
        /// Get Database
        /// </summary>
        Database Database { get; }
    }
}
