using Common.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ProjectManager.ViewModel.TaskVM
{
    public class ShareVM
    {
        public List<User> UserList { get; set; }

        public List<User> SharedWith { get; set; }

        public string SelectedID { get; set; }
        public List<SelectListItem> ListOfUsers { get; set; }
    }
}
