using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Common.DataBaseAccess;
using Common.Entity;
using Common.Repository;
using Common.Service;
using ProjectManager.ViewModel.Shareed;
using ProjectManager.ViewModel.WorkLogVM;

namespace ProjectManager.Controllers
{
    public class LogsController : Controller
    {
        //------------------DISPLAYS ALL LOGS--------------------//
        public IActionResult Index(DisplayVM model)
        {
            Context context = new Context();
            WorkLogRepository repo = new WorkLogRepository();
            model.Pager ??= new PagerVM();
            model.Filter ??= new FilterVM();
            List<WorkLog> list =context.WorkLogs.ToList();
            List<ProjectTask> taskList = context.Tasks.ToList();    
            bool titleFlag = true;
            bool dateFlag = true;

            if (string.IsNullOrEmpty(model.Filter.taskTitle))
            {
                titleFlag = false;
            }
            if (model.Filter.From == null && model.Filter.From == null)
            {
                dateFlag = false;
            }

            foreach (var item in context.WorkLogs)
            {
                ProjectTask projectTask = null;
                foreach (var task in taskList)
                {
                    if (task.ID == item.TaskID)
                    {
                        projectTask = task;
                    }
                }

                if (titleFlag)
                {
                    if (!projectTask.title.Contains(model.Filter.taskTitle) )
                    {
                        list.Remove(item);
                        continue;
                    }
                }
                if (dateFlag)
                {
                    if (item.Date < model.Filter.From || model.Filter.To > item.Date)
                    {
                        list.Remove(item);
                        continue;
                    }
                }
                
            }


            model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0
                                                        ? 10
                                                        : model.Pager.ItemsPerPage;

            model.Pager.Page = model.Pager.Page <= 0
                                    ? 1
                                    : model.Pager.Page;

            model.Pager.PagesCount = (int)Math.Ceiling(repo.LogsCount(x => x.UserID == Authentication.LoggedUser.ID) / (double)model.Pager.ItemsPerPage);

            model.LogsList = repo.GetAllFromList(x => x.UserID == Authentication.LoggedUser.ID,list, model.Pager.Page, model.Pager.ItemsPerPage);
            model.TasksList = context.Tasks.ToList();

            return View(model);
        }
        //-------------------------------------------------------//
        //-------------CREATE WORKLOG FROM WORK TAB--------------//
        [HttpGet]
        public IActionResult Create()
        {
            Context context = new Context();
            CreateVM vm = new CreateVM();

            List<TaskToUser> list = new List<TaskToUser>();
            foreach (var item in context.TaskToUser)
            {
                if (item.UserID == Authentication.LoggedUser.ID)
                {
                    list.Add(item);
                }
            }
            var selectList = (from task in list
                              select new SelectListItem()
                              {
                                  Text = context.Tasks.Find(task.TaskID).title,
                                  Value = task.TaskID.ToString()
                              }).ToList();


            if (selectList == null || selectList.Count == 0)
            {
                selectList.Add(new SelectListItem() { Text = "You dont have any tasks", Value = "-3" });
            }


            vm.TaskList = selectList;
            return View(vm);
        }
        [HttpPost]
        public IActionResult Create(CreateVM model)
        {
            Context context = new Context();
            WorkLog item = new WorkLog();

            item.UserID = Authentication.LoggedUser.ID;
            item.Date = DateTime.Now;
            item.TaskID = model.TaskID;
            item.LoggedHours = model.LoggedHours;

            context.WorkLogs.Add(item);
            context.SaveChanges();

            return RedirectToAction("Index","Logs");
        }
        //-------------------------------------------------------//
        //-----------CREATE WORKLOG FROM DETAILS TAB-------------//
        [HttpGet]
        public IActionResult CreateSpecific()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateSpecific(CreateVM model)
        {
            Context context = new Context();
            WorkLog log = new WorkLog();

            log.UserID = Authentication.LoggedUser.ID;
            log.TaskID = Authentication.LoggedTask.ID;
            log.Date = DateTime.Now;
            log.LoggedHours = model.LoggedHours;

            context.WorkLogs.Add(log);
            context.SaveChanges();

            return RedirectToAction("TaskDetail","Task");
        }
        //-------------------------------------------------------//
    }
}
