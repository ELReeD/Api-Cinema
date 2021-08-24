using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELReedCinema.Areas.Users.Models
{
    public class UserTokens
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
