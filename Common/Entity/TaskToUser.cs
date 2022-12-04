using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entity
{
    [Table("SharedTasks")]
    public class TaskToUser : BaseEntity
    {
        public int UserID { get; set; }
        public int ProjectID { get; set; }
        public int TaskID { get; set; }
    }
}
