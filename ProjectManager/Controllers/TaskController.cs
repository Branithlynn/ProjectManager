using Microsoft.AspNetCore.Mvc;
using Common.Repository;
using Common.Service;
using ProjectManager.ViewModel.TaskVM;
using ProjectManager.ViewModel.Shareed;
using Microsoft.AspNetCore.Mvc.Rendering;
using Common.DataBaseAccess;
using Common.Entity;

namespace ProjectManager.Controllers
{
    public class TaskController : Controller
    {
        //------------------TASK SCREEN-------------------------------//
        public IActionResult TaskList(DisplayVM model)
        {
            Authentication.LoggedTask = null;
            Context context = new Context();
            TaskRepository repo = new TaskRepository();

            model.usersList = context.Users.ToList();

            model.Pager ??= new PagerVM();
            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0
                                        ? 10
                                        : model.Pager.ItemsPerPage;

            model.Pager.Page = model.Pager.Page <= 0
                                    ? 1
                                    : model.Pager.Page;



            model.Pager.PagesCount = (int)Math.Ceiling(repo.TasksCount() / (double)model.Pager.ItemsPerPage);
            //-------------------------------------------------------------//

            model.SharedTaskList = context.TaskToUser.ToList();
            if (model.SelectedID <= 0 || model.SelectedID == null)
            {
                model.TaskList = repo.GetAll(null, model.Pager.Page, model.Pager.ItemsPerPage);
            }
            else
            {
                List<ProjectTask> list = new List<ProjectTask>();
                foreach(var item in model.SharedTaskList)
                {
                    if (item.UserID == model.SelectedID && Authentication.LoggedProject.ID == item.ProjectID)
                    {
                        list.Add(context.Tasks.Find(item.TaskID));
                    }
                }
                model.TaskList = list;
            }
                
            //-----------------------------------------------------------//
            var selectList = (from user in context.Users
                              select new SelectListItem()
                              {
                                  Text = user.username,
                                  Value = user.ID.ToString()
                              }).ToList();
            selectList.Insert(0, new SelectListItem()
            {
                Text = "All",
                Value = "0"
            });
            
            //-------------------------------------------------------------//
            model.ListOfUsers = selectList;
            model.Owner = context.Users.Find(Authentication.LoggedProject.ownerID);
            return View(model);
            
        }
        public IActionResult TaskDetail(DetailsVM model)
        {
            Context context = new Context();

            if (model.Comment != null)
            {
                if (!string.IsNullOrEmpty(model.Comment.Comment))
                {
                    model.Comment.UserID = Authentication.LoggedUser.ID;
                    model.Comment.TaskID = Authentication.LoggedTask.ID;
                    model.Comment.Date = DateTime.Now;  
                    context.TaskComments.Add(model.Comment);
                    context.SaveChanges();
                    model.Comment = null;
                }
            }

            if (Authentication.LoggedTask == null)
            {
                return RedirectToAction("Index","Home");
            }

            

            var list = new List<WorkLog>();
            foreach (var item in context.WorkLogs)
            {
                if (item.TaskID == Authentication.LoggedTask.ID)
                {
                    list.Add(item);
                }
            }
            if (model.Filter != null && list.Count >0)
            {
                DateTime flag = new DateTime(0001, 1, 1, 0, 0, 0);
                bool dateFlag = true;
                if (model.Filter.From ==flag && model.Filter.To == flag)
                {
                    //{1.1.0001 г. 0:00:00}
                    dateFlag = false;
                }
                foreach (var item in context.WorkLogs)
                {
                    //pravq nov log i obhojdam s foreach da namera loga
                    //zashtoto ne moje contexta dokato raboti pak da se izvurshva rabota s nego
                    //i ne obhojdam prosto lista zashtoto v momenta koito iztrie nenujniq log bie greshka che e 
                    //promenen lista
                    WorkLog log = null;
                    foreach (var workLog in list)
                    {
                        if (item.ID == workLog.ID)
                        {
                            log = workLog;
                        }
                    }
                    if (log != null)
                    {
                        if (model.Filter.UserID != 0)
                        {
                            if (log.UserID != model.Filter.UserID)
                            {
                                list.Remove(log);
                                continue;
                            }
                        }
                        if (dateFlag)
                        {
                            if (log.Date < model.Filter.From || log.Date > model.Filter.To)
                            {
                                list.Remove(log);
                                continue;
                            }
                        }
                    }
                }
            }
            model.Owner = context.Users.ToList();
            model.Logs = list;

            var selectList = (from user in context.Users
                              select new SelectListItem()
                              {
                                  Text = user.username,
                                  Value = user.ID.ToString()
                              }).ToList();
            selectList.Insert(0, new SelectListItem()
            {
                Text = "All",
                Value = "0"
            });
            model.ListOfUsers = selectList;

            model.CommentsList = context.TaskComments.Where(x => x.TaskID == Authentication.LoggedTask.ID).ToList();
            return View(model);
        }
        
        //------------------------------------------------------------//
        //------------------REDIRECT----------------------------------//
        public IActionResult Redirect(int ID)
        {
            Context context = new Context();
            Authentication.LoggedTask = context.Tasks.Find(ID);
            return RedirectToAction("TaskDetail" , "Task");
        }
        //------------------------------------------------------------//
        //------------------ADD TASK----------------------------------//
        [HttpGet]
        public IActionResult AddTask()
        {
            Context context = new Context();
            CreateVM vm = new CreateVM();
            vm.UserList = context.Users.ToList();
            vm.SharedList = context.ProjectToUser.ToList();
            return View(vm);
        }
        [HttpPost]
        public IActionResult AddTask(CreateVM task)
        {
            TaskRepository taskRepository = new TaskRepository();
            ProjectTask item = new ProjectTask();

            item.parentID = Authentication.LoggedProject.ID;
            item.title = task.title;
            item.description = task.description;
            taskRepository.addTask(item);

            return RedirectToAction("TaskList","Task");
        }
        //------------------------------------------------------------//
        //------------------REMOVE TASK-------------------------------//
        public IActionResult RemoveTask(int id)
        {
            TaskRepository taskRepository = new TaskRepository();
            taskRepository.deleteTask(id);
            return RedirectToAction("TaskList", "Task");
        }
        //------------------------------------------------------------//
        //------------------UPDATE TASK-------------------------------//
        [HttpGet]
        public IActionResult UpdateTask(int id)
        {
            UpdateVM item = new UpdateVM();
            Context context = new Context();
            ProjectTask updateTask = context.Tasks.Find(id);

            item.ID = updateTask.ID;
            item.parentID = updateTask.parentID;
            item.title = updateTask.title;
            item.description = updateTask.description;

            return View(item);
        }
        [HttpPost]
        public IActionResult UpdateTask(UpdateVM item)
        {
            ProjectTask task = new ProjectTask();
            TaskRepository taskRepository = new TaskRepository();

            task.ID = item.ID;
            task.parentID = item.parentID;
            task.title = item.title;
            task.description = item.description;

            taskRepository.updateTask(task);
            return RedirectToAction("TaskList", "Task");
        }
        //------------------------------------------------------------//
        //------------------SHARE TASK--------------------------------//
        [HttpGet]
        public IActionResult ShareTask(int taskID)
        {
            Context context = new Context();
            ProjectTask projectTask = context.Tasks.Find(taskID);
            ShareVM shareVM = new ShareVM();

            Authentication.LoggedTask = projectTask;
            shareVM.UserList=context.Users.ToList();
            
            List<User> users = new List<User>();
            foreach (var item in context.ProjectToUser)
            {
                if (projectTask.parentID == item.ProjectID)
                {
                    foreach (var user in shareVM.UserList)
                    {
                        if (user.ID == item.UserID)
                        {
                            users.Add(user);
                        }
                    }
                }
            }
            
            var selectList = (from user in users.Where(user => user.ID != Authentication.LoggedUser.ID)
                              select new SelectListItem()
                              {
                                  Text = user.username,
                                  Value = user.ID.ToString()
                              }).ToList();
            shareVM.ListOfUsers = selectList;

            List<User> SharedWith = new List<User>();

            foreach (var item in context.TaskToUser)
            {
                if (item.TaskID == Authentication.LoggedTask.ID)
                {
                    bool isExisting = false;
                    foreach(var user in SharedWith)
                    {
                        if (user.ID == item.UserID)
                        {
                            isExisting = true;
                            break;
                        }
                    }
                    if (!isExisting)
                    {
                        foreach (var user in shareVM.UserList)
                        {
                            if (item.UserID == user.ID)
                            {
                                SharedWith.Add(user);
                                break;
                            }
                        }
                    }
                }
            }
            shareVM.SharedWith = SharedWith;
            
            return View(shareVM);
        }
        [HttpPost]
        public IActionResult ShareTask(ShareVM vm ,int userID)
        {
            Context context = new Context();
            TaskToUser item = new TaskToUser();
            Project project = context.Projects.Find(Authentication.LoggedTask.parentID);

            item.UserID = userID;
            item.TaskID = Authentication.LoggedTask.ID;
            item.ProjectID = project.ID;
            foreach (var share in context.TaskToUser)
            {
                if (share.UserID == item.UserID && share.TaskID == item.TaskID && item.ProjectID == share.ProjectID)
                {
                    return RedirectToAction("TaskList", "Task");
                }
            }
            context.TaskToUser.Add(item);
            context.SaveChanges();
            Authentication.LoggedTask = null;

            return RedirectToAction("TaskList","Task");
        }
        //------------------------------------------------------------//
        //------------------REMOVE SHARE------------------------------//
        public IActionResult RemoveShare(int id)
        {
            Context context =new Context();
            TaskToUser item = context.TaskToUser.Find(id);
            context.TaskToUser.Remove(item);
            context.SaveChanges();
            return RedirectToAction("TaskList", "Task");
        }
        //------------------------------------------------------------//
    }
}
