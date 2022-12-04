using Common.DataBaseAccess;
using Common.Entity;
using Common.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace Api.Controllers
{
    public class UserControllers : Controller
    {
        [HttpGet]
        public ActionResult Get()
        { 
            Context context = new Context();
            return Ok(context.Users);
        }

        [HttpGet("{ID}")]
        public ActionResult Get(int ID)
        {
            Context context = new Context();
            User item = context.Users.Find(ID);

            if (item == null)
                return NotFound();

            return Ok(item);
        }

        [HttpPost]
        public ActionResult Post()
        {
            return View();
        }

        [HttpPut]
        public IActionResult Put([FromQuery] User model)
        {
            Context context = new Context();
            User item = context.Users.Find(model.ID);

            if (item == null) 
            {
                context.Users.Add(model);
            }
            else
            {
                item.ID = model.ID;
                item.username = model.username;
                item.password = model.password;
                item.firstName = model.firstName;
                item.lastName = model.lastName;
                item.IsAdmin = model.IsAdmin;

                context.Entry(item).State = EntityState.Modified;
            }

            context.SaveChanges();
            return Created(model.ID.ToString(),model);
        }
        [HttpDelete("{ID}")]
        public ActionResult Delete(int ID)
        {
            User item = null;
            foreach (User user in UsersRepository.Items)
            {
                if (user.ID == ID)
                {
                    item = user;
                    break;
                }
            }

            if (item != null)
            {
                UsersRepository.Items.Remove(item);
                return Ok(item);
            }

            return NotFound();
        }
    }
}
