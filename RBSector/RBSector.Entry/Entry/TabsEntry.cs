using NHibernate;
using NHibernate.Linq;
using RBSector.DataBase.Models;
using RBSector.Entry.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RBSector.Entry.Tools;
using System.Xml.Serialization;
using System.IO;
using System.Data;
using System.Json;
using Newtonsoft.Json;

namespace RBSector.Entry.Entry
{
    public class TabsEntry
    {
        private ISession session;
        private UniversalCRUD<Tabs> tab_crud;
        public TabsEntry()
        {
            session = NHibernateConf.Session;
            tab_crud = new UniversalCRUD<Tabs>();
        }
        public bool AddTab(string json)
        {
            if (string.IsNullOrEmpty(json)) return false;
            Tabs newTab = new Tabs();
            var obj = (JsonObject)JsonValue.Parse(json);
            newTab.TbName = obj["TB_Name"].ToString().Trim('\"');
            return tab_crud.SaveOrUpdate(newTab);
        }
        public int GetTabId(string name)
        {
            int idTab = -1;
            using (session.BeginTransaction())
            {
                idTab = (from p in session.Query<Tabs>()
                         where p.TbName.Equals(name)
                         select p.TbRecid).FirstOrDefault();
            }
            return idTab;
        }
        public bool IsNotExistTab(string name)
        {
            string nameTab = string.Empty;
            using (session.BeginTransaction())
            {
                nameTab = (from p in session.Query<Tabs>()
                           where p.TbName.Equals(name)
                           select p.TbName).FirstOrDefault();
            }
            return string.IsNullOrEmpty(nameTab);
        }
        public string GetAllTabs()
        {
            List<Tabs> tabs = new List<Tabs>();
            using (session.BeginTransaction())
            {
                tabs = (List<Tabs>)session.CreateQuery("FROM Tabs").List<Tabs>();
            }
            string result = JsonTools.Componets<Tabs>(tabs,true);
            //string s = JsonConvert.SerializeObject(tabs);
           // var json = (JsonObject)JsonValue.Parse(result);
            //result.sn = json["sn"].ToString().Trim('\"');*/
            return result;
        }
        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }
        public Tabs GetTab(string name)
        {
            #region[SQL SELECT NHIBERNATE]

                Tabs tab;
                using (session.BeginTransaction())
                {
                    tab = (from p in session.Query<Tabs>()
                           where p.TbName.Equals(name)
                           select p).FirstOrDefault();
                }
            #endregion

            return tab;//JsonConvert.SerializeObject(tab_vm);
        }
    }
}
