using System.ComponentModel.DataAnnotations;

namespace ProjectManager.ViewModel.UserVM
{
    public class EditVM
    {
        public int ID { get; set; }
        [Display(Name = "Username: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string Username { get; set; }
        [Display(Name = "Password: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string Password { get; set; }
        [Display(Name = "First Name: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string FirstName { get; set; }
        [Display(Name = "Last Name: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string LastName { get; set; }
        public bool IsAdmin { get; set; }
    }
}
