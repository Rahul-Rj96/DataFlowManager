using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFormManager.Models
{
    public class FormTypeModel
    {
        public string FormType
        {
            get;
            set;
        }
        public List<FormFieldsModel> FormFields
        {
            get;
            set;
        }
    }
}
