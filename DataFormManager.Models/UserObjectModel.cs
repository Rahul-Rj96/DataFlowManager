﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFormManager.Models
{
    public class UserObjectModel
    {

        public int UserId { get; private set; }
        public string Username { get; set; }
        public string EmailId { get; set; }
        public string Sub { get; set; }

        public UserObjectModel(int id, string username,string emailId, string sub)
        {
            this.UserId = id;
            this.Username = username;
            this.EmailId = emailId;
            this.Sub = sub;
        }
    }
}
