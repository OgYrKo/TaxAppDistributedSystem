using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MainOfficeClient.Classes
{
    internal class CurrentUser
    {
        private static CurrentUser _instance = null;
        private static readonly object _lock = new object();
        public string Login { get; private set; }
        public string Password { get; private set; }
        public string Postition { get; private set; }
        public string MailLogin { get; set; }
        public string MailPassword { get; set; }

        private CurrentUser() 
        {
            Login = "postgres";
            Password = "1riffik1";
            Postition = "superadmin";
        }
        private CurrentUser(string login, string password, string postition)
        {
            Login = login;
            Password = password;
            Postition = postition;
        }

        public static CurrentUser Instance(string login, string password, string postition)
        {
            lock (_lock)
            {
                if (_instance == null) { _instance = new CurrentUser(login, password, postition); }
                return _instance;
            }
        }

        public static void ResetUser()
        {
            _instance = null;
        }

        public static CurrentUser Instance()
        {
            lock (_lock)
            {
                if (_instance == null) { _instance = new CurrentUser(); }
                return _instance;
            }
        }
    }
}
