using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entity
{
    [Table("Projects")]
    public class Project : BaseEntity
    {
        public int ownerID { get; set; }
        public string title { get; set; }
        public string description { get; set; }
    }
}
