using Autofac;
using static System.Formats.Asn1.AsnWriter;

namespace Web.Demo.Areas.Admin.Models
{
    public class BaseModel
    {
        protected ILifetimeScope? _scope;
  
        public BaseModel()
        {

        }

        public  virtual void ResolveDependency(ILifetimeScope scope)
        {
            _scope = scope;
        }
    }

}
