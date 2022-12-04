using Common.Entity;
using ProjectManager.ViewModel.Shareed;

namespace ProjectManager.ViewModel.UserVM
{
    public class IndexVM
    {
        public List<User> Items { get; set; }
        public PagerVM Pager { get; set; }
        public FilterVM Filter { get; set; }
    }
}
