using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public class PermissionHelper
    {

        public static Boolean IsUserHasPermission(string accessToken, string permissionName)
        {
            var stream = accessToken;
            var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            string email = tokenS.Claims.First(claim => claim.Type == "email").Value;
            return true;
        }
    }
}
