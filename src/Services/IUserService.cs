﻿using Microsoft.AspNetCore.Components.Authorization;
using PSI_FRONT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PSI_FRONT.Services
{
    public interface IUserService
    {
        public Task SetLoginAuthenticationAsync(string token);
        public Task<bool> AddUserAsync(User newUser);
        Task Logout();
        Task<bool> Login(User user);
    }
}
