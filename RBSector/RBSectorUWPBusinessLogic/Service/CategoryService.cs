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
        public bool CreateCategory(string nameCategory)
        {
            bool isUniqueName = BindingModel.CheckNameUnique(typeof(TabViewModel), nameCategory);
            if (!isUniqueName) return false;
            CategoryViewModel category = new CategoryViewModel();
            category.CT_Name = nameCategory;
            category.CT_RECID = GenerateNextCategoryID;
            category.TabParent = BindingModel.GetParentTab();
            category.Status = STATUS.Created.ToString();
            category.TabParent.Categories.Add(category);

            if (isUniqueName)
            {
                SetCategorySingleToBindingModel(category);
            }
            return true;
        }
        public bool Update(string oldCategoryName, string newNameTab)
        {
            if (!BindingModel.CheckNameUnique(typeof(CategoryViewModel), newNameTab)) return false;
            var item = BindingModel.Category.FirstOrDefault(x => x.CT_Name == oldCategoryName);
            if (item != null)
            {
                item.CT_Name = newNameTab;
                item.Status = STATUS.Edited.ToString();
            }
            return true;
        }
        public bool Delete(int id)
        {
            var item = BindingModel.Category.FirstOrDefault(x => x.CT_RECID == id);
            if (item == null)
            {
                BindingModel.DELETED_ITEM = DELETED_PART.CATEGORY_DELETED + ":" + id;
                item.Status = STATUS.Deleted.ToString();
                BindingModel.Category.Remove(item);
            }
            else return false;
            return true;
        }
        public CategoryViewModel GetCategory(int id)
        {
            if (BindingModel.Category == null || BindingModel.Category.Count < 1) return null;
            return BindingModel.Category.Where(x => x.CT_RECID == id).FirstOrDefault();
        }
        public int GenerateNextCategoryID
        {
            get
            {
                var list = GetAllCategory();
                if (list.Count > 0)
                    return (list.Select(x => x.CT_RECID).Max() + 1);
                return 1;
            }
        }

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
        public ObservableCollection<CategoryViewModel> GetAllCategory()
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
