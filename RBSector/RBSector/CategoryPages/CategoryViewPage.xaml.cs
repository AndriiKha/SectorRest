using RBSector.Tools;
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
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace RBSector.CategoryPages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CategoryViewPage : Page
    {
        private Presenter _presenter;
        private ProductService _product_srv;
        private ObservableCollection<CategoryViewModel> Category;
        public CategoryViewPage()
        {
            this.InitializeComponent();
            _presenter = Presenter.Instance();
            _product_srv = new ProductService();
            Category = _presenter.Category;

            _presenter.ClickReadMode += ClickReadMode_Event;
            _presenter.ClickEditMode += ClickEditMode_Event;

        }
        private void GridView_ItemClickCategory(object sender, ItemClickEventArgs e)
        {
            CategoryViewModel category = (CategoryViewModel)e.ClickedItem;
            if (category != null)
            {

                txb_nameCategory.Text = category.CT_Name;
                _product_srv.SetProductsToBindingModel(_presenter.ProductsForSelectedCategory(category));

                _presenter.SelectedProductRecid = -1;
                _presenter.SelectedCategoryRecid = category.CT_RECID;

                if (_presenter.isEditMode)
                    _presenter.Initi_ClickOnTabOrCategory(category);
            }
        }
        private void btn_EditCreateFrame_Click(object sender, RoutedEventArgs e)
        {
            _presenter.Initi_ClickOnTabOrCategory(EditPart.CATEGORY);
        }
        private async void ClickEditMode_Event(object obj, EventArgs e)
        {
            btn_NewCategory.Visibility = Visibility.Visible;
            //CategoryClickItem.ItemClick += GridView_ItemClickCategory;
        }
        private async void ClickReadMode_Event(object obj, EventArgs e)
        {
            btn_NewCategory.Visibility = Visibility.Collapsed;
            //CategoryClickItem.ItemClick -= GridView_ItemClickCategory;
        }
    }
}
