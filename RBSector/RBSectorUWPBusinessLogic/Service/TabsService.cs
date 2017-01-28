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
        public int GenerateNextTabID { get { return BindingModel.Tabs.Select(x => x.TB_RECID) != null ? (BindingModel.Tabs.Select(x => x.TB_RECID).Max() + 1) : default(int); } }
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
