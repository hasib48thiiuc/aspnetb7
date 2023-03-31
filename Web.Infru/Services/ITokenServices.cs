using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Web.Infru.Services
{
    public interface ITokenServices
    {
        Task<string> GetJwtToken(IList<Claim> claims);
    }
}
