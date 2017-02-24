using Newtonsoft.Json;
using NHibernate;
using NHibernate.Linq;
using RBSector.DataBase.Models;
using RBSector.Entry.Logic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RBSector.Entry.Entry
{
    public class UserEntry
    {
        private ISession session;
        private UniversalCRUD<Usersdata> user_crud;

        public UserEntry()
        {
            session = NHibernateConf.Session;
            user_crud = new UniversalCRUD<Usersdata>();
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
        public string isLogInPin(string pin)
        {
            session = NHibernateConf.CreateNewSession;
            Usersdata user = null;
            using (session.BeginTransaction())
            {
                user = (from p in session.Query<Usersdata>()
                        where p.UsrPassword.Equals(pin)
                        select p).FirstOrDefault();
            }
            if (user == null) return string.Empty;
            string json = JsonConvert.SerializeObject(user);
            return json;
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
        public Usersdata GetUser(int recid)
        {
            Usersdata user = null;
            try
            {
                user = user_crud.GetObj_ID(recid);
            }
            catch (Exception exc)
            {
                return user;
            }
            return user;
        }

        public bool UpdateUser(Usersdata user)
        {
            try
            {
                user_crud.SaveOrUpdate(user);
            }
            catch (Exception e)
            {
                //todo log it
                return false;
            }
            return true;
        }

        public List<Usersdata> GetAllUsers()
        {
            var resData = new List<Usersdata>();
            try
            {
                using (session.BeginTransaction())
                {
                    resData.AddRange((List<Usersdata>)session.CreateQuery("FROM Usersdata").List<Usersdata>());
                }
            }
            catch (Exception e)
            {
                //todo log it
                return new List<Usersdata>();
            }
            return resData;
        }

        public bool DeleteUser(int recId)
        {
            return user_crud.Delete<Usersdata>(recId);
        }
    }
}
