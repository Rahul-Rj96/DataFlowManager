using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFormManager.Models
{
    public class UserFormPermissionObjectModel
    {
        public string Username { get; set; }
        public string RoleName { get; set; }
        public string FormName { get; set; }
        public string Permission { get; set; }


        public UserFormPermissionObjectModel(string Username, string RoleName, string FormName, string Permission)
        {
            this.Username = Username;
            this.RoleName = RoleName;
            this.FormName = FormName;
            this.Permission = Permission;
        }
    }
}
