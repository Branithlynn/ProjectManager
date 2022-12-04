using System.ComponentModel.DataAnnotations;

namespace ProjectManager.ViewModel.TaskVM
{
    public class FilterVM
    {
        public int UserID { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime From { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime To { get; set; }
    }
}
