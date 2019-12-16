using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataFormManager.Models;
using System.Data.SqlClient;
using System.Configuration;

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

        public static TokenObjectModel createToken()
        {
            Guid AccessTokenObj = Guid.NewGuid();
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
