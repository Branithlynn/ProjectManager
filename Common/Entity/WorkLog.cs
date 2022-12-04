using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entity
{
    [Table("WorkLogs")]
    public class WorkLog : BaseEntity
    {
        public int UserID { get; set; }
        public int TaskID { get; set; }
        public int LoggedHours { get; set; }
        public DateTime Date { get; set; }
    }
}
