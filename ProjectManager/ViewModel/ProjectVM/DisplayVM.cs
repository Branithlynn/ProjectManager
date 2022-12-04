using Microsoft.AspNetCore.Mvc.Rendering;
using Common.Entity;
using ProjectManager.ViewModel.Shareed;
using System.ComponentModel;

namespace ProjectManager.ViewModel.ProjectVM
{
    public class DisplayVM
    {
        public List<Project> ProjectsList { get; set; }
        public PagerVM Pager { get; set; }
        public FilterVM Filter { get; set; }
    }
}
