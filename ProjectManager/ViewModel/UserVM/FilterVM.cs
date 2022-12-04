using Common.Entity;
using System.ComponentModel;
using System.Linq.Expressions;

namespace ProjectManager.ViewModel.UserVM
{
    public class FilterVM
    {
        [DisplayName("Username: ")]
        public string Username { get; set; }
        [DisplayName("First Name: ")]
        public string FirstName { get; set; }
        [DisplayName("Last Name: ")]
        public string LastName { get; set; }

        public Expression<Func<User,bool>> GetFilter()
        {
            return i => (string.IsNullOrEmpty(Username) || i.username.Contains(Username)) &&
                        (string.IsNullOrEmpty(FirstName) || i.firstName.Contains(FirstName)) &&
                        (string.IsNullOrEmpty(LastName) || i.lastName.Contains(LastName));
        }
    }
}
