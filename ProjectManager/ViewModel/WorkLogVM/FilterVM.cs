using Common.Entity;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;

namespace ProjectManager.ViewModel.WorkLogVM
{
    public class FilterVM
    {
        public string taskTitle { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime From { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime To { get; set; }
    }
}
