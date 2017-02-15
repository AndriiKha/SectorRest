using Newtonsoft.Json;
using RBSectorUWPBusinessLogic.JSonTools;
using RBSectorUWPBusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSectorUWPBusinessLogic.Service
{
    public class TabsService
    {
        private ServiceClient.TabsServiceClient srv;
        public TabsService()
        {
            srv = new ServiceClient.TabsServiceClient(ServiceClient.TabsServiceClient.EndpointConfiguration.BasicHttpBinding_ITabsService);
        }
        public bool CreateTab(string nameTab)
        {
            bool isUniqueName = BindingModel.CheckNameUnique(typeof(TabViewModel), nameTab);
            if (!isUniqueName) return false;

            TabViewModel tab = new TabViewModel();
            tab.TB_RECID = GenerateNextTabID;
            tab.TB_Name = nameTab;
            tab.Status = STATUS.Created.ToString();

            if (isUniqueName)
            {
                SetTabsSingleToBindingModel(tab);
            }
            return true;
        }
        public bool Update(string oldTabName, string newNameTab)
        {
            if (!BindingModel.CheckNameUnique(typeof(TabViewModel), newNameTab)) return false;
            var item = BindingModel.Tabs.FirstOrDefault(x => x.TB_Name == oldTabName);
            if (item != null)
            {
                item.TB_Name = newNameTab;
                item.Status = STATUS.Edited.ToString();
            }
            return true;
        }
        public bool Delete(int id)
        {
            var item = BindingModel.Tabs.FirstOrDefault(x => x.TB_RECID == id);
            if (item == null)
            {
                BindingModel.DELETED_ITEM = DELETED_PART.TAB_DELETED + ":" + id;
                item.Status = STATUS.Deleted.ToString();
                BindingModel.Tabs.Remove(item);
            }
            else return false;
            return true;
        }
        public int GenerateNextTabID
        {
            get
            {
                var list = BindingModel.Tabs;
                if (list.Count > 0)
                    return list.Select(x => x.TB_RECID).Max() + 1;
                return 1;
            }
        }
        public static TabViewModel GetTab(int id)
        {
            if (BindingModel.Tabs == null || BindingModel.Tabs.Count < 1) return null;
            return BindingModel.Tabs.Where(x => x.TB_RECID == id).FirstOrDefault();
        }
        public ObservableCollection<TabViewModel> GetAllTabs()
        {
            string tabJson = srv.GetAllTabsAsync().Result;
            var tabs = tabJson.TabsDeserialize(typeof(TabViewModel));
            return tabs;
        }
        public void SetTabsToBindingModel(ObservableCollection<TabViewModel> tabs)
        {
            if (BindingModel.Tabs.Count > 0)
                BindingModel.Tabs.Clear();
            foreach (var item in tabs)
            {
                if (BindingModel.Tabs.Where(x => x.TB_RECID == item.TB_RECID).FirstOrDefault() == null)
                    BindingModel.Tabs.Add(item);
            }
        }
        public void SetTabsSingleToBindingModel(TabViewModel tab)
        {
            if (BindingModel.Tabs.Where(x => x.TB_RECID == tab.TB_RECID).FirstOrDefault() == null)
                BindingModel.Tabs.Add(tab);
        }
    }
}
