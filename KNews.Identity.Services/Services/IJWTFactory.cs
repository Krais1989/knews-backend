using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace KNews.Identity.Services.Services
{
    public interface IJWTFactory
    {
        string Generate(IEnumerable<Claim> Claims);
    }
}
