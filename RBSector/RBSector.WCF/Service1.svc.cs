using RBSector.DataBase.Models;
using RBSector.Entry.Entry;
using RBSector.WCF.IService;
using System.Collections.Generic;

namespace RBSector.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : ITabsService, IMainService, IUserService
    {
        TabsEntry tb_entry;
        CategoryEntry cat_entry;
        MainSubmitEntry main_entry;
        UserEntry user_entry;
        public Service1()
        {
            tb_entry = new TabsEntry();
            cat_entry = new CategoryEntry();
            main_entry = new MainSubmitEntry();
            user_entry = new UserEntry();
        }
        public bool AddTabs(string name)
        {

            return tb_entry.AddTab(name);
        }

        public string GetAllTabs()
        {
            return tb_entry.GetAllTabs();
        }

        public Tabs GetTab(string name)
        {
            return tb_entry.GetTab(name);
        }

        public bool AddCategory(string name)
        {
            return cat_entry.AddCategory(name);
        }

        public bool SaveResult(string json, string deleted)
        {
            return main_entry.SaveResult(json, deleted);
        }

        public bool SaveOrder(string json)
        {
            return main_entry.SaveOrder(json);
        }

        public bool isLogIn(string login, string password)
        {
            throw new System.NotImplementedException();
        }

        public bool UpdateUser(Usersdata user)
        {
            return user_entry.UpdateUser(user);
        }

        public Usersdata GetUser(int recId)
        {
            return user_entry.GetUser(recId);
        }

        public bool AddUser(string login, string password, string lname, string fname, string email, string role)
        {
            return user_entry.AddUser(login, password, lname, fname, email, role);
        }

        public bool DeleteUser(int recId)
        {
            return user_entry.DeleteUser(recId);
        }

        public List<Usersdata> GetAllUsers()
        {
            return user_entry.GetAllUsers();
        }
    }
}
