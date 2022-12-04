using Common.Entity;
using Common.Service;
using Common.DataBaseAccess;
using System.Data.Entity;
using System.Linq.Expressions;
using System.Linq;
using System;
using System.Collections.Generic;

namespace Common.Repository
{
    public class ProjectRepository
    {
        public static Project GetById(int id)
        {
            Context context = new Context();
            Project project = context.Projects.Find(id);
            return project;
        }
        public void InsertProject(Project item)
        {
            Context context = new Context();
            Project project = new Project();

            project.ownerID = Authentication.LoggedUser.ID;
            project.title = item.title;
            project.description = item.description;

            context.Projects.Add(project);
            context.SaveChanges();
        }
        public void DeleteProjectByID(int id)
        {
            Context context = new Context();
            TaskRepository taskRepo= new TaskRepository();
            Project project = context.Projects.Find(id);
            taskRepo.DeleteTasksByOwnerID(project.ID);
            context.Projects.Remove(project);
            foreach (var item in context.ProjectToUser)
            {
                if (item.ProjectID == id)
                {
                    context.ProjectToUser.Remove(item);
                }
            }
            context.SaveChanges();
        }
        public void DeleteProjectsByOwnerID(int ownerID)
        {
            Context context = new Context();
            TaskRepository repository = new TaskRepository();
            foreach (var item in context.Projects)
            {
                if (item.ownerID == ownerID)
                {
                    context.Projects.Remove(item);
                    repository.DeleteTasksByOwnerID(item.ID);
                }
            }
            context.SaveChanges();
        }
        public void UpdateProject(Project item)
        {
            Context context = new Context();
            Project project = context.Projects.Find(item.ID);

            project.ID = item.ID;
            project.ownerID = Authentication.LoggedUser.ID;
            project.title = item.title;
            project.description = item.description;

            context.Entry(project).State = EntityState.Modified;
            context.SaveChanges();
        }
        public int ProjectsCount(Expression<Func<Project, bool>> filter = null)
        {
            Context context = new Context();
            IQueryable<Project> query = context.Projects;

            if (filter != null)
                query = query.Where(filter);

            return query.Count();
        }
        /*
         * 
        public int SpecificListCount(Expression<Func<Project, bool>> filter = null , List<Project> list )
        {
            Context context = new Context();
            IQueryable<Project> query = list.AsQueryable();

            if (filter != null)
                query = query.Where(filter);

            return query.Count();
        }
        */
        public List<Project> GetAll(Expression<Func<Project, bool>> filter = null, int page = 1, int pageSize = int.MaxValue)
        {
            Context context = new Context();
            IQueryable<Project> query = context.Projects;
            if (filter != null)
                query = query.Where(filter);

            return query.OrderBy(x => x.ID).Skip((page - 1) * pageSize).Take(pageSize).ToList();
        }
    }
}
