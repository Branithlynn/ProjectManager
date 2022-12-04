using Common.Entity;
using System.Linq.Expressions;

namespace ProjectManager.ViewModel.ProjectVM
{
    public class FilterVM
    {
        public int UserID { get; set; }
        public string title { get; set; }
        public string description { get; set; }

        public Expression<Func<Project,bool>> GetFilter()
        {
            return i => (string.IsNullOrEmpty(title) || i.title.Contains(title)) &&
                        (string.IsNullOrEmpty(description) || i.description.Contains(description));
        }
    }
}
