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
    public class FormTypeHelper
    {
        public static FormTypeModel  GetFormFields(string formName)
        {
            FormTypeModel ModelDef = null;
            string dataString = null;
            string connString = ConfigurationManager.ConnectionStrings["ReleaseFlowDBConnectionString"].ConnectionString;       //read from config  
            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                {
                    String spGetFormFields = @"[dbo].[Proc_FormType_GetFormFields]";
                    SqlCommand cmd = new SqlCommand(spGetFormFields, conn);
                    conn.Open();
                    cmd.CommandType = CommandType.StoredProcedure;
                    SqlParameter param = cmd.Parameters.Add("@FormName", SqlDbType.VarChar);
                    param.Value = formName;
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows && dr.Read())
                    {
                        dataString = Convert.ToString(dr["FormConfig"]);
                        ModelDef = JsonConvert.DeserializeObject<FormTypeModel>(dataString);
                       
                    }
                    dr.Close();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.Message);
            }
            return ModelDef;
        }
    }
}
