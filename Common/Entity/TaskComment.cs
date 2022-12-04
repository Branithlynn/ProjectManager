using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entity
{
    [Table("TaskComments")]
    public class TaskComment : BaseEntity
    {
        public int TaskID { get; set; }
        public int UserID { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; }
    }
}
