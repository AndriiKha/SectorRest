using RBSector.ImagePages;
using RBSector.Tools;
using RBSectorUWPBusinessLogic.Service;
using RBSectorUWPBusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
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
    public sealed partial class ProductPage : Page
    {
        #region[Biding]
        private string Text { get; set; }
        #endregion

        #region[EVENTS]
        public event EventHandler Changing;
        private async void Changing_Page(object sender, EventArgs e)
        {

        }
        #endregion

        private EditPart PartEdit;
        private ProductService _srv_product;
        private CategoryService cat_srv;

        private ContentDialog dialog;
        public ProductPage()
        {
            this.InitializeComponent();
            _srv_product = new ProductService();
            cat_srv = new CategoryService();
            PartEdit = EditPart.NONE;
            Text = PartEdit.ToString();
            Changing += Changing_Page;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            bool isSave = false;
            ProductViewModel product = _srv_product.GetProduct(BindingModel.SelectedProduct);
            if (product == null)
            {
                isSave = _srv_product.CreateProduct(this.EditCreateName.Text, this.EditCreatePrice.Text);
            }
            else
            {
                product.PR_Name = this.EditCreateName.Text;
                product.Price = Decimal.Parse(this.EditCreatePrice.Text);
                isSave = _srv_product.Update(product);
            }
            if (!isSave) this.NameTextBlockEditCreate.Text = "Feild name!!!";
            else
            {
                var category = cat_srv.GetCategory(BindingModel.SelectedCategory);
                if (category != null)
                    _srv_product.SetProductsToBindingModel(category.ProductsForSelectedCategory());
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is EditPart)
                if (e.Parameter != null)
                    PartEdit = (EditPart)e.Parameter;
            if (e.Parameter is ProductViewModel)
            {
                ProductViewModel product = e.Parameter as ProductViewModel;
                txbl_NameObjToEditOrCtreat.Text = product.PR_Name;
                EditCreateName.Text = product.PR_Name;
                EditCreatePrice.Text = product.Price.ToString();
                SelectedImage.Source = product.Image;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private async void btn_Image_Click(object sender, RoutedEventArgs e)
        {
            ImageViewPage imagePage = new ImageViewPage();
            await imagePage.ShowAsync();

            ProductViewModel product = _srv_product.GetProduct(BindingModel.SelectedProduct);
            this.SelectedImage.Source = ProductService.SelectedImageForProduct.bitmapImage;
            _srv_product.Update(product);
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (BindingModel.SelectedProduct != -1)
            {
                _srv_product.DeleteProduct(BindingModel.SelectedProduct);
            }
        }
    }
}
