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

        public static void AddUserFormsData(List<UserFormObjectModel> userFormsData)
        {
            DataTable userFormsTable = new DataTable();
            userFormsTable.Columns.Add("UserId", typeof(Int32));
            userFormsTable.Columns.Add("FormId", typeof(Int32));
            foreach (UserFormObjectModel userFormdata in userFormsData)
            {
                userFormsTable.Rows.Add(userFormdata.UserId , userFormdata.FormId);
            }
        string connString = ConfigurationManager.ConnectionStrings["ReleaseFlowDBConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    String spAddUserFormsData = @"dbo.[Proc_UserForms_AddMappingData]";
                    SqlCommand cmd = new SqlCommand(spAddUserFormsData, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    if (userFormsTable.Rows.Count != 0)
                    {
                        conn.Open();
                        SqlParameter releaseFlowParam1 = cmd.Parameters.AddWithValue("@UserFormsTable", userFormsTable);
                        var reader = cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
            }
        }

    }
}
