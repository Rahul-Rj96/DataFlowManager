using DataFormManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Helpers
{
   public class UserSpecificFormsHelper
    {

        public static List<FormDataModel> GetUserFormDataList(int userId,string formTypename)
        {
            List<FormDataModel> formDatas = new List<FormDataModel>();
            string dataString = null;
            string connString = ConfigurationManager.ConnectionStrings["DFMDBConnectionString"].ConnectionString;       //read from config  
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    String spGetUserFormDatas = @"[dbo].[Proc_Form_GetUserFormDatas]";
                    SqlCommand cmd = new SqlCommand(spGetUserFormDatas, conn);
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param1 = cmd.Parameters.Add("@UserId", SqlDbType.Int);
                    param1.Value = userId;
                    SqlParameter param2 = cmd.Parameters.Add("@FormTypeName", SqlDbType.VarChar);
                    param2.Value = formTypename;
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.HasRows && dr.Read())
                    {
                        dataString = Convert.ToString(dr["FormData"]);
                        FormDataModel data = JsonConvert.DeserializeObject<FormDataModel>(dataString);
                        formDatas.Add(data);
                    }
                    dr.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
            }
            return formDatas;

        }

        public static List<FormDataModel> GetLimitedUserFormDataList(int userId, string formTypename,string start, string count)
        {
            List<FormDataModel> formDatas = new List<FormDataModel>();
            string dataString = null;
            
            string connString = ConfigurationManager.ConnectionStrings["DFMDBConnectionString"].ConnectionString;       //read from config  
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    String spGetUserFormDatas = @"[dbo].[Proc_Form_GetLimitedUserFormDatas]";
                    SqlCommand cmd = new SqlCommand(spGetUserFormDatas, conn);
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param1 = cmd.Parameters.Add("@UserId", SqlDbType.Int);
                    param1.Value = userId;
                    SqlParameter param2 = cmd.Parameters.Add("@FormTypeName", SqlDbType.VarChar);
                    param2.Value = formTypename;
                    SqlParameter param3 = cmd.Parameters.Add("@Start", SqlDbType.Int);
                    param3.Value = Int32.Parse(start);
                    SqlParameter param4 = cmd.Parameters.Add("@Count", SqlDbType.Int);
                    param4.Value = Int32.Parse(count);
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.HasRows && dr.Read())
                    {
                        dataString = Convert.ToString(dr["FormData"]);
                        FormDataModel data = JsonConvert.DeserializeObject<FormDataModel>(dataString);
                        formDatas.Add(data);
                    }
                    dr.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
            }
            return formDatas;

        }
    }
}
