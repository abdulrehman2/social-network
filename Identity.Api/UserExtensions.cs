using System.Security.Claims;

namespace Identity.Api
{
 
    public static class UserExtensions
    {
      
        public static int GetUserId(this ClaimsPrincipal claimsIdentity)
        {
            var claim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == "sid");
            return (claim != null) ? Convert.ToInt32(claim.Value) : -1;
        }
        public static string GetCustomerExternalRefNumber(this ClaimsPrincipal claimsIdentity)
        {
            var claim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid);
            return (claim != null) ? claim.Value : "";
        }
        public static string GetCustomerName(this ClaimsPrincipal claimsIdentity)
        {
            var claim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName);
            return (claim != null) ? claim.Value : "";
        }
  


    }
}
