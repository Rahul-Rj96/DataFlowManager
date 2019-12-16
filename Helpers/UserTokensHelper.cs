using DataFormManager.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
    public  class UserTokensHelper
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

        public static bool MapUserToken(UserTokensObjectModel userTokenObj)
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
                    cmd.CommandText = "Proc_UserTokens_CreateUserToken";
                    cmd.Parameters.Add(new SqlParameter("@UserId", userTokenObj.UserId));
                    cmd.Parameters.Add(new SqlParameter("@TokenId", userTokenObj.TokenId));
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
    }
}
