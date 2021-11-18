﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using RailwayApp.Data;
using RailwayApp.Models;

namespace RailwayApp.Services
{
    public interface IUserService
    {
        IEnumerable<User> Get();
        bool Check(string name, string email, string login);
       Task<User>GetAsync(Guid id);
       Task<User>GetAsync(string email);
        Task CreateAsync(User user);
        Task Update(User user);
        Task Delete(Guid id);
        ClaimsIdentity Authenticate(User user);
    }

    public class UserService : IUserService
    {
        private readonly AppDataContext _appDataContext;

        public UserService(AppDataContext appDataContext)
        {
            _appDataContext = appDataContext;
        }

        public IEnumerable<User> Get()
        {
          return _appDataContext.Users;
        }

        public bool Check(string name, string email, string passportNumber)
        {
            if (_appDataContext.Users.FirstOrDefault(u => u.Name == name) != null) return true;
            if (_appDataContext.Users.FirstOrDefault(u => u.EmailUser == email) != null) return true;
            if (_appDataContext.Users.FirstOrDefault(u => u.PassportNumber == passportNumber) != null) return true;
            return false;
        }

        public async Task<User> GetAsync(Guid id)
        {
           return await _appDataContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<User> GetAsync(string email)
        {
            return await _appDataContext.Users.FirstOrDefaultAsync(u => u.EmailUser == email);
        }

        public async Task CreateAsync(User user)
        {
            user.Role = _appDataContext.Roles.FirstOrDefaultAsync(n => n.RoleName == "Common_User").Result;
            await _appDataContext.AddAsync(user);
            await _appDataContext.SaveChangesAsync();
        }

        public async Task Update(User user)
        {
             _appDataContext.Update(user);
            await _appDataContext.SaveChangesAsync();
        }

        public async Task Delete(Guid id)
        {
            _appDataContext.Remove(_appDataContext.Users.FirstOrDefault(u => u.Id == id));
           await _appDataContext.SaveChangesAsync();
        }

        public ClaimsIdentity Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.EmailUser),
                new Claim("NameUser", user.Name),
                new Claim("Role",_appDataContext.Roles.FirstOrDefault(r=>r.RoleId==user.RoleId).RoleName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            return id;
        }
    }
}
