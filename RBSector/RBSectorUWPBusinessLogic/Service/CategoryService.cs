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
        private Presenter _presenter;
        public CategoryService()
        {
            _presenter = Presenter.Instance();
            srv = new ServiceClient.TabsServiceClient(ServiceClient.TabsServiceClient.EndpointConfiguration.BasicHttpBinding_ITabsService);
        }
        public bool CreateCategory(string nameCategory)
        {
            bool isUniqueName = _presenter.CheckNameUnique<TabViewModel>(nameCategory);
            if (!isUniqueName) return false;
            CategoryViewModel category = new CategoryViewModel();
            category.CT_Name = nameCategory;
            category.CT_RECID = GenerateNextCategoryID;
            category.TabParent = _presenter.GetSelectedTab();
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
            if (!_presenter.CheckNameUnique<CategoryViewModel>(newNameTab)) return false;
            var item = _presenter.Category.FirstOrDefault(x => x.CT_Name == oldCategoryName);
            if (item != null)
            {
                item.CT_Name = newNameTab;
                item.Status = STATUS.Edited.ToString();
            }
            return true;
        }
        public bool Delete(int id)
        {
            var item = _presenter.Category.FirstOrDefault(x => x.CT_RECID == id);
            if (item != null)
            {
                if (!STATUS.Created.Equals(item.Status))
                    _presenter.DELETED_ITEM = DELETED_PART.CATEGORY_DELETED + ":" + id;
                item.Status = STATUS.Deleted.ToString();
                _presenter.Category.Remove(item);
            }
            else return false;
            return true;
        }
        public CategoryViewModel GetCategory(int id)
        {
            if (_presenter.Category == null || _presenter.Category.Count < 1) return null;
            return _presenter.Category.Where(x => x.CT_RECID == id).FirstOrDefault();
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
            if (_presenter.Category.Count > 0)
                _presenter.Category.Clear();
            foreach (var item in category)
            {
                if (_presenter.Category.Where(x => x.CT_RECID == item.CT_RECID).FirstOrDefault() == null)
                    _presenter.Category.Add(item);
            }
        }
        public void SetCategorySingleToBindingModel(CategoryViewModel category)
        {
            if (_presenter.Category.Where(x => x.CT_RECID == category.CT_RECID).FirstOrDefault() == null)
                _presenter.Category.Add(category);
        }
        public ObservableCollection<CategoryViewModel> GetAllCategory()
        {
            ObservableCollection<CategoryViewModel> category = new ObservableCollection<CategoryViewModel>();
            foreach (var tab in _presenter.Tabs)
            {
                _presenter.AddRange(category, tab.Categories);
            }
            return category;
        }
    }
}
