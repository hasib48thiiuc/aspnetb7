using Web.Infru.UnitOfWork;
using CourseBO = Web.Infru.Business_Objects.Course;
using CourseEO = Web.Infru.Enitites.Course;
namespace Web.Infru.Services
{
    public class CourseServices : ICourseServices
    {
        //ekhane business object ke pathabo
        private readonly IApplicationUnitOfWork _applicationUnitOfWork;
        public CourseServices(IApplicationUnitOfWork applicationUnitOfWork)
        {
            _applicationUnitOfWork = applicationUnitOfWork;
        }
        public void CreateCourse(CourseBO course)
        {
            CourseEO courseentity = new CourseEO();
            courseentity.Title = course.Title;
            courseentity.Fees = course.Fees;
            courseentity.ClassStartDate = course.ClassStartDate;

            _applicationUnitOfWork.Courses.Add(courseentity);
            _applicationUnitOfWork.Save();

        }
        public (int total, int totalDisplay, IList<CourseBO> records) GetCourses(int pageIndex,
           int pageSize, string searchText, string orderby)
        {
            (IList<CourseEO> data, int total, int totalDisplay) results = _applicationUnitOfWork
                .Courses.GetCourses(pageIndex, pageSize, searchText, orderby);

            IList<CourseBO> courses = new List<CourseBO>();
            foreach (CourseEO courseEO in results.data)
            {
                courses.Add(new CourseBO
                {
                    Id = courseEO.Id,
                    Title = courseEO.Title,
                    Fees = courseEO.Fees,
                    ClassStartDate = courseEO.ClassStartDate
                });
            }

            return (results.total, results.totalDisplay, courses);
        }

    }
}
