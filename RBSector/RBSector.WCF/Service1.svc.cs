using RBSector.Entry.Entry;
using RBSector.WCF.IService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace RBSector.WCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service1.svc or Service1.svc.cs at the Solution Explorer and start debugging.
    public class Service1 : ITabsService
    {
        TabsEntry tb_entry;
        public Service1()
        {
            tb_entry = new TabsEntry();
        }
        public bool AddTabs(string name)
        {
            
            return tb_entry.AddTab(name);
        }

        public string GetAllTabs()
        {
            return tb_entry.GetAllTabs();
        }

        public string GetTab(string name)
        {
            return string.Empty;//tb_entry.GetTab(name);
        }
    }
}
