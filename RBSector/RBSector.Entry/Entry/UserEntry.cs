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
        public bool AddUser(string login, string password, string lname, string fname, string email, string role)
        {
            Usersdata user = new Usersdata();
            user.UsrLogin = login;
            user.UsrPassword = password;
            user.UsrLname = lname;
            user.UsrFname = fname;
            user.UsrEmail = email;
            user.UsrRole = role;
            try
            {
                using (session.BeginTransaction())
                {
                    session.Save(user);
                    session.Transaction.Commit();
                }
            }
            catch (Exception e)
            {
                return false;
            }
            return true;
        }
        public bool isLogIn(string login, string password)
        {
            Usersdata user = null;
            using (session.BeginTransaction())
            {
                user = (from p in session.Query<Usersdata>()
                        where p.UsrLogin.Equals(login) && p.UsrPassword.Equals(password)
                        select p).FirstOrDefault();
            }
            return user != null;
        }
        public bool isNotExistLogin(string login)
        {
            string checkedLogin = null;
            using (session.BeginTransaction())
            {
                checkedLogin = (from p in session.Query<Usersdata>()
                                where p.UsrLogin.ToLower().Equals(login.ToLower())
                                select p.UsrLogin).FirstOrDefault();
            }
            return string.IsNullOrEmpty(checkedLogin);
        }
    }
}
