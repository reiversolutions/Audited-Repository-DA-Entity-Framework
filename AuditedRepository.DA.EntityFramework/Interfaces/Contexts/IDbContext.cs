using AuditedRepository.Interfaces.Models;
using AuditedRepository.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuditedRepository.DA.EntityFramework.Interfaces.Contexts
{
    /// <summary>
    /// Interface for DbContext
    /// </summary>
    public interface IDbContext : IDisposable, IObjectContextAdapter
    {
        /// <summary>
        /// Save all modified data
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
        
        /// <summary>
        /// Retrieve DbSet
        /// </summary>
        /// <typeparam name="T">Type of class</typeparam>
        /// <returns>DbSet</returns>
        DbSet<T> Set<T>() where T : class;


        /// <summary>
        /// Get Database
        /// </summary>
        Database Database { get; }
    }
}
