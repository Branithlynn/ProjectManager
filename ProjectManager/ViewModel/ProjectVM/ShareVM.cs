using Common.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.ViewModel.ProjectVM
{
    public class ShareVM
    {
        [Display(Name = "SharedID: ")]
        public int ID { get; set; }
        public int ownerID { get; set; }
        [Display(Name = "Title: ")]
        public string title { get; set; }
        [Display(Name = "Description: ")]
        public string description { get; set; }
        public List<User> UsersList { get; set; }
        public List<ProjectToUser> SharedList { get; set; }
        public string SelectedID { get; set; }
        public List<SelectListItem> ListOfUsers { get; set; }
    }
}
