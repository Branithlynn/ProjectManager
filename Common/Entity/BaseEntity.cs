using System.ComponentModel.DataAnnotations;

namespace Common.Entity
{
    public class BaseEntity
    {
        [Key]
        public int ID { get; set; }
    }
}
