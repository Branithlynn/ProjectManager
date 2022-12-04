using Common.DataBaseAccess;
using Common.Entity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace Common.Repository
{
    public class TaskRepository
    {
        public List<ProjectTask> getTask(int ownerID)
        {
            Context context = new Context();
            List < ProjectTask > list = new List<ProjectTask>();
            foreach (var item in context.Tasks)
            {
                if (item.parentID == ownerID)
                {
                    list.Add(item);
                }
            }
            return list;
        }
        public void DeleteTasksByOwnerID(int ownerID)
        {
            Context context = new Context();
            List< ProjectTask > list = getTask(ownerID);
            foreach (var item in list)
            {
                ProjectTask task = context.Tasks.Find(item.ID);
                foreach (var log in context.WorkLogs)
                {
                    if (log.TaskID == item.ID)
                    {
                        context.WorkLogs.Remove(log);
                    }
                }
                foreach (var item1 in context.TaskToUser)
                {
                    if (item1.TaskID == item.ID)
                    {
                        context.TaskToUser.Remove(item1);
                    }
                }
                context.Tasks.Remove(task);
            }
            context.SaveChanges();
        }
        public void addTask(ProjectTask task)
        {
            Context context = new Context();
            context.Tasks.Add(task);
            context.SaveChanges();
        }
        public void deleteTask(int id)
        {
            Context context= new Context();
            ProjectTask task = context.Tasks.Find(id);
            foreach (var item in context.WorkLogs)
            {
                if (item.TaskID == id)
                {
                    context.WorkLogs.Remove(item);
                }
            }
            foreach (var item in context.TaskToUser)
            {
                if (item.TaskID == task.ID)
                {
                    context.TaskToUser.Remove(item);
                }
            }
            context.Tasks.Remove(task); 
            context.SaveChanges();
        }
        public void updateTask(ProjectTask task)
        {
            Context context = new Context();
            ProjectTask item = new ProjectTask();

            item.ID = task.ID;
            item.parentID = task.parentID;
            item.title = task.title;
            item.description=task.description;

            context.Entry(item).State = EntityState.Modified;
            context.SaveChanges();
        }
        public int TasksCount(Expression<Func<ProjectTask, bool>> filter = null)
        {
            Context context = new Context();
            IQueryable<ProjectTask> query = context.Tasks;

            if (filter != null)
                query = query.Where(filter);

            return query.Count();
        }
        public List<ProjectTask> GetAll(Expression<Func<ProjectTask, bool>> filter = null, int page = 1, int pageSize = int.MaxValue)
        {
            Context context = new Context();
            IQueryable<ProjectTask> query = context.Tasks;
            if (filter != null)
                query = query.Where(filter);

            return query.OrderBy(x => x.ID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
