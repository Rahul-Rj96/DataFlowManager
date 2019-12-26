using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataFormManager.Models;
using System.Data.SqlClient;
using System.Configuration;
using System.IdentityModel;
using System.Security;
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

            var issuer = "https://dataformmanager.io";
            var authority = "https://dataformmanager.io"; 
            var privateKey = "J6k2eVCTXDp5b97u6gNH5GaaqHDxCmzz2wv3PRPFRsuW2UavK8LGPRauC4VSeaetKTMtVmVzAC8fh8Psvp8PFybEvpYnULHfRpM8TA2an7GFehrLLvawVJdSRqh2unCnWehhh2SJMMg5bktRRapA8EGSgQUV8TCafqdSEHNWnGXTjjsMEjUpaxcADDNZLSYPMyPSfp6qe5LMcd5S9bXH97KeeMGyZTS2U8gp3LGk2kH4J4F3fsytfpe9H9qKwgjb";
            var daysValid = 7;

            var createJwt =  await CreateJWTAsync(userObj,issuer, authority, privateKey, daysValid);

            return createJwt;
            // string key = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";

            // var securityKey = new Microsoft
            //    .IdentityModel.Tokens.SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));

            // var credentials = new Microsoft.IdentityModel.Tokens.SigningCredentials
            //                   (securityKey, SecurityAlgorithms.HmacSha256Signature);


            // var header = new JwtHeader(credentials);

            // var payload = new JwtPayload
            //{
            //    { "userId", userObj.UserId},
            //    {"username", userObj.Username },
            //    {"email", userObj.EmailId },
            //    { "issuer", "http://dataformmanager.com/"},
            //};

            // var secToken = new JwtSecurityToken(header, payload);
            // var handler = new JwtSecurityTokenHandler();


            // var tokenString = handler.WriteToken(secToken);
            // return tokenString;
        }

        public static async Task<string> CreateJWTAsync(
            UserObjectModel user,
            string issuer,
            string authority,
            string symSec,
            int daysValid)
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
                expires: DateTime.UtcNow.AddDays(daysValid),
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
