using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataFormManager.Models;
using System.Data.SqlClient;
using System.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Newtonsoft.Json;

namespace Helpers
{
    public class TokenHelper
    {

        public static SqlConnectionStringBuilder getConnectionString()
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = ConfigurationManager.AppSettings["ServerName"];
            builder.UserID = ConfigurationManager.AppSettings["UserId"];
            builder.Password = ConfigurationManager.AppSettings["Password"];
            builder.InitialCatalog = ConfigurationManager.AppSettings["DbName"];
            return builder;
        }

        public static TokenObjectModel createToken(UserObjectModel userObj)
        {
            //Guid AccessTokenObj = Guid.NewGuid();
            var AccessTokenObj = Task.Run(() => CreateJWTToken(userObj)).Result;
            Guid RefreshTokenObj = Guid.NewGuid();
            Guid AuthorizationCodeObj = Guid.NewGuid();
              
            DateTime currentTime = DateTime.Now;
            DateTime x5MinsLater = currentTime.AddMinutes(5);

            try
            {
                SqlConnectionStringBuilder builder = getConnectionString();
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Connection = connection;
                    cmd.CommandText = "Proc_Token_CreateToken";
                    cmd.Parameters.Add(new SqlParameter("@AccessToken", AccessTokenObj.ToString()));
                    cmd.Parameters.Add(new SqlParameter("@RefreshToken", RefreshTokenObj.ToString()));
                    SqlParameter parameter = cmd.Parameters.Add("@ExpiresIn", System.Data.SqlDbType.DateTime);
                    parameter.Value = x5MinsLater;
                    cmd.Parameters.Add(new SqlParameter("@AuthorizationCode", AuthorizationCodeObj.ToString()));
                    bool IsSuccess = cmd.ExecuteNonQuery() != 0 ? true : false;
                    TokenObjectModel tokenObj = new TokenObjectModel(AccessTokenObj.ToString(), RefreshTokenObj.ToString(), x5MinsLater,AuthorizationCodeObj.ToString());
                    return tokenObj;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
                return null;
            }
            
        }

        public static async Task<string> CreateJWTToken(UserObjectModel userObj)
        {

            var issuer = ConfigurationManager.AppSettings["issuer"];
            var authority = ConfigurationManager.AppSettings["authority"];
            var privateKey = ConfigurationManager.AppSettings["private_key"];
            var daysValid = 7;
            DateTime currentTime = DateTime.Now;
            DateTime x5MinsLater = currentTime.AddMinutes(5);

            var createJwt =  await CreateJWTAsync(userObj,issuer, authority, privateKey, daysValid, x5MinsLater);

            return createJwt;
           
        }

        public static async Task<string> CreateJWTAsync(
            UserObjectModel user,
            string issuer,
            string authority,
            string symSec,
            int daysValid,
            DateTime x5minsLater)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            ClaimsIdentity claims;
            try
            {
                claims = Task.Run(() => CreateClaimsIdentities(user)).Result;
            }
            catch(Exception e)
            {
                throw e;
            }

            // Create JWToken
            var token = tokenHandler.CreateJwtSecurityToken(issuer: issuer,
                audience: authority,
                subject: claims,
                notBefore: DateTime.UtcNow,
                //expires: DateTime.UtcNow.AddDays(daysValid),
                expires: x5minsLater,
                signingCredentials:
                new SigningCredentials(
                    new SymmetricSecurityKey(
                        Encoding.Default.GetBytes(symSec)),
                        SecurityAlgorithms.HmacSha256Signature));

            return tokenHandler.WriteToken(token);
        }

        public static Task<ClaimsIdentity> CreateClaimsIdentities(UserObjectModel user)
        {
            var userData = GetUserPermissionObjectByUserId(user.UserId); 
            ClaimsIdentity claimsIdentity = new ClaimsIdentity();
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Email, user.EmailId));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()));
            claimsIdentity.AddClaim(new Claim(ClaimTypes.Name, user.Username));
            //claimsIdentity.AddClaim(new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(userData)));

            //var roles = Enumerable.Empty<Role>(); // Not a real list.
            //foreach (var data in userData)
            //{ claimsIdentity.AddClaim(new Claim(ClaimTypes.UserData, JsonConvert.SerializeObject(data))); }

            foreach (var data in userData)
            { claimsIdentity.AddClaim(new Claim(ClaimTypes.Role, JsonConvert.SerializeObject(data))); }

            return Task.FromResult(claimsIdentity);
        }


        public static List<UserFormPermissionObjectModel> GetUserPermissionObjectByUserId (int userId)
        {
            SqlDataReader rdr = null;
            var userPermissionList = new List<UserFormPermissionObjectModel>();
            try
            {
                SqlConnectionStringBuilder builder = getConnectionString();

                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Connection = connection;
                    cmd.CommandText = "Proc_UserRoleFormTypePermissions_GetUserRoleFormTypePermissionByUserId";
                    cmd.Parameters.Add(new SqlParameter("@userId", userId));
                    rdr = cmd.ExecuteReader();
                    while (rdr.Read())
                    {
                        String Username = (String)rdr["Username"];
                        string RoleName = (String)rdr["RoleName"];
                        string FormName = (String)rdr["FormName"];
                        string Permission = (string)rdr["PermisssionName"];
                        UserFormPermissionObjectModel userFormPermissionObj = new UserFormPermissionObjectModel(Username, RoleName, FormName, Permission);
                        userPermissionList.Add(userFormPermissionObj);
                    }
                    return userPermissionList;
                }
            }
            catch (SqlException e)
            {
                return userPermissionList;
            }
        }
            
        public static TokenObjectModel getTokenByAuthorizationCode(string AuthorizationCode)
        {
            SqlDataReader rdr = null;
            try
            {
                SqlConnectionStringBuilder builder = getConnectionString();
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Connection = connection;
                    cmd.CommandText = "Proc_Token_GetTokenByAuthorizationCode";
                    cmd.Parameters.Add(new SqlParameter("@AuthorizationCode", AuthorizationCode));
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        TokenObjectModel tokenObj = new TokenObjectModel((int)rdr["TokenId"], (String)rdr["AccessToken"], (String)rdr["RefreshToken"], Convert.ToDateTime(rdr["ExpiresIn"]),(string)rdr["AuthorizationCode"]);
                        return tokenObj;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
                return null;
            }
        }

        public static UserObjectModel getUserByRefreshToken(string refreshToken)
        {
            SqlDataReader rdr = null;
            try
            {
                SqlConnectionStringBuilder builder = getConnectionString();
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Connection = connection;
                    cmd.CommandText = "Proc_UserTokens_GetUserByRefreshToken";
                    cmd.Parameters.Add(new SqlParameter("@RefreshToken", refreshToken));
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        UserObjectModel userObj = new UserObjectModel((int)rdr["UserId"], (String)rdr["Username"], (String)rdr["EmailId"], (String)rdr["Sub"]);
                        return userObj;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
                return null;
            }
        }

        public static UserObjectModel getUserByAccessToken(string accessToken)
        {
            SqlDataReader rdr = null;
            try
            {
                SqlConnectionStringBuilder builder = getConnectionString();
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Connection = connection;
                    cmd.CommandText = "Proc_UserTokens_GetUserByAccessToken";
                    cmd.Parameters.Add(new SqlParameter("@AccessToken", accessToken));
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        UserObjectModel userObj = new UserObjectModel((int)rdr["UserId"], (String)rdr["Username"], (String)rdr["EmailId"], (String)rdr["Sub"]);
                        return userObj;
                    }
                    else
                    {
                        return null;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
                return null;
            }
        }
        public static bool IsAccessTokenValid(string accessToken)
        {
            SqlDataReader rdr = null;
            try
            {
                SqlConnectionStringBuilder builder = getConnectionString();
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Connection = connection;
                    cmd.CommandText = "Proc_Token_IsAccessTokenValid";
                    cmd.Parameters.Add(new SqlParameter("@AccessToken", accessToken));
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        bool IsAccessTokenValid = bool.Parse((string)rdr["AccessTokenValid"]);
                        return IsAccessTokenValid;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
                return false;
            }
        }
    }
}
