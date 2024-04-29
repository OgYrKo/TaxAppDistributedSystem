using System.Collections.Generic;
using System.Linq;
using InterfacesLibrary;
using DBClassesLibrary;
using Microsoft.EntityFrameworkCore;
using CoreWCF;

namespace ServerImplementation
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Settings: SetUserTemplate, ISettings
    {
        public string AddUser(User user)
        {
            using (var context = new TSNAPContext())
            {
                try
                {
                    context.Database.ExecuteSqlRaw("CALL create_new_user ({0}, {1}, {2});", user.Usename, user.Passwd, user.Rolname);
                    return "1";
                }
                catch
                {
                    return "-1";
                }
            }
        }

        public string DeleteUser(string login)
        {
            using (var context = new TSNAPContext())
            {
                try
                {
                    context.Database.ExecuteSqlRaw("CALL delete_user ({0});", login);
                    return "1";
                }
                catch
                {
                    return "-1";
                }
            }
        }

        public List<User> GetAllUsers()
        {
            using (var context = new TSNAPContext())
            {
                List<User> users;
                users = context.Users.ToList();
                return users;
            }
        }

        public User GetUser(uint id)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    var users = context.Users;
                    return users.AsQueryable().FirstOrDefault(x => x.Usesysid == id);
                }
            }
            catch
            {
                return null;
            }
        }

        public User GetUserByName(string login)
        {
            try
            {
                using (var context = new TSNAPContext())
                {
                    var users = context.Users;
                    return users.AsQueryable().FirstOrDefault(x => x.Usename.Equals(login));
                }
            }
            catch
            {
                return null;
            }
        }

        public string SetConnectionString(string login, string password)
        {
            try
            {
                using (var context = new TSNAPContext(login, password))
                {
                    var response = context.Users.Where(value => value.Usename == login).Select(value => context.get_group_name(value.Usename)).ToList();
                    string group_name = response[0];
                    return group_name;
                }

            }
            catch
            {
                return "-1";
            }
        }

        public string UpdateUser(User user)
        {
            using (var context = new TSNAPContext())
            {
                try
                {
                    context.Database.ExecuteSqlRaw("CALL update_user({0}, {1}, {2}, {3});", user.Usesysid, user.Usename, user.Passwd, user.Rolname);
                    return "1";
                }
                catch
                {
                    return "-1";
                }
            }
        }
    }
}