using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMLibrary.Entities;

namespace TMLibrary.Repository
{
    public class UserRepository
    {
        private TMContext db = null;

        public User GetByUserNameAndPassword(string username, string password)
        {
            db = new TMContext();

            using (db)
            {
                User user = (from u in db.Users
                             where u.UserName.Equals(username) &&
                             u.Password.Equals(password)
                             select u).FirstOrDefault();
                return user;
            }
        }

        public List<User> GetAll()
        {
            using (db = new TMContext())
            {
                List<User> resultList = new List<User>();
                resultList = (from u in db.Users
                              select u).ToList();
                return resultList;
            }
        }

        private void Insert(User item)
        {
            using (db = new TMContext())
            {
                var User = new User();
                User.UserName = item.UserName;
                User.Password = item.Password;
                User.FirstName = item.FirstName;
                User.LastName = item.LastName;

                db.Users.Add(User);
                db.SaveChanges();
            }
        }

        private void Update(User item)
        {
            using (var db = new TMContext())
            {
                db.Database.ExecuteSqlCommand("UPDATE Users" +
                      " SET UserName={0},Password={1},FirstName={2},LastName={3} " +
                       "WHERE UserId={4}", item.UserName, item.Password, item.FirstName,
                       item.LastName, item.UserId);
                db.SaveChanges();
            }
        }

        public void Save(User item)
        {
            if (item.UserId > 0)
            {
                Update(item);
            }
            else
            {
                Insert(item);
            }
        }

        public void Delete(User item)
        {
            using (var db = new TMContext())
            {
                var usrdb = (from u in db.Users
                             where u.UserId == item.UserId
                             select u).First();
                db.Users.Remove(usrdb);
                db.SaveChanges();
            }
        }
    }
}
