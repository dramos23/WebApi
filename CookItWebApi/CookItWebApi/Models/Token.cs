using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CookItWebApi.Models
{
    public class Token
    {
        public string _AccessToken { get; set; }
        public DateTime _ExpireDate { get; set; }

        public Token()
        {
        }

        public Token(string AccessToken, DateTime ExpireDate)
        {
            _AccessToken = AccessToken;
            _ExpireDate = ExpireDate;
        }
    }
}
