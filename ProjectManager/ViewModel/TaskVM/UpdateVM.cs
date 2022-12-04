using System.ComponentModel.DataAnnotations;

namespace ProjectManager.ViewModel.TaskVM
{
    public class UpdateVM
    {
        public int ID { get; set; }
        public int parentID { get; set; }
        [Display(Name = "Title: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string title { get; set; }
        [Display(Name = "Description: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public string description { get; set; }
    }
}
