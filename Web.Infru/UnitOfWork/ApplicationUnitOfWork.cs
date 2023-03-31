using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Infru.DBContexts;
using Web.Infru.Repository;

namespace Web.Infru.UnitOfWork
{
    public class ApplicationUnitOfWork : UnitOfWork, IApplicationUnitOfWork
    {
        public ICourseRepository Courses { get; private set; }

        public ApplicationUnitOfWork(IApplicationDbContext dbContext,
            ICourseRepository courseRepository) : base((DbContext)dbContext)
        {
            Courses = courseRepository;
        }
    }
}
