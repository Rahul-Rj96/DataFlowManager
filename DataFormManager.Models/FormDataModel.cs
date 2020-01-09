using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFormManager.Models
{
    public class FormDataModel
    {
        public int? FormId
        {
            get;
            set;
        }
        public string FormType
        {
            get;
            set;
        }
        public string FormName
        {
            get;
            set;
        }
        public List<DataValueModel> FormData
        {
            get;
            set;
        }
        public DatesModel EffectiveDate
        {
            get;
            set;
        }

    }
}
