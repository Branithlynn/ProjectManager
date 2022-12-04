using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entity
{
    [Table("Tasks")]
    public class ProjectTask : BaseEntity
    {
        public int taskOwnerID { get; set; }
        public int parentID { get; set; }
        public string title { get; set; }
        public string description { get; set; }
    }
}
