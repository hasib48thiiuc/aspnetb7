using Autofac;
using Web.Demo.Models;
using Web.Infru.Services;
using Web.Demo.Models;

namespace Web.Demo.Areas.Admin.Models
{
    public class CourseListModel : BaseModel
    {
        private ICourseServices _courseservice;

        public CourseListModel() : base()
        {

        }
        public CourseListModel(ICourseServices courseservice)
        {
            _courseservice = courseservice;
        }
        public override void ResolveDependency(ILifetimeScope scope)
        {

            base.ResolveDependency(scope);
            _courseservice = _scope.Resolve<ICourseServices>();
        }


        internal object? GetPagedCourses(DataTablesAjaxRequestModel model)
        {
            var data = _courseservice.GetCourses(
                           model.PageIndex,
                           model.PageSize,
                           model.SearchText,
                           model.GetSortText(new string[] { "Title", "Fees", "ClassStartDate" }));

            return new
            {
                recordsTotal = data.total,
                recordsFiltered = data.totalDisplay,
                data = (from record in data.records
                        select new string[]
                        {
                            record.Title,
                            record.Fees.ToString(),
                            record.ClassStartDate.ToString(),
                            record.Id.ToString()
                        }
                    ).ToArray()
            };
        }
    }  }

