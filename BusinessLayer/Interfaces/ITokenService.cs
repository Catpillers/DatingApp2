using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Entities;

namespace BusinessLayer.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
