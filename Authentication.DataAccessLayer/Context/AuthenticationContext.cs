using Authentication.DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Authentication.DataAccessLayer.Context
{
   public class AuthenticationContext :DbContext
    {

        public AuthenticationContext(DbContextOptions<AuthenticationContext> options): base (options)
        {

        }


        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
