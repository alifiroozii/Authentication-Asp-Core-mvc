using Authentication.DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authentication.Core.Interfaces
{
    public interface IUser
    {
        bool IsMobileNumberExist(string mobileNumber);

        int AddUser(User user);

        bool ActiveUser(string activeCode);


        User LoginUser(string mobileNumber, string password);

        User ForgetPassword(string mobileNumber);


        bool ResetPassword(string activeCode, string password);

        bool checkUserRole(string roleName, string moileNumber);

        string GetRoleName(int id);

    }
}
