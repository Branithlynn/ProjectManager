using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Common.Entity
{
    [Table("Users")]
    public class User : BaseEntity
    {
        public string username { get; set; }
        public string password { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public bool IsAdmin { get; set; }
    }
}
