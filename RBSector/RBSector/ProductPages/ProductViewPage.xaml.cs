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

namespace RBSector.ProductPages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProductViewPage : Page
    {
        private ObservableCollection<ProductViewModel> Products;
        private Presenter _presenter;
        private OrderService _order_srv;
        public ProductViewPage()
        {
            this.InitializeComponent();
            _presenter = Presenter.Instance();
            Products = _presenter.Products;
            _order_srv = OrderService.Instance();

            _presenter.ClickReadMode += ClickReadMode_Event;
            _presenter.ClickEditMode += ClickEditMode_Event;
        }
        private void GridView_ItemClickProduct(object sender, ItemClickEventArgs e)
        {
            ProductViewModel product = (ProductViewModel)e.ClickedItem;
            if (product != null)
            {
                _presenter.SelectedProductRecid = product.PR_RECID;
                txb_nameProbuct.Text = product.PR_Name;
                if (_presenter.isEditMode)
                    _presenter.Initi_ClickOnProduct(product);
                else _order_srv.Add(product);
            }
        }
        private void btn_AddProduct_Click(object sender, RoutedEventArgs e)
        {
            _presenter.SelectedProductRecid = -1;
            _presenter.Initi_ClickOnProduct(null);
        }
        private async void ClickEditMode_Event(object obj, EventArgs e)
        {
            btn_AddProduct.Visibility = Visibility.Visible;
            //  ProductClickItem.ItemClick += GridView_ItemClickProduct;
        }
        private async void ClickReadMode_Event(object obj, EventArgs e)
        {
            btn_AddProduct.Visibility = Visibility.Collapsed;
            // ProductClickItem.ItemClick -= GridView_ItemClickProduct;
        }
    }
}
