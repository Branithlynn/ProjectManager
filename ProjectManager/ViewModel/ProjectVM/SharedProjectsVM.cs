using Microsoft.AspNetCore.Mvc.Rendering;
using Common.Entity;
using ProjectManager.ViewModel.Shareed;
using System.ComponentModel;

namespace ProjectManager.ViewModel.ProjectVM
{
    public class SharedProjectsVM
    {
        public List<Project> Shared { get; set; }
    }
}
