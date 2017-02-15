using RBSector.ImagePages;
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
        private ImageService im_srv;
        #endregion

        #region[Binding Model]
        private ObservableCollection<TabViewModel> Tabs;
        private ObservableCollection<CategoryViewModel> Category;
        private ObservableCollection<ProductViewModel> Products;
        #endregion

        #region[Events]
        public event EventHandler Loading;
        private async void LoadImageForProducts_Page_Loading(object sender, EventArgs e)
        {
            int countTabs = BindingModel.Tabs.Count;
            for(int i = 0; i < countTabs; i++)
            {
                int countCategory = BindingModel.Tabs[i].Categories.Count;
                for(int j = 0; j < countCategory; j++)
                { 
                    for(int l = 0; l < BindingModel.Tabs[i].Categories[j].Products.Count; l++)
                    {
                        BindingModel.Tabs[i].Categories[j].Products[l].Image = await im_srv.GetImage(BindingModel.Tabs[i].Categories[j].Products[l].IM_Byte);
                    }
                }
            }

        }
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
            im_srv = new ImageService();

            BindingModel.Initi();
            tb_srv.SetTabsToBindingModel(tb_srv.GetAllTabs());
            Tabs = BindingModel.Tabs;
            Category = BindingModel.Category;
            Products = BindingModel.Products;

            Loading += LoadImageForProducts_Page_Loading;
            Loading(null, null);
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
            string i = ImageService.ByteToStringForDB(Tabs.FirstOrDefault().Categories.FirstOrDefault().Products.LastOrDefault().IM_Byte);

            if (mn_srv.SaveResult(json, BindingModel.DELETED_ITEM))
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
                BindingModel.SelectedProduct = -1;
                cat_srv.SetCategoryToBindingModel(tab.CategoryForSelectedTab());
                BindingModel.SelectedTab = tab.TB_RECID;
                this.FrameEditCreate.Navigate(typeof(EditPage), tab);
            }
        }

        private void GridView_ItemClickCategory(object sender, ItemClickEventArgs e)
        {
            CategoryViewModel category = (CategoryViewModel)e.ClickedItem;
            can_addProduct = true;
            CANADDPRODUCT = Visibility.Visible;
            if (category != null)
            {
                BindingModel.SelectedProduct = -1;
                txb_nameCategory.Text = category.CT_Name;
                prod_srv.SetProductsToBindingModel(category.ProductsForSelectedCategory());
                BindingModel.SelectedCategory = category.CT_RECID;

                this.FrameEditCreate.Navigate(typeof(EditPage), category);
            }
        }

        private void GridView_ItemClickProduct(object sender, ItemClickEventArgs e)
        {
            ProductViewModel product = (ProductViewModel)e.ClickedItem;
            if (product != null)
            {
                BindingModel.SelectedProduct = product.PR_RECID;
                txb_nameProbuct.Text = product.PR_Name;
                this.FrameEditCreate.Navigate(typeof(ProductPage), product);
            }
        }

        private void btn_AddProduct_Click(object sender, RoutedEventArgs e)
        {
            BindingModel.SelectedProduct = -1;
            EditPart selectedPartobj = Tools.Tools.EditObj(sender);
            this.FrameEditCreate.Navigate(typeof(ProductPage), selectedPartobj);
        }

        private void listView_Category_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*CategoryViewModel category = (CategoryViewModel)e.ClickedItem;
            can_addProduct = true;
            CANADDPRODUCT = Visibility.Visible;
            if (category != null)
            {
                if (BindingModel.SelectedCategory != category.CT_RECID)
                {
                    prod_srv.SetProductsToBindingModel(category.ProductsForSelectedCategory());
                    BindingModel.SelectedCategory = category.CT_RECID;
                }
            }*/
        }
    }
}
