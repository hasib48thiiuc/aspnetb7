using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;

namespace Web.Infru.Securities
{
    public class CourseViewRequirementHandler :
          AuthorizationHandler<CourseViewRequirement>
    {
        protected override Task HandleRequirementAsync(
               AuthorizationHandlerContext context,
               CourseViewRequirement requirement)
        {
            if (context.User.HasClaim(x => x.Type == "ViewCourse" && x.Value == "true"))
            {
                context.Succeed(requirement);
            } 

            return Task.CompletedTask;
        }
    }
}
