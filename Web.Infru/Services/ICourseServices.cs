using Web.Infru.Business_Objects;

namespace Web.Infru.Services
{
    public interface ICourseServices
    {
        void CreateCourse(Course course);
        (int total, int totalDisplay, IList<Course> records) GetCourses(int pageIndex, int pageSize, string searchText, string orderby);
    }
}