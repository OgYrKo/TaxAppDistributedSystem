using InterfacesLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerImplementation
{
    public abstract class SetUserTemplate: ISetUser
    {
        protected string UsernameDB { get; set; }
        protected string PasswordDB { get; set; }

        public void SetUser(string usernameDB, string passwordDB)
        {
            UsernameDB = usernameDB;
            PasswordDB = passwordDB;
        }
    }
}
