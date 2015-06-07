using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMLibrary.Entities;

namespace TMLibrary.Repository
{
    public class TaskRepository
    {
        private TMContext db = null;

        public List<Tasks> GetAll()
        {
            using (db = new TMContext())
            {
                List<Tasks> taskList = new List<Tasks>();
                taskList = (from t in db.Tasks
                            select t).ToList();
                return taskList;
            }
        }

        public void Save(Tasks item)
        {
            if (item.TaskId > 0)
            {
                Update(item);
            }
            else
            {
                Insert(item);
            }
        }

        private void Insert(Tasks item)
        {
            using (db = new TMContext())
            {
                var dbTask = new Tasks();
                dbTask.Title = item.Title;
                dbTask.Description = item.Description;
                dbTask.EstimatedTime = item.EstimatedTime;
                dbTask.Createdon = DateTime.Now;
                dbTask.CreatedBy = item.CreatedBy;
                dbTask.Assignedto = item.Assignedto;
                dbTask.IsFinished = item.IsFinished;

                db.Tasks.Add(dbTask);
                db.SaveChanges();
            }
        }

        private void Update(Tasks item)
        {
            using (db = new TMContext())
            {
                db.Database.ExecuteSqlCommand(@"UPDATE Tasks" + " SET Title = {0}, Description = {1}, EstimatedTime = {2}, Assignedto = {3}, IsFinished = {4}" +
                    " WHERE TaskId = {5}", item.Title, item.Description, item.EstimatedTime, item.Assignedto, item.IsFinished, item.TaskId);
                db.SaveChanges();
            }
        }

        public void Delete(Tasks item)
        {
            using (db = new TMContext())
            {
                var dbTask = (from t in db.Tasks
                              where t.TaskId == item.TaskId
                              select t).First();
                db.Tasks.Remove(dbTask);
                db.SaveChanges();
            }
        }
    }
}
