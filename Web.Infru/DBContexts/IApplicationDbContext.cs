using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Infru.Enitites;

namespace Web.Infru.DBContexts
{
    public interface IApplicationDbContext
    {
       public  DbSet<Course> Courses { get; set; }
    }
}
