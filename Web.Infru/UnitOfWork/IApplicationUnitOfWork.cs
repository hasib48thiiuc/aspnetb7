using Web.Infru.Repository;

namespace Web.Infru.UnitOfWork
{
    public interface IApplicationUnitOfWork:IUnitOfWork 
    {
        ICourseRepository Courses { get; }
    }
}