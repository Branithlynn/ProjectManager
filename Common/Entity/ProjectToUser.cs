using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entity
{
    [Table("SharedProjects")]
    public class ProjectToUser : BaseEntity
    {
        public int UserID { get; set; }
        public int ProjectID { get; set; }
    }
}
