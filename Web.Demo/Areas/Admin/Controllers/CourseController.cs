using Autofac;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.Demo.Areas.Admin.Models;
using Web.Demo.Models;


namespace Web.Demo.Areas.Admin.Controllers
{
    [Area("Admin")]
//    [Authorize(Roles ="Admin,Teacher")]

    public class CourseController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly ILogger<CourseController> _logger;
        public CourseController(ILogger<CourseController> logger, ILifetimeScope scope)
        {
            _scope = scope;
            _logger = logger;
        }
        [Authorize(Policy = "CourseViewRequirementPolicy")]

        public IActionResult Index()
        {
            return View();
        }
        [Authorize(Policy = "CourseCreatePolicy")]

        public IActionResult Create()

        {
            CourseCreateModel model = _scope.Resolve<CourseCreateModel>();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "CourseViewPolicy")]
 
        public async Task<IActionResult> Create(CourseCreateModel model)
        {
            if(ModelState.IsValid)
            {
                model.ResolveDependency(_scope);
                await model.CreateCourse();
            }
            return View(model);
        }
        // nicher code tao copy kora  
        public JsonResult GetCourseData()
        {
            var dataTableModel = new DataTablesAjaxRequestModel(Request);
            var model = _scope.Resolve<CourseListModel>();
             return Json(model.GetPagedCourses(dataTableModel));
        }

    }
}
