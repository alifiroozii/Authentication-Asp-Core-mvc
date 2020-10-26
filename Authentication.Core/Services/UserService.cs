using Authentication.Core.Interfaces;
using Authentication.DataAccessLayer.Context;
using Authentication.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Authentication.Core.Classes;
using Microsoft.EntityFrameworkCore;

namespace Authentication.Core.Services
{
    public class UserService : IUser
    {

        AuthenticationContext _Context;

        public UserService(AuthenticationContext context)
        {
            _Context = context;
        }

        public bool ActiveUser(string activeCode)
        {
            var user = _Context.Users.FirstOrDefault(u => u.IsActive == false && u.Code == activeCode);
            if (user!=null)
            {
                user.Code = CodeGenerators.ActiveCode();
                user.IsActive = true;

                _Context.SaveChanges();
                return true;

            }
            else
            {
                return false;
            }
        }

        public int AddUser(User user)
        {
            _Context.Users.Add(user);
            _Context.SaveChanges();
            return user.Id;

        }

        public bool checkUserRole(string roleName, string moileNumber)
        {
            return _Context.Users.Include(u => u.Role).Any(u => u.Mobile == moileNumber && u.Role.Name==roleName );
        }

        public User ForgetPassword(string mobileNumber)
        {

            return _Context.Users.FirstOrDefault(u => u.Mobile == mobileNumber);


        }

        public string GetRoleName(int id)
        {
            var role = _Context.Roles.Find(id);
            return role.Name;
        }

        public bool IsMobileNumberExist(string mobileNumber)
        {
            return _Context.Users.Any(u => u.Mobile == mobileNumber);
        }

        public User LoginUser(string mobileNumber, string password)
        {
            string hashPassword = HashGeneretors.EncodingPassWithMd5(password);
            return _Context.Users.FirstOrDefault(u => u.Mobile == mobileNumber && u.Password == hashPassword);


        }

        public bool ResetPassword(string activeCode, string password)
        {
            var user = _Context.Users.FirstOrDefault(u => u.Code == activeCode && u.IsActive == true);

            if (user!=null)
            {
                string HashPassword = HashGeneretors.EncodingPassWithMd5(password);
                user.Password = HashPassword;
                user.Code= CodeGenerators.ActiveCode();

                _Context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
