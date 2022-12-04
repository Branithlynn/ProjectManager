using System.ComponentModel.DataAnnotations;

namespace ProjectManager.ViewModel.ProjectVM
{
    public class EditVM
    {
        public int ID { get; set; }
        public int OwnerID { get; set; }
        [Display(Name = "Title: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string Title { get; set; }
        [Display(Name = "Desciption: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string Description { get; set; }
    }
}
