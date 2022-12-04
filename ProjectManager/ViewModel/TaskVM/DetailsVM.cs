using Microsoft.AspNetCore.Mvc.Rendering;
using Common.Entity;

namespace ProjectManager.ViewModel.TaskVM
{
    public class DetailsVM
    {
        public List<User> Owner { get; set; }
        public List<WorkLog> Logs { get; set; }
        public FilterVM Filter { get; set; }
        public List<SelectListItem> ListOfUsers { get; set; }
        public TaskComment Comment { get; set; }
        public List<TaskComment> CommentsList { get; set; }
    }
}
