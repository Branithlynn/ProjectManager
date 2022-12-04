using Common.Entity;
using System.Data.Entity;

namespace Common.DataBaseAccess
{
    public class Context :DbContext
    {

        public DbSet<User> Users { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<ProjectTask> Tasks { get; set; }
        public DbSet<ProjectToUser> ProjectToUser { get; set; }
        public DbSet<TaskToUser> TaskToUser { get; set; }
        public DbSet<WorkLog> WorkLogs { get; set; }
        public DbSet<TaskComment> TaskComments { get; set; }

        public Context() : base("Server = localhost\\sqlexpress; Database=ProjectManager;Trusted_Connection=True;")
        {
            Users = this.Set<User>();
            Projects = this.Set<Project>();
            Tasks = this.Set<ProjectTask>();
            ProjectToUser = this.Set<ProjectToUser>();
            TaskToUser = this.Set<TaskToUser>();
            WorkLogs = this.Set<WorkLog>();
            TaskComments = this.Set<TaskComment>();
        }
    }
}
