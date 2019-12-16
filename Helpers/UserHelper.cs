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
    public class UserHelper
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

        public static UserObjectModel getUserBySubKey(string subkey)
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
                    cmd.CommandText = "Proc_RCKRUser_getUserBySubKey";
                    cmd.Parameters.Add(new SqlParameter("@Sub", subkey));
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
    }
}
