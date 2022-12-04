using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Common.DataBaseAccess;
using Common.Entity;
using Common.Repository;
using Common.Service;
using ProjectManager.ViewModel.ProjectVM;
using ProjectManager.ViewModel.Shareed;

namespace ProjectManager.Controllers
{
    public class ProjectController : Controller
    {
        //------------------PROJECTS SCREEN---------------------------//
        public IActionResult ProjectsList(DisplayVM model)
        {
            Authentication.LoggedProject = null;
            Authentication.LoggedTask = null;
            Context context = new Context();
            ProjectRepository repo = new ProjectRepository();
            model.Pager ??= new PagerVM();
            model.Filter ??= new FilterVM();

            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0 ? 10 : model.Pager.ItemsPerPage;

            model.Pager.Page = model.Pager.Page <= 0 ? 1 : model.Pager.Page;

            model.Filter.UserID = Authentication.LoggedUser.ID;
            var filter = model.Filter.GetFilter();
            model.ProjectsList = repo.GetAll(filter, model.Pager.Page, model.Pager.ItemsPerPage);
            model.Pager.PagesCount = (int)Math.Ceiling(repo.ProjectsCount(filter) / (double)model.Pager.ItemsPerPage);

            

            if (Authentication.LoggedUser.ID == 0)
            {
                foreach (var item in context.Users)
                {
                    if (item.username == Authentication.LoggedUser.username && item.password == Authentication.LoggedUser.password)
                    {
                        Authentication.LoggedUser = item;
                        Authentication.LoggedProject = null;

                        return View(model);
                    }
                }
            }
            Authentication.LoggedProject = null;
            Authentication.LoggedTask = null;



           
            return View(model);
        }
        public IActionResult SharedProjects(SharedProjectsVM model)
        {
            Authentication.LoggedProject = null;
            Authentication.LoggedTask = null;
            //-----------pager i filter------------------------------//
            Context context = new Context();

            var listOfProjects = context.Projects.ToList();
            var list = new List<Project>();
            foreach (var item in context.ProjectToUser)
            {
                if (item.UserID == Authentication.LoggedUser.ID)
                {
                    foreach (var project in listOfProjects)
                    {
                        if (project.ID == item.ProjectID)
                        {
                            list.Add(project);
                        }
                    }
                }
            }

            model.Shared = list;
            return View(model);
        }
        //------------------------------------------------------------//
        //------------------INSERT PROJECT----------------------------//
        [HttpGet]
        public IActionResult CreateProject()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateProject(CreateVM item)
        {
            ProjectRepository projectRepo = new ProjectRepository();
            Project project = new Project();

            project.ownerID = Authentication.LoggedUser.ID;
            project.title = item.Title;
            project.description = item.Description;

            projectRepo.InsertProject(project);
            return RedirectToAction("ProjectsList","Project");
        }
        //------------------------------------------------------------//
        //------------------DELETING PROJECT METHOD-------------------//
        public IActionResult DeleteProject(int id)
        {
            ProjectRepository projectRepo = new ProjectRepository();
            projectRepo.DeleteProjectByID(id);
            return RedirectToAction("ProjectsList" , "Project");
        }
        //------------------------------------------------------------//
        //------------------UPDATEING PROJECT METHOD------------------//
        [HttpGet]
        public IActionResult UpdateProject(int id)
        {
            Context context = new Context();
            Project project = context.Projects.Find(id);
            EditVM editVM = new EditVM();

            editVM.ID = project.ID;
            editVM.OwnerID = project.ownerID;
            editVM.Title = project.title;
            editVM.Description = project.description;

            return View(editVM);
        }
        [HttpPost]
        public IActionResult UpdateProject(EditVM item)
        {
            ProjectRepository projectRepo = new ProjectRepository();
            Project project = new Project();

            project.ID = item.ID;
            project.title=item.Title;
            project.description=item.Description;
            project.ownerID = item.OwnerID;

            projectRepo.UpdateProject(project);

            return RedirectToAction("ProjectsList", "Project");
        }
        //------------------------------------------------------------//
        //------------------REDIRECT PROJECT METHOD-------------------//
        public IActionResult Redirect(int id)
        {
            Context context=new Context();
            Authentication.LoggedProject = context.Projects.Find(id);
            return RedirectToAction("TaskList", "Task");
        }
        //------------------------------------------------------------//
        //------------------SHARE PROJECT METHOD----------------------//
        [HttpGet]
        public IActionResult Share(int id)
        {
            Context context = new Context();
            Project project =context.Projects.Find(id);
            ShareVM shareVM = new ShareVM();
            UsersRepository repo = new UsersRepository();

            shareVM.ID = project.ID;
            shareVM.ownerID = project.ownerID;
            shareVM.title = project.title;
            shareVM.description = project.description;
            shareVM.UsersList = repo.GetAll(x => x.ID != Authentication.LoggedUser.ID);
            shareVM.SharedList = context.ProjectToUser.ToList();

            var selectList = (from user in context.Users
                              select new SelectListItem()
                              {
                                  Text = user.username,
                                  Value = user.ID.ToString()
                              }).ToList();
            shareVM.ListOfUsers = selectList;
            return View(shareVM);
        }
        [HttpPost]
        public IActionResult Share(string SelectedID, int projectID)
        {
            Context context = new Context();
            ProjectToUser userToProject = new ProjectToUser();

            userToProject.UserID = int.Parse(SelectedID);
            userToProject.ProjectID = projectID;
            foreach (var item in context.ProjectToUser)
            {
                if (item.ProjectID == userToProject.ProjectID && item.UserID == userToProject.UserID)
                {
                    return RedirectToAction("ProjectsList", "Project");
                }
            }
            context.ProjectToUser.Add(userToProject);
            context.SaveChanges();
            return RedirectToAction("ProjectsList","Project");
        }
        //------------------------------------------------------------//
        //------------------REMOVE SHARE METHOD-----------------------//
        public IActionResult RevomoveShare(int id , int userID)
        {
            Context context = new Context();
            foreach (var item in context.ProjectToUser)
            {
                if (item.ProjectID == id && item.UserID == userID)
                {
                    context.ProjectToUser.Remove(item);
                }
            }
            context.SaveChanges();
            return RedirectToAction("ProjectsList", "Project");
        }
        //------------------------------------------------------------//
    }
}
