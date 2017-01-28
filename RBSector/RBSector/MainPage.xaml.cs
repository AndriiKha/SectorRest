using RBSector.ProductPages;
using RBSector.Tools;
using RBSectorUWPBusinessLogic.JSonTools;
using RBSectorUWPBusinessLogic.Options;
using RBSectorUWPBusinessLogic.Service;
using RBSectorUWPBusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RBSector
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region[Options]
        private bool is_editMode = OptionsAvailability.IS_EDITMODE;
        private bool can_addCategory = OptionsAvailability.IS_EDITMODE && OptionsAvailability.CAN_ADDCATEGORY;
        private bool can_addProduct = OptionsAvailability.IS_EDITMODE && OptionsAvailability.CAN_ADDPRODUCTS;
        private Visibility EDITMODE;
        private Visibility CANADDCATEGORY;
        private Visibility CANADDPRODUCT;
        #endregion

        #region[Initi Service]
        private TabsService tb_srv;
        private CategoryService cat_srv;
        private ProductService prod_srv;
        private MainSubmitService mn_srv;
        #endregion

        #region[Binding Model]
        private ObservableCollection<TabViewModel> Tabs;
        private ObservableCollection<CategoryViewModel> Category;
        private ObservableCollection<ProductViewModel> Products;
        #endregion
        public MainPage()
        {
            this.InitializeComponent();
            is_editMode = true;
            EDITMODE = Visibility.Visible;
            CANADDCATEGORY = Visibility.Visible;
            CANADDPRODUCT = Visibility.Visible;

            tb_srv = new TabsService();
            mn_srv = new MainSubmitService();
            cat_srv = new CategoryService();
            prod_srv = new ProductService();

            BindingModel.Initi();
            tb_srv.SetTabsToBindingModel(tb_srv.GetAllTabs());
            Tabs = BindingModel.Tabs;
            Category = BindingModel.Category;
            Products = BindingModel.Products;
        }
        private void btn_EditCreateFrame_Click(object sender, RoutedEventArgs e)
        {
            //switch()
            EditPart selectedPartobj = Tools.Tools.EditObj(sender);
            this.FrameEditCreate.Navigate(typeof(EditPage), selectedPartobj);
        }

        private void btn_editMode_Click(object sender, RoutedEventArgs e)
        {
            string json = JsonT.SerealizeObjWithComponent(Tabs);
            if (mn_srv.SaveResult(json))
            {
                btn_editMode.Content = "Edit";
                is_editMode = false;
                EDITMODE = Visibility.Collapsed;
            }
        }

        private void GridView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }

        private void GridView_ItemClickTabs(object sender, ItemClickEventArgs e)
        {
            TabViewModel tab = (TabViewModel)e.ClickedItem;
            can_addCategory = true;
            CANADDCATEGORY = Visibility.Visible;
            if (tab != null)
            {
                if (BindingModel.SelectedTab != tab.TB_RECID)
                {
                    cat_srv.SetCategoryToBindingModel(tab.CategoryForSelectedTab());
                    BindingModel.SelectedTab = tab.TB_RECID;
                }
            }
        }

        private void GridView_ItemClickCategory(object sender, ItemClickEventArgs e)
        {
            CategoryViewModel category = (CategoryViewModel)e.ClickedItem;
            can_addProduct = true;
            CANADDPRODUCT = Visibility.Visible;
            if (category != null)
            {
                if (BindingModel.SelectedCategory != category.CT_RECID)
                {
                    prod_srv.SetProductsToBindingModel(category.ProductsForSelectedCategory());
                    BindingModel.SelectedCategory = category.CT_RECID;
                }
            }
        }

        private void GridView_ItemClickProduct(object sender, ItemClickEventArgs e)
        {

        }

        private void btn_AddProduct_Click(object sender, RoutedEventArgs e)
        {
            EditPart selectedPartobj = Tools.Tools.EditObj(sender);
            this.FrameEditCreate.Navigate(typeof(ProductPage), selectedPartobj);
        }

        private void listView_Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            CategoryViewModel category = (CategoryViewModel)e.ClickedItem;
            can_addProduct = true;
            CANADDPRODUCT = Visibility.Visible;
            if (category != null)
            {
                if (BindingModel.SelectedCategory != category.CT_RECID)
                {
                    prod_srv.SetProductsToBindingModel(category.ProductsForSelectedCategory());
                    BindingModel.SelectedCategory = category.CT_RECID;
                }
            }
        }
    }
}
