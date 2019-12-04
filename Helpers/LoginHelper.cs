using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DataFormManager.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.SessionState;

namespace Helpers
{
    public class LoginHelper
    {

        public static SqlConnectionStringBuilder getConnectionString()
        {
            // Build connection string

            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
            builder.DataSource = ConfigurationManager.AppSettings["ServerName"];
            builder.UserID = ConfigurationManager.AppSettings["UserId"];
            builder.Password = ConfigurationManager.AppSettings["Password"];
            builder.InitialCatalog = ConfigurationManager.AppSettings["DbName"];
            return builder;
        }


        public static UserObjectModel GetAccesToken(string code)
        {
            string url = "https://www.googleapis.com/oauth2/v4/token";
            WebClient wc = new WebClient();

            wc.QueryString.Add("code", code);
            wc.QueryString.Add("client_id", "892661883096-49tbcjjtktgr35m3cemah46llusc572t.apps.googleusercontent.com");
            wc.QueryString.Add("client_secret", "B19605NTuDOU8xqqhjDzYA65");
            wc.QueryString.Add("redirect_uri", "http://dataformmanager.dev37.grcdev.com/api/login/google/");
            wc.QueryString.Add("grant_type", "authorization_code");


            if (code == null)
            {
                UserObjectModel dummyUserObj = new UserObjectModel(1000, "sachin", "sachin@rckr.com", "276219201");
                return (dummyUserObj);
            }
            else
            {
                var data = wc.UploadValues(url, "POST", wc.QueryString);
                var responseString = UnicodeEncoding.UTF8.GetString(data);
                TokenObjectModel token = JsonConvert.DeserializeObject<TokenObjectModel>(responseString);


                var stream = token.id_token;
                var handler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                var jsonToken = handler.ReadToken(stream);
                var tokenS = handler.ReadToken(stream) as JwtSecurityToken;
                var sub = tokenS.Claims.First(claim => claim.Type == "sub").Value;
                var email = tokenS.Claims.First(claim => claim.Type == "email").Value;
                var name = tokenS.Claims.First(claim => claim.Type == "name").Value;
                UserObjectModel userFakeObj = new UserObjectModel(1, name, email, sub);
                bool isUserExists = IsUserRegistered(sub);
                bool registerSuccess = false;
                if (!isUserExists)
                {
                    registerSuccess = RegisterUser(userFakeObj) ? true : false;
                }
                if (isUserExists | registerSuccess)
                {
                    CreateUserSession(userFakeObj);
                }

                return userFakeObj;

            }

        }

        public static string GetAuthCode()
        {
            string url = @"https://accounts.google.com/o/oauth2/v2/auth?scope=profile+email+openid&access_type=offline&include_granted_scopes=true&state=state_parameter_passthrough_value&redirect_uri=http%3A%2F%2Fdataformmanager.dev37.grcdev.com%2Fapi%2Flogin%2Fgoogle%2F&response_type=code&client_id=892661883096-49tbcjjtktgr35m3cemah46llusc572t.apps.googleusercontent.com";
            return url;
        }


        public static bool IsUserRegistered(string sub)
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
                    cmd.CommandText = "Proc_RCKRUser_IsUserExist";
                    cmd.Parameters.Add(new SqlParameter("@Sub", sub));
                    rdr = cmd.ExecuteReader();
                    if (rdr.Read())
                    {
                        bool IsUserExists = bool.Parse((string)rdr["UserExists"]);
                        return IsUserExists;
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

        public static bool RegisterUser(UserObjectModel UserObj)
        {
            try
            {
                SqlConnectionStringBuilder builder = getConnectionString();
                using (SqlConnection connection = new SqlConnection(builder.ConnectionString))
                {
                    connection.Open();
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Connection = connection;
                    cmd.CommandText = "Proc_RCKRUser_CreateUser";
                    cmd.Parameters.Add(new SqlParameter("@Username", UserObj.Username));
                    cmd.Parameters.Add(new SqlParameter("@EmailId", UserObj.EmailId));
                    cmd.Parameters.Add(new SqlParameter("@Sub", UserObj.Sub));
                    bool IsSuccess = cmd.ExecuteNonQuery() != 0 ? true : false;
                    return IsSuccess;
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
                return false;
            }
        }

        public static void CreateUserSession(UserObjectModel userObj)
        {
            HttpContext.Current.Session["UserId"] = userObj.UserId;
            HttpContext.Current.Session["Username"] = userObj.Username;
            HttpContext.Current.Session["Sub"] = userObj.Sub;
        }

    }
}
