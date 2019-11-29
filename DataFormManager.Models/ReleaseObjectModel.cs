using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFormManager.Models
{
    public class ReleaseObjectModel

    {
        public int FormTypeId
        {
            get;
            set;
        }
        public String TaskName
        {
            get;
            set;
        }
        public String TaskDescription
        {
            get;
            set;
        }
        public String Status
        {
            get;
            set;
        }
        public String ApplicationName
        {
            get;
            set;
        }
        public String ProjectName
        {
            get;
            set;
        }
        public String ReleaseDate
        {
            get;
            set;
        }

    }

}
