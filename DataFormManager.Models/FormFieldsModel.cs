using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFormManager.Models
{
    public class FormFieldsModel
    {
        public string Name
        {
            get;
            set;
        }
        public string Type
        {
            get;
            set;
        }
        public string Id
        {
            get;
            set;
        }
        public List<string> Values
        {
            get;
            set;
        }
    }
}
