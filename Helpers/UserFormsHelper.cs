using DataFormManager.Models;
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
    public class UserFormsHelper
    {

        public static void AddUserFormsData(UserFormObjectModel userFormsData)
        {
            int formId = userFormsData.FormId;
            int userId = userFormsData.UserId;
            string connString = ConfigurationManager.ConnectionStrings["ReleaseFlowDBConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    String spAddUserFormsData = @"dbo.[Proc_UserForms_AddMappingData]";
                    SqlCommand cmd = new SqlCommand(spAddUserFormsData, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlParameter releaseFlowParam1 = cmd.Parameters.AddWithValue("@FormId", formId);
                    SqlParameter releaseFlowParam2 = cmd.Parameters.AddWithValue("@UserId", userId);
                    var reader = cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
            }
        }

    }
}
