using Common.Entity;
using ProjectManager.ViewModel.Shareed;

namespace ProjectManager.ViewModel.WorkLogVM
{
    public class DisplayVM
    {
        public List<WorkLog> LogsList { get; set; }
        public List<ProjectTask> TasksList { get; set; }
        public PagerVM Pager { get; set; }
        public FilterVM Filter { get; set; }
    }
}
