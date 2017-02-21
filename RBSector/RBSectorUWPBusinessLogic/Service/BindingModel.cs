using RBSectorUWPBusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSectorUWPBusinessLogic.Service
{
    public static class BindingModel
    {
        public static int SelectedTab { get; set; }
        public static int SelectedCategory { get; set; }
        public static int SelectedProduct { get; set; }

        public static ObservableCollection<TabViewModel> Tabs { get; set; }
        public static ObservableCollection<CategoryViewModel> Category { get; set; }
        public static ObservableCollection<ProductViewModel> Products { get; set; }

        private static string deleted_item = string.Empty;
        public static string DELETED_ITEM
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
        public static void Initi()
        {
            Tabs = new ObservableCollection<TabViewModel>();
            Category = new ObservableCollection<CategoryViewModel>();
            Products = new ObservableCollection<ProductViewModel>();
        }

        public static ObservableCollection<CategoryViewModel> CategoryForSelectedTab(this TabViewModel tab)
        {
            ObservableCollection<CategoryViewModel> category = tab.Categories;
            return category;
        }
        public static ObservableCollection<ProductViewModel> ProductsForSelectedCategory(this CategoryViewModel category)
        {
            ObservableCollection<ProductViewModel> products = category.Products;
            return products;
        }
        public static bool CheckNameUnique<T>(T type, string name)
        {
            bool result = false;
            List<string> names = new List<string>();
            if (type is TabViewModel)
            {
                if (Tabs == null || Tabs.Count < 1) return true;
                names = Tabs.Select(x => x.TB_Name.ToUpper()).ToList<string>();
            }
            else if (type is CategoryViewModel)
            {
                if (Category == null || Category.Count < 1) return true;
                names = Category.Select(x => x.CT_Name.ToUpper()).ToList<string>();
            }
            else if (type is ProductViewModel)
            {
                if (Products == null || Products.Count < 1) return true;
                names = Products.Select(x => x.PR_Name.ToUpper()).ToList<string>();
            }
            if (names != null && !names.Any(x => x.Equals(name.ToUpper())))
                result = true;
            return result;
        }
        public static CategoryViewModel GetParentCategory()
        {
            int id = SelectedCategory;
            if (Category == null || Category.Count < 1) return null;
            return Category.Where(x => x.CT_RECID == id).FirstOrDefault();
        }
        public static TabViewModel GetParentTab()
        {
            int id = SelectedTab;
            if (Tabs == null || Tabs.Count < 1) return null;
            return Tabs.Where(x => x.TB_RECID == id).FirstOrDefault();
        }
        public static void AddRange<T>(this ObservableCollection<T> collection, ObservableCollection<T> addCollection)
        {
            if (addCollection == null) return;
            foreach (var item in addCollection)
                collection.Add(item);
        }
    }
}
