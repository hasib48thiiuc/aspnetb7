using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.Infru.DBContexts;
using Web.Infru.Enitites;

namespace Web.Infru.Repository
{
    public class CourseRepository : Repository<Course, Guid>,ICourseRepository
    {
        public CourseRepository(IApplicationDbContext context) : base((DbContext)context)
        {
        }

        public (IList<Course> data, int total, int totalDisplay) GetCourses(int pageIndex,
           int pageSize, string searchText, string orderby)
        {
            (IList<Course> data, int total, int totalDisplay) results =
                GetDynamic(x => x.Title.Contains(searchText), orderby,
               "Topics,CourseStudents",
               pageIndex, pageSize, true);

            return results;
        }
    }
}
