using Autofac;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.Build.Framework;
using Web.Infru.Business_Objects;
using Web.Infru.Services;

namespace Web.Demo.Areas.Admin.Models
{
    public class CourseCreateModel:BaseModel
    {
        [Required]
        public string Title { get; set; }

        public String Fees { get; set; }

        public DateTime ClassStartDate { get; set; }

        private ICourseServices _courseservice;
        public CourseCreateModel():base()
        {

        }
        public CourseCreateModel(ICourseServices courseservice)
        {
            _courseservice = courseservice;
        }
        public  override void ResolveDependency(ILifetimeScope scope)
        {

            base.ResolveDependency(scope);
            _courseservice = _scope.Resolve<ICourseServices>();
        }
        internal async Task CreateCourse()
        {

            Course course = new Course();

            course.Title= Title;
            course.Fees= Fees;
            course.ClassStartDate= ClassStartDate;

            _courseservice.CreateCourse(course);
        }
    }
}
