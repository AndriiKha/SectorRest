using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
using RBSector.DataBase.Models;
using RBSector.Entry.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSector.Entry.Entry
{
    public class UserEntry
    {
        private ISession session;

        public UserEntry()
        {
            session = NHibernateConf.Session;
        }
        public void AddUser(string login, string password, string lname, string fname, string email,string role)
        {
            Usersdata user = new Usersdata();
            user.Login = login;
            user.Password = password;
            user.Lname = lname;
            user.Fname = fname;
            user.Email = email;
            user.Role = role;

            using (session.BeginTransaction())
            {
                session.Save(user);
                session.Transaction.Commit();
            }
        }
        public bool isLogIn(string login, string password)
        {
            Usersdata user = null;
            using (session.BeginTransaction())
            {
                user = (from p in session.Query<Usersdata>()
                        where p.Login.Equals(login) && p.Password.Equals(password)
                        select p).FirstOrDefault();
            }
            return user!=null;
        }
    }
}
