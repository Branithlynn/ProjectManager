using Microsoft.AspNetCore.Mvc.Rendering;
using Common.Entity;
using ProjectManager.ViewModel.Shareed;
using System.ComponentModel;

namespace ProjectManager.ViewModel.TaskVM
{
    public class DisplayVM
    {
        public List<User> usersList { get; set; }
        public List<TaskToUser> SharedTaskList { get; set; }
        public List<ProjectTask> TaskList { get; set; }
        public PagerVM Pager { get; set; }
        public User Owner { get; set; }


        [DisplayName("User")]
        public int SelectedID { get; set; }
        public List<SelectListItem> ListOfUsers { get; set; }
    }
}
