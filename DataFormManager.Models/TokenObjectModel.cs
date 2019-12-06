using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFormManager.Models
{
    public class TokenObjectModel
    {

         
        public string access_token
        {
            get;
            set;
        }
        public int expires_in
        {
            get;
            set;
        }
    
        public String scope
        {
            get;
            set;
        }
        public String token_type
        {
            get;
            set;
        }
        public String id_token
        {
            get;
            set;
        }
      
    }
}
