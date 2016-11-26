using AuditedRepository.DA.EntityFramework.Interfaces.Contexts;
using AuditedRepository.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.DA.EntityFramework.Models
{
    public class MockDbContext : DbContext, IDbContext
    {
        public virtual DbSet<Entity> Entities { get; set; }
    }
}
