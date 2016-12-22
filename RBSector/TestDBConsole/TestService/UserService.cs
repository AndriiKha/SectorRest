using NHibernate;
using NHibernate.Cfg;
using RBSector.DataBase.Models;
using RBSector.Entry.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestDBConsole.Logic;

namespace TestDBConsole.TestService
{
    public class UserService
    {
        private ISession mySession;

        public UserService()
        {
            mySession = NHibernateConf.Session;
        }
        public void AddUser(string login, string password, string lname, string fname, string email, string role)
        {
            Usersdata user = new Usersdata();
            user.Login = login;
            user.Password = password;
            user.Lname = lname;
            user.Fname = fname;
            user.Email = email;
            user.Role = role;

            using (mySession.BeginTransaction())
            {
                mySession.Save(user);
                mySession.Transaction.Commit();
            }
        }
    }
}
