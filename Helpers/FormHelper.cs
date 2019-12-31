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
        public static void AddFormData(FormDataModel formData,int userId)
        {
            int formId = new int();
            int isSelfAssigned = new int();
            string formTypeName = formData.FormType;
            string connString = ConfigurationManager.ConnectionStrings["ReleaseFlowDBConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    String spAddFormData1 = @"dbo.[Proc_Form_AddForm]";
                    String spAddFormData2 = @"dbo.[Proc_Form_AddFormDataWithId]";
                    SqlCommand cmd1 = new SqlCommand(spAddFormData1, conn);
                    SqlCommand cmd2 = new SqlCommand(spAddFormData2, conn);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd2.CommandType = CommandType.StoredProcedure;
                    conn.Open();                   
                    SqlParameter formDataParam1 = cmd1.Parameters.AddWithValue("@FormTypeName", formTypeName);
                    SqlParameter formDataParam2 = cmd1.Parameters.AddWithValue("@CreatedBy", userId);
                    var reader1 = cmd1.ExecuteReader();
                    while (reader1.Read())
                    {
                        formId = Convert.ToInt32(reader1["FormID"]);
                    }
                    reader1.NextResult();
                    while (reader1.Read())
                    {
                        isSelfAssigned = Convert.ToInt32(reader1["IsSelfAssigned"]);
                    }
                    reader1.Close();
                    formData.FormId = formId;
                    string jsonFormData = JsonConvert.SerializeObject(formData);
                    SqlParameter formDataParam3 = cmd2.Parameters.AddWithValue("@FormData", jsonFormData);
                    SqlParameter formDataParam4 = cmd2.Parameters.AddWithValue("@FormId", formId);
                    var reader2 = cmd2.ExecuteNonQuery();
                    conn.Close();
                    if (isSelfAssigned == 1)
                    {
                        UserFormObjectModel userFormsData = new UserFormObjectModel() {
                            FormId = formId,
                            UserId = userId};
                        UserFormsHelper.AddUserFormsData(userFormsData);

                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
            }
        }

        public static void UpdateFormData(FormDataModel formData)
        {
            string jsonFormData = JsonConvert.SerializeObject(formData);
            string connString = ConfigurationManager.ConnectionStrings["ReleaseFlowDBConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    String spAddFormData1 = @"dbo.[Proc_Form_UpdateFormData]";
                    SqlCommand cmd1 = new SqlCommand(spAddFormData1, conn);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlParameter formDataParam1 = cmd1.Parameters.AddWithValue("@FormData", jsonFormData);
                    SqlParameter formDataParam2 = cmd1.Parameters.AddWithValue("@FormId", formData.FormId);
                    var reader1 = cmd1.ExecuteNonQuery();
                    conn.Close();
                    }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
            }

        }

        public static void DeleteFormData(int formId)
        {
            string connString = ConfigurationManager.ConnectionStrings["ReleaseFlowDBConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    String spAddFormData1 = @"dbo.[Proc_Form_DeleteFormData]";
                    SqlCommand cmd1 = new SqlCommand(spAddFormData1, conn);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlParameter formDataParam1 = cmd1.Parameters.AddWithValue("@FormId", formId);
                    var reader1 = cmd1.ExecuteNonQuery();
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
