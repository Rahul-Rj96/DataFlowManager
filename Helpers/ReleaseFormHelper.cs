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
    public class ReleaseFormHelper
    {
        public static int AddReleaseData(ReleaseObjectModel releaseData)
        {
            int formId = new int();
            string jsonReleaseData = JsonConvert.SerializeObject(releaseData);
            string connString = ConfigurationManager.ConnectionStrings["ReleaseFlowDBConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    String spAddReleaseData = @"dbo.[Proc_Form_AddReleaseData]";
                    SqlCommand cmd = new SqlCommand(spAddReleaseData, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlParameter releaseFlowParam1 = cmd.Parameters.AddWithValue("@FormData", jsonReleaseData);
                    //releaseFlowParam1.SqlDbType = SqlDbType.Structured;
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        formId = Convert.ToInt32(reader["FormID"]);
                    }
                    reader.Close();
                    conn.Close();


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
            }
            return formId;
        }
    }
}
