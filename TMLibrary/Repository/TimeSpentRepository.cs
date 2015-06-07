using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMLibrary.Entities;

namespace TMLibrary.Repository
{
    public class TimeSpentRepository
    {
        private TMContext db = new TMContext();

        public void Insert(Tasks item, TimeSpent tSpent)
        {
            using (db = new TMContext())
            {
                var dbTspent = new TimeSpent();
                dbTspent.Userid = tSpent.Userid;
                dbTspent.Taskid = tSpent.Taskid;
                dbTspent.LogTime = tSpent.LogTime;
                dbTspent.Date = DateTime.Now;
                dbTspent.IsFinished = item.IsFinished;

                db.tSpents.Add(dbTspent);
                db.SaveChanges();
            }
        }

        public List<TimeSpent> GetAll(int id)
        {
            using (db = new TMContext())
            {
                List<TimeSpent> tSpentList = (from t in db.tSpents
                                              where t.Taskid == id
                                              select t).ToList();
                return tSpentList;
            }
        }
    }
}
