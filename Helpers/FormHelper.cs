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
    public class FormHelper
    {
        public static int AddFormData(FormDataModel formData)
        {
            int formId = new int();
            string formTypeName = formData.FormType;
            string jsonFormData = JsonConvert.SerializeObject(formData);
            string connString = ConfigurationManager.ConnectionStrings["ReleaseFlowDBConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    String spAddFormData = @"dbo.[Proc_Form_AddFormData]";
                    SqlCommand cmd = new SqlCommand(spAddFormData, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlParameter formDataParam1 = cmd.Parameters.AddWithValue("@FormData", jsonFormData);
                    SqlParameter formDataParam2 = cmd.Parameters.AddWithValue("@FormTypeName", formTypeName);
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
