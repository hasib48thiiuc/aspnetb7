using Autofac;
using Web.Demo.Areas.Admin.Models;
using Web.Demo.Models;

namespace Web.Demo
{
    public class Webmodule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<CourseModel>().As<ICourseModel>()
                .InstancePerLifetimeScope();

            builder.RegisterType<CourseModel>().AsSelf();

            builder.RegisterType<CourseListModel>().AsSelf();

            builder.RegisterType<CourseCreateModel>().AsSelf();
            builder.RegisterType<RegisterModel>().AsSelf();
            builder.RegisterType<LoginModel>().AsSelf();



            base.Load(builder);
        }
    }
}
