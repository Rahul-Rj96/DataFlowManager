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

        public static List<FormDataModel> GetUserFormDataList(int userId)
        {
            List<FormDataModel> formDatas = new List<FormDataModel>();
            string dataString = null;
            string connString = ConfigurationManager.ConnectionStrings["ReleaseFlowDBConnectionString"].ConnectionString;       //read from config  
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    String spGetUserFormDatas = @"[dbo].[Proc_Form_GetUserFormDatas]";
                    SqlCommand cmd = new SqlCommand(spGetUserFormDatas, conn);
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = cmd.Parameters.Add("@UserId", SqlDbType.Int);
                    param.Value = userId;
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
