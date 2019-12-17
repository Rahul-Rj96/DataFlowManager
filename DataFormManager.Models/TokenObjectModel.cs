using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataFormManager.Models
{
    public class TokenObjectModel
    {

        public int TokenId { get;  set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpiresIn { get; set; }
        public string AuthorizationCode { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public string EmailId { get; set; }

        public TokenObjectModel() { }
        public TokenObjectModel( string AccessToken, string RefreshToken, DateTime ExpiresIn, string AuthorizationCode)
        {
            this.AccessToken = AccessToken;
            this.RefreshToken = RefreshToken;
            this.ExpiresIn = ExpiresIn;
            this.AuthorizationCode = AuthorizationCode;
        }
        public TokenObjectModel(int TokenId, string AccessToken, string RefreshToken, DateTime ExpiresIn, string AuthorizationCode)
        {
            this.TokenId = TokenId;
            this.AccessToken = AccessToken;
            this.RefreshToken = RefreshToken;
            this.ExpiresIn = ExpiresIn;
            this.AuthorizationCode = AuthorizationCode;
        }

        public TokenObjectModel(int TokenId, string AccessToken, string RefreshToken, DateTime ExpiresIn, string AuthorizationCode, int UserId, string Username, string EmailId)
        {
            this.TokenId = TokenId;
            this.AccessToken = AccessToken;
            this.RefreshToken = RefreshToken;
            this.ExpiresIn = ExpiresIn;
            this.AuthorizationCode = AuthorizationCode;
            this.UserId = UserId;
            this.Username = Username;
            this.EmailId = EmailId;
        }
    }
}

