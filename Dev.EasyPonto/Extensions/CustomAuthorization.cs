using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Dev.EasyPonto.Extensions
{
    public class CustomAuthorization
    {
        public static bool ValidarClaimsUsuario(string claimName, string claimValue)
        {
            var identity = (ClaimsIdentity)HttpContext.Current.User.Identity;
            var claim = identity.Claims.FirstOrDefault(c => c.Type == claimName);
            return claim != null && claim.Value.Contains(claimValue);
        }


        public class ClaimsAuthorizeAttribute : AuthorizeAttribute
        {
            private readonly string _claimName;
            private readonly string _claimValue;

            public ClaimsAuthorizeAttribute(string claimName, string claimValue)
            {
                _claimName = claimName;
                _claimValue = claimValue;
            }

            protected override bool AuthorizeCore(HttpContextBase httpContext)
            {
                return CustomAuthorization.ValidarClaimsUsuario(_claimName, _claimValue);
            }


        }
    }
}