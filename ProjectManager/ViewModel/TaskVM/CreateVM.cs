using Common.Entity;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.ViewModel.TaskVM
{
    public class CreateVM
    {
        [Display(Name = "Title: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string title { get; set; }
        [Display(Name = "Description: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string description { get; set; }

        public int taskOwnerID { get; set; }
        public List<User> UserList { get; set; }
        public List<ProjectToUser> SharedList { get; set; }
    }
}
