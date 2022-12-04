using Microsoft.AspNetCore.Mvc.Rendering;
using Common.Entity;
using System.ComponentModel.DataAnnotations;

namespace ProjectManager.ViewModel.WorkLogVM
{
    public class CreateVM
    {
        public List<SelectListItem> TaskList { get; set; }
        public int UserID { get; set; }
        [Display(Name = "Task: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public int TaskID { get; set; }
        [Display(Name = "Worked Hours: ")]
        [Required(ErrorMessage = "This field is Required!")]
        public int LoggedHours { get; set; }
        public DateTime Date { get; set; }
    }
}
