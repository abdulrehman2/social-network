using System.Security.Claims;

namespace Post.Api
{
 
    public static class UserExtensions
    {
        //public static string GetSpecificClaim(this ClaimsIdentity claimsIdentity, string claimType)
        //{
        //    var claim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == claimType);
        //    return (claim != null) ? claim.Value : string.Empty;
        //}

        //public static int GetUserId(this ClaimsPrincipal claimsIdentity)
        //{
        //    var claim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
        //    return (claim != null) ? Convert.ToInt32(claim.Value) : -1;
        //}

        //public static string GetUserName(this ClaimsPrincipal claimsIdentity)
        //{
        //    var claim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name);
        //    return (claim != null) ? Convert.ToString(claim.Value) : null;
        //}


        #region customer terminal
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
        #endregion


        #region executive and rm terminal
        public static int GetEmployeeId(this ClaimsPrincipal claimsIdentity)
        {
            var claim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid);
            return (claim != null) ? Convert.ToInt32(claim.Value) : -1;
        }
        #endregion
    }
}
