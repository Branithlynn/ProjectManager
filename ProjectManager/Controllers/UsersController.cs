using Microsoft.AspNetCore.Mvc;
using Common.Entity;
using Common.DataBaseAccess;
using Common.Service;
using Common.Repository;
using ProjectManager.ViewModel.UserVM;
using ProjectManager.ViewModel.Shareed;

namespace ProjectManager.Controllers
{
    public class UsersController : Controller
    {
        //------------------LOGOUT-------------------------------//
        public IActionResult Logout()
        {
            Authentication.LoggedUser = null;
            Authentication.LoggedProject = null;
            Authentication.LoggedTask = null;
            return RedirectToAction("HomePage", "Home");
        }
        //-------------------------------------------------------//
        //------------------REGISTER SCREEN----------------------//
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterVM item)
        {
            if (!ModelState.IsValid)
                return View(item);

            UsersRepository usersRepository = new UsersRepository();
            User user = new User();

            user.username = item.Username;
            user.password = item.Password;
            user.firstName = item.FirstName;
            user.lastName =item.LastName;
            user.IsAdmin = false;
            usersRepository.AddUser(user);
            Authentication.LoggedUser=user;

            return RedirectToAction("HomePage","Home");
        }
        //-------------------------------------------------------//
        //------------------LOGIN SCREEN-------------------------//
        [HttpGet]
        public IActionResult Loggin()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Loggin(string username, string password)
        {
            Authentication.GetInfoToLoggedUser(username,password);

            if (Authentication.LoggedUser != null)
            {
                    return RedirectToAction("HomePage","Home");
            }
            else
            {
                return RedirectToAction("Loggin");
            }
            
        }
        //-------------------------------------------------------//
        //------------------DISPLAYS ALL USERS-------------------//
        public IActionResult UserList(IndexVM model)
        {
            Authentication.LoggedProject = null;
            Authentication.LoggedTask = null;
            if (Authentication.LoggedUser != null)
            {

                model.Pager ??= new PagerVM();
                model.Filter ??= new FilterVM();
                model.Pager.ItemsPerPage = model.Pager.ItemsPerPage <= 0
                                            ? 10
                                            : model.Pager.ItemsPerPage;

                model.Pager.Page = model.Pager.Page <= 0
                                        ? 1
                                        : model.Pager.Page;

                var filter = model.Filter.GetFilter();

                UsersRepository usersRepository = new UsersRepository();
                model.Items = usersRepository.GetAll(filter, model.Pager.Page,model.Pager.ItemsPerPage);
                model.Pager.PagesCount = (int)Math.Ceiling( usersRepository.UsersCount(filter) / (double)model.Pager.ItemsPerPage);
                
                return View(model);
            }
            else
            {
                return View("Loggin");
            }
        }
        //-------------------------------------------------------//
        //------------------ADD METHOD---------------------------//
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(CreateVM item)
        {
            if(!ModelState.IsValid)
                return View(item);

            UsersRepository userRepo = new UsersRepository();
            User user = new User();
            user.username = item.Username;
            user.password = item.Password;
            user.firstName = item.FirstName;
            user.lastName = item.LastName;
            user.IsAdmin = item.IsAdmin;

            userRepo.AddUser(user);

            if (Authentication.LoggedUser.IsAdmin)
            {
                return RedirectToAction("UserList", "Users");
            }
            else
            {
                return RedirectToAction("ProjectsList", "Project");
            }
        }
        //------------------------------------------------------//
        //------------------DELETING USER METHOD----------------//
        public IActionResult DeleteUser(int id)
        {
            UsersRepository userRepo = new UsersRepository();
            ProjectRepository projectRepo = new ProjectRepository();

            projectRepo.DeleteProjectsByOwnerID(id);
            userRepo.DeleteUser(id);

            return RedirectToAction("UserList","Users");
        }
        //------------------------------------------------------//
        //------------------UPDATE USER-------------------------//
        [HttpGet]
        public IActionResult UpdateUser(int id)
        {
            Context context = new Context();
            User user = context.Users.Find(id);
            EditVM item = new EditVM();

            item.ID = user.ID;
            item.Username = user.username;
            item.Password = user.password;
            item.FirstName = user.firstName;
            item.LastName = user.lastName;
            item.IsAdmin = user.IsAdmin;

            return View(item);
        }
        [HttpPost]
        public IActionResult UpdateUser(EditVM item)
        {
            UsersRepository userRepo = new UsersRepository();
            User user = new User();
            user.ID = item.ID;
            user.username = item.Username;
            user.password = item.Password;
            user.firstName = item.FirstName;
            user.lastName = item.LastName;
            user.IsAdmin = item.IsAdmin;

            userRepo.UpdateUser(user);

            return RedirectToAction("UserList", "Users");
        }
        //------------------------------------------------------//
    }
}
