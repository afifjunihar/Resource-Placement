using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Library
{
    public static class Claims
    {
      public static string GetSpecificClaim(this ClaimsIdentity claimsIdentity, string claimType)
      {
         var claim = claimsIdentity.Claims.FirstOrDefault(x => x.Type == claimType);

         return (claim != null) ? claim.Value : string.Empty;
      }

      public static string GetName(this ClaimsIdentity claimsIdentity)
		{
         var claim = claimsIdentity.FindFirst(ClaimTypes.Name);

         return (claim != null) ? claim.Value : string.Empty;
      }

      public static string GetSid(this ClaimsIdentity claimsIdentity)
      {
         var claim = claimsIdentity.FindFirst(ClaimTypes.Sid);

         return (claim != null) ? claim.Value : string.Empty;
      }
   }
}
