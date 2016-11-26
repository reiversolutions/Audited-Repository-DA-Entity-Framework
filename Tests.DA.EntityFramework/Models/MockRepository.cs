using AuditedRepository.DA.EntityFramework.Repositories;
using AuditedRepository.Interfaces.Repositories;
using AuditedRepository.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuditedRepository.DA.EntityFramework.Interfaces.Contexts;

namespace Tests.DA.EntityFramework.Models
{
    public class MockRepository : RepositoryBase<Entity>, IRepository<Entity>
    {
        public MockRepository(IDbContext context) : base(context) { }
    }
}
