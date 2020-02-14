using DataFormManager.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Helpers
{
    public class FormHelper
    {
        public static void AddFormData(FormDataModel formData, int userId)
        {
            int formId = new int();
            int isSelfAssigned = new int();
            string formTypeName = formData.FormType;
            string connString = ConfigurationManager.ConnectionStrings["DFMDBConnectionString"].ConnectionString;
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
                        UserFormObjectModel userFormsData = new UserFormObjectModel()
                        {
                            FormId = formId,
                            UserId = userId
                        };
                        List<UserFormObjectModel> userFormsDatas = new List<UserFormObjectModel>();
                        userFormsDatas.Add(userFormsData);
                        UserFormsHelper.AddUserFormsData(userFormsDatas);

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
            string connString = ConfigurationManager.ConnectionStrings["DFMDBConnectionString"].ConnectionString;
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
            string connString = ConfigurationManager.ConnectionStrings["DFMDBConnectionString"].ConnectionString;
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

        public static string GetFormTypeNameUsingId(int formId)
        {
            string formType = null;
            string connString = ConfigurationManager.ConnectionStrings["DFMDBConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    String spAddFormData1 = @"dbo.[Proc_Form_GetFormTypeName]";
                    SqlCommand cmd1 = new SqlCommand(spAddFormData1, conn);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlParameter formDataParam1 = cmd1.Parameters.AddWithValue("@FormId", formId);
                    var reader1 = cmd1.ExecuteReader();
                    while (reader1.Read())
                    {
                        formType = Convert.ToString(reader1["FormName"]);
                    }
                    conn.Close();
                    return formType;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
                return formType;
            }
        }

        public static List<FormDataModel> GetFormsToAssign()
        {
            List<FormDataModel> formsToAssign = new List<FormDataModel>();
            string dataString = null;
            string connString = ConfigurationManager.ConnectionStrings["DFMDBConnectionString"].ConnectionString;       //read from config  
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    String spGetFormsToAssign = @"[dbo].[Proc_Form_GetFormsToAssign]";
                    SqlCommand cmd = new SqlCommand(spGetFormsToAssign, conn);
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.HasRows && dr.Read())
                    {
                        dataString = Convert.ToString(dr["FormData"]);
                        FormDataModel data = JsonConvert.DeserializeObject<FormDataModel>(dataString);
                        formsToAssign.Add(data);
                    }
                    dr.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
            }
            return formsToAssign;
        }

        public static List<UserIdNamemodel> GetUsersToAssign()
        {
            List<UserIdNamemodel> users = new List<UserIdNamemodel>();

            string connString = ConfigurationManager.ConnectionStrings["DFMDBConnectionString"].ConnectionString;       //read from config  
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    String spGetFormsToAssign = @"[dbo].[Proc_RCKRUser_GetUserToAssignForm]";
                    SqlCommand cmd = new SqlCommand(spGetFormsToAssign, conn);
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlDataReader dr = cmd.ExecuteReader();
                    while (dr.HasRows && dr.Read())
                    {
                        UserIdNamemodel user = new UserIdNamemodel();
                        user.UserId = Convert.ToInt32(dr["UserId"]);
                        user.Username = Convert.ToString(dr["Username"]);
                        users.Add(user);
                    }
                    dr.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
            }
            return users;
        }

        public static int InsertFile(HttpFileCollection file)
        {
            int fileId = new int();
            string connString = ConfigurationManager.ConnectionStrings["DFMDBConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    String spAddFormData = @"dbo.[Proc_Files_InsertFile]";
                    SqlCommand cmd = new SqlCommand(spAddFormData, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlParameter formDataParam1 = cmd.Parameters.AddWithValue("@FileRecord", file[0].InputStream);
                    SqlParameter formDataParam2 = cmd.Parameters.AddWithValue("@FileType", file[0].ContentType);
                    SqlParameter formDataParam3 = cmd.Parameters.AddWithValue("@FileFullName", file[0].FileName);
                    SqlParameter formDataParam4 = cmd.Parameters.AddWithValue("@FileFormType", file.Keys[0]);
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        fileId = Convert.ToInt32(reader["FileId"]);
                    }
                    reader.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
            }
            return fileId;
        }


        public static void EditFile(HttpFileCollection file, int fileId)
        {
            string connString = ConfigurationManager.ConnectionStrings["DFMDBConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    String spAddFormData = @"dbo.[Proc_Files_EditFile]";
                    SqlCommand cmd = new SqlCommand(spAddFormData, conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlParameter formDataParam1 = cmd.Parameters.AddWithValue("@FileRecord", file[0].InputStream);
                    SqlParameter formDataParam2 = cmd.Parameters.AddWithValue("@FileType", file[0].ContentType);
                    SqlParameter formDataParam3 = cmd.Parameters.AddWithValue("@FileFullName", file[0].FileName);
                    SqlParameter formDataParam4 = cmd.Parameters.AddWithValue("@FileFormType", file.Keys[0]);
                    SqlParameter formDataParam5 = cmd.Parameters.AddWithValue("@FileId", fileId);
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
            }
        }

        public static fileObj GetFile(string fileId)
        {
            fileObj file = new fileObj();
            string connString = ConfigurationManager.ConnectionStrings["DFMDBConnectionString"].ConnectionString;
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    String spAddFormData1 = @"dbo.[Proc_Files_GetFile]";
                    SqlCommand cmd1 = new SqlCommand(spAddFormData1, conn);
                    cmd1.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    SqlParameter formDataParam1 = cmd1.Parameters.AddWithValue("@FileId", Int32.Parse(fileId));
                    var reader1 = cmd1.ExecuteReader();
                    while (reader1.Read())
                    {
                       file.fileType = Convert.ToString(reader1["FileType"]);
                       file.fileRecord = (byte[])reader1["FileRecord"];
                        file.fileName = Convert.ToString(reader1["FileFullName"]);
                       
                    }
                    conn.Close();
                    return file;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
                return null;
            }
        }
    }
}