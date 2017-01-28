using RBSectorUWPBusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSectorUWPBusinessLogic.Service
{
   public class CategoryService
    {
        private ServiceClient.TabsServiceClient srv;
        public CategoryService()
        {
            srv = new ServiceClient.TabsServiceClient(ServiceClient.TabsServiceClient.EndpointConfiguration.BasicHttpBinding_ITabsService);
        }
        
        public static CategoryViewModel GetCategory(int id)
        {
            if (BindingModel.Category == null || BindingModel.Category.Count < 1) return null;
            return BindingModel.Category.Where(x => x.CT_RECID == id).FirstOrDefault();
        }
        public int GenerateNextCategoryID { get { return (GetAllCategory().Select(x => x.CT_RECID).Max() + 1); } }

        public void SetCategoryToBindingModel(ObservableCollection<CategoryViewModel> category)
        {
            if (BindingModel.Category.Count > 0)
                BindingModel.Category.Clear();
            foreach (var item in category)
            {
                if (BindingModel.Category.Where(x => x.CT_RECID == item.CT_RECID).FirstOrDefault() == null)
                    BindingModel.Category.Add(item);
            }
        }
        public void SetCategorySingleToBindingModel(CategoryViewModel category)
        {
            if (BindingModel.Category.Where(x => x.CT_RECID == category.CT_RECID).FirstOrDefault() == null)
                BindingModel.Category.Add(category);
        }
        public static ObservableCollection<CategoryViewModel> GetAllCategory()
        {
            ObservableCollection<CategoryViewModel> category = new ObservableCollection<CategoryViewModel>();
            foreach (var tab in BindingModel.Tabs)
            {
                category.AddRange(tab.Categories);
            }
            return category;
        }
    }
}
