using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFormManager.Models
{
    public class UserTokensObjectModel
    {
             public int UserTokenId { get; set; }
             public int UserId { get; set; }
             public int TokenId { get; set; }
           
            public UserTokensObjectModel() { }
            public UserTokensObjectModel(int UserTokenId, int UserId, int TokenId)
            {
                this.UserTokenId = UserTokenId;
                this.UserId = UserId;
                this.TokenId = TokenId;
            }
            public UserTokensObjectModel(int UserId, int TokenId)
            {
                this.UserId = UserId;
                this.TokenId = TokenId;
            }

    }
}
