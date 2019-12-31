using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{

    public class JwtTokenObject
    {

        public string Username { get; set; }
        public string RoleName { get; set; }
        public string FormName { get; set; }
        public string Permission { get; set; }

    }

    public class PermissionHelper
    {

        public static IDictionary<string, int> PermissionValueDict = new Dictionary<string, int>()
        {
            {"Read",1 },
            {"Write",2 },
            {"FullAccess",4}
        };
        

        public static Boolean IsUserHasPermission(string accessToken, string formName,string permissionName)
        {
            var stream = accessToken;
            var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jsonToken = handler.ReadToken(stream);
            var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
            List<string> jsonStrPermissions = tokenS.Claims.Where(claim => claim.Type == "role").Select(a => a.Value).ToList();
            int permissionValue = 0;
            foreach (string permission in jsonStrPermissions)
            {
                JwtTokenObject result = JsonConvert.DeserializeObject<JwtTokenObject>(permission);
                if (result.FormName == formName)
                {                
                    permissionValue += PermissionValueDict[result.Permission]; 
                }

            };

            if (permissionValue>= PermissionValueDict[permissionName])
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
