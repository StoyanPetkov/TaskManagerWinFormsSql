using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMLibrary.Entities;
using TMLibrary.Repository;

namespace TaskManager.Service
{
    public static class AuthenticateService
    {
        public static User LoggedUser { get; set; }

        public static User AuthenticateUser(string username, string password)
        {
            UserRepository userRepo = new UserRepository();
            AuthenticateService.LoggedUser = userRepo.GetByUserNameAndPassword(username, password);
            return LoggedUser;
        }
    }
}
