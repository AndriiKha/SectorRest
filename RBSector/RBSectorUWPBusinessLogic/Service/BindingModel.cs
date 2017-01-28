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

        public static ObservableCollection<TabViewModel> Tabs { get; set; }
        public static ObservableCollection<CategoryViewModel> Category { get; set; }
        public static ObservableCollection<ProductViewModel> Products { get; set; }
        public static void Initi()
        {
            Tabs = new ObservableCollection<TabViewModel>();
            Category = new ObservableCollection<CategoryViewModel>();
            Products = new ObservableCollection<ProductViewModel>();
        }
        /*public static void SetTabs(ObservableCollection<TabViewModel> tabs)
        {
            if (Tabs.Count > 0)
                Tabs.Clear();
            foreach (var item in tabs)
            {
                if (Tabs.Where(x => x.TB_RECID == item.TB_RECID).FirstOrDefault() == null)
                    Tabs.Add(item);
            }
        }*/
        /*public static void SetTabsSingle(TabViewModel tab)
        {
            if (Tabs.Where(x => x.TB_RECID == tab.TB_RECID).FirstOrDefault() == null)
                Tabs.Add(tab);
        }*/
        /*public static void SetCategory(ObservableCollection<CategoryViewModel> category)
        {
            if (Category.Count > 0)
                Category.Clear();
            foreach (var item in category)
            {
                if (Category.Where(x => x.CT_RECID == item.CT_RECID).FirstOrDefault() == null)
                    Category.Add(item);
            }
        }*/
        /*public static void SetCategorySingle(CategoryViewModel category)
        {
            if (Category.Where(x => x.CT_RECID == category.CT_RECID).FirstOrDefault() == null)
                Category.Add(category);
        }*/
        /*public static void SetProducts(ObservableCollection<ProductViewModel> products)
        {
            if (Products.Count > 0)
                Products.Clear();
            foreach (var item in products)
            {
                if (Products.Where(x => x.PR_RECID == item.PR_RECID).FirstOrDefault() == null)
                    Products.Add(item);
            }
        }
        public static void SetProductsSingle(ProductViewModel product)
        {
            if (Products.Where(x => x.PR_RECID == product.PR_RECID).FirstOrDefault() == null)
                Products.Add(product);
        }*/
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
        /*public static int GenerateNextTabID { get { return Tabs.Select(x => x.TB_RECID) != null ? (Tabs.Select(x => x.TB_RECID).Max() + 1) : default(int); } }*/
        // public static int GenerateNextCategoryID { get { return (GetAllCategory().Select(x => x.CT_RECID).Max() + 1); } }
        //public static int GenerateNextProductID { get { return (GetAllProducts().Select(x => x.PR_RECID).Max() + 1); } }

        public static bool CheckNameUnique<T>(this T type, string name)
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
        /*public static TabViewModel GetTab(int id)
        {
            if (Tabs == null || Tabs.Count < 1) return null;
            return Tabs.Where(x => x.TB_RECID == id).FirstOrDefault();
        }*/
        /*public static CategoryViewModel GetCategory(int id)
        {
            if (Category == null || Category.Count < 1) return null;
            return Category.Where(x => x.CT_RECID == id).FirstOrDefault();
        }*/

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

        /*public static ObservableCollection<CategoryViewModel> GetAllCategory()
        {
            ObservableCollection<CategoryViewModel> category = new ObservableCollection<CategoryViewModel>();
            foreach (var tab in Tabs)
            {
                category.AddRange(tab.Categories);
            }
            return category;
        }*/
        /*public static ObservableCollection<ProductViewModel> GetAllProducts()
        {
            ObservableCollection<ProductViewModel> products = new ObservableCollection<ProductViewModel>();
            foreach (var cat in Category)
            {
                products.AddRange(cat.Products);
            }
            return products;
        }*/

        public static void AddRange<T>(this ObservableCollection<T> collection, ObservableCollection<T> addCollection)
        {
            if (addCollection == null) return;
            foreach (var item in addCollection)
                collection.Add(item);
        }
    }
}
