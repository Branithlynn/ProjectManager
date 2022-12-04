using Common.Entity;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.ViewModel.WorkLogVM
{
    public class CreateSpecificVM
    {
        public int TaskID { get; set; }
        [Display(Name = "Worked Hours: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public int LoggedHours { get; set; }
        public DateTime Date { get; set; }
        public int UserID { get; set; }
    }
}
