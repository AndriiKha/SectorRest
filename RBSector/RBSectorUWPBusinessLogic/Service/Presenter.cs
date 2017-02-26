using RBSectorUWPBusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSectorUWPBusinessLogic.Service
{
    public class Presenter
    {
        #region[Varibles]
        private static Presenter presenter;
        #endregion

        #region[CTOR]
        private Presenter()
        {
            Initi();
        }
        #endregion

        #region[Properties]
        public int SelectedTabRecid { get; set; }
        public int SelectedCategoryRecid { get; set; }
        public int SelectedProductRecid { get; set; }
        public bool SatusSaving { get; set; }
        public ObservableCollection<TabViewModel> Tabs { get; set; }
        public ObservableCollection<CategoryViewModel> Category { get; set; }
        public ObservableCollection<ProductViewModel> Products { get; set; }
        public ObservableCollection<OrderViewModel> Orders { get; set; }

        private string deleted_item = string.Empty;
        public string DELETED_ITEM
        {
            get
            {
                return deleted_item;
            }
            set
            {
                if (string.IsNullOrEmpty(deleted_item))
                {
                    deleted_item = string.Empty;
                    deleted_item += value;
                }
                else deleted_item += "," + value;

            }
        }
        public bool isEditMode { get; set; }
        #endregion

        #region[Methods]
        public static Presenter Instance()
        {
            if (presenter == null)
            {
                presenter = new Presenter();
            }
            return presenter;
        }
        public void Initi()
        {
            Tabs = new ObservableCollection<TabViewModel>();
            Category = new ObservableCollection<CategoryViewModel>();
            Products = new ObservableCollection<ProductViewModel>();
        }
        public bool CheckNameUnique<T>(string name)
        {
            bool result = false;
            List<string> names = new List<string>();
            if (typeof(T) == typeof(TabViewModel))
            {
                if (Tabs == null || Tabs.Count < 1) return true;
                names = Tabs.Select(x => x.TB_Name.ToUpper()).ToList<string>();
            }
            else if (typeof(T) == typeof(CategoryViewModel))
            {
                if (Category == null || Category.Count < 1) return true;
                names = Category.Select(x => x.CT_Name.ToUpper()).ToList<string>();
            }
            else if (typeof(T) == typeof(ProductViewModel))
            {
                if (Products == null || Products.Count < 1) return true;
                names = Products.Select(x => x.PR_Name.ToUpper()).ToList<string>();
            }
            if (names != null && !names.Any(x => x.Equals(name.ToUpper())))
                result = true;
            return result;
        }
        public ObservableCollection<CategoryViewModel> CategoryForSelectedTab(TabViewModel tab)
        {
            ObservableCollection<CategoryViewModel> category = tab.Categories;
            return category;
        }
        public ObservableCollection<ProductViewModel> ProductsForSelectedCategory(CategoryViewModel category)
        {
            ObservableCollection<ProductViewModel> products = category.Products;
            return products;
        }
        public CategoryViewModel GetSelectedCategory()
        {
            int id = SelectedCategoryRecid;
            if (Category == null || Category.Count < 1) return null;
            return Category.Where(x => x.CT_RECID == id).FirstOrDefault();
        }
        public TabViewModel GetSelectedTab()
        {
            int id = SelectedTabRecid;
            if (Tabs == null || Tabs.Count < 1) return null;
            return Tabs.Where(x => x.TB_RECID == id).FirstOrDefault();
        }
        public void AddRange<T>(ObservableCollection<T> ToCollection, ObservableCollection<T> FromCollection)
        {
            if (FromCollection == null) return;
            foreach (var item in FromCollection)
                ToCollection.Add(item);
        }

        public void ClearCollectionForBinding()
        {
            Products.Clear();
            Category.Clear();
            Tabs.Clear();
        }
        #endregion

        #region[EVENTS]

        public event EventHandler ClickEditMode;
        public event EventHandler ClickReadMode;
        public void Initi_ClickEditMode()
        {
            ClickEditMode(null, null);
        }
        public void Initi_ClickReadMode()
        {
            ClickReadMode(null, null);
        }

        #region[Products]
        public event EventHandler ClickOnProduct;
        public void Initi_ClickOnProduct(object obj)
        {
            ClickOnProduct(obj, null);
        }
        #endregion

        #region[Category]
        #endregion

        #region[Tabs]
        public event EventHandler ClickOnTabOrCategory;
        public void Initi_ClickOnTabOrCategory(object obj)
        {
            ClickOnTabOrCategory(obj, null);
        }
        #endregion

        #endregion
    }
}
