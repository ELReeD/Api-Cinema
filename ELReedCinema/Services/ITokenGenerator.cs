using ELReedCinema.Model.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ELReedCinema.Services
{
    public interface ITokenGenerator
    {
        public string CreateJwtToken(string userName, string id);
        public string CreateRefreshToken();
        
    }
}
