using Common.Entity;
using Common.DataBaseAccess;
using Common.Repository;

namespace Common.Service
{
    public class Authentication
    {
        public static User LoggedUser { get; set; }
        public static Project LoggedProject { get; set; }
        public static ProjectTask LoggedTask { get; set; }
        public static void GetInfoToLoggedUser(string username , string password)
        {
            Context context = new Context();
            UsersRepository controller = new UsersRepository();
            LoggedUser = controller.getByUsernameAndPassword(username, password);
        }
    }
}
