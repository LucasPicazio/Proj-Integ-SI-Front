using PSI_FRONT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSI_FRONT.Services
{
    public interface IUserService
    {
        public Task<bool> GetLoginAuthenticationAsync(User user);
        public Task<bool> AddUserAsync(User newUser);
    }
}
