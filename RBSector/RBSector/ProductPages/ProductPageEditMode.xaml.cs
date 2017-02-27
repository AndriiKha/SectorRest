using RBSector.ImagePages;
using RBSector.OthersPages;
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
    public sealed partial class ProductPageEditMode : Page
    {
        private bool isEdit = false;

        #region[EVENTS]
        public event EventHandler Changing;
        private async void Changing_Page(object sender, EventArgs e)
        {

        }
        #endregion

        private EditPart PartEdit;
        private ProductService _srv_product;
        private CategoryService cat_srv;
        private Presenter _presenter;
        private ProductViewModel Product;
        private ContentDialog dialog;
        public ProductPageEditMode()
        {
            this.InitializeComponent();
            _srv_product = new ProductService();
            cat_srv = new CategoryService();

            _presenter = Presenter.Instance();

            PartEdit = EditPart.NONE;
            Changing += Changing_Page;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (_presenter.isEditMode)
            {
                bool isSave = false;
                ProductViewModel product = _srv_product.GetProduct(_presenter.SelectedProductRecid);
                if (product == null)
                {
                    if (_presenter.CheckNameUnique<ProductViewModel>(this.EditCreateName.Text))
                    {
                        isSave = _srv_product.CreateProduct(this.EditCreateName.Text, this.EditCreatePrice.Text);
                    }
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
                    Clear();
                    var category = cat_srv.GetCategory(_presenter.SelectedCategoryRecid);
                    if (category != null)
                        _srv_product.SetProductsToBindingModel(_presenter.ProductsForSelectedCategory(category));
                }
            }
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is ProductViewModel)
            {
                Product = e.Parameter as ProductViewModel;
                //txbl_NameObjToEditOrCtreat.Text = Product.PR_Name;
                EditCreateName.Text = Product.PR_Name;
                EditCreatePrice.Text = Product.Price.ToString();
                SelectedImage.Source = Product.Image;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }
        private async void btn_Image_Click(object sender, RoutedEventArgs e)
        {

            ImageViewPage imagePage = new ImageViewPage();
            await imagePage.ShowAsync();

            if (ProductService.SelectedImageForProduct != null)
            {
                this.SelectedImage.Source = ProductService.SelectedImageForProduct.bitmapImage;
                if (Product != null)
                {
                    _srv_product.Update(Product);
                }
            }
        }

        private void btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            if (_presenter.SelectedProductRecid != -1)
            {
                _srv_product.DeleteProduct(_presenter.SelectedProductRecid);
            }
        }

        private async void btn_Ingredients_Click(object sender, RoutedEventArgs e)
        {

            IngredientsPage ingredients = new IngredientsPage();
            //ingredients.NameProduct = Product.PR_Name;
            await ingredients.ShowAsync();
            if (Product != null)
            {
                //Product.IG_Description = ingredients.Ingredients.IG_Description;
                //_srv_product.Update(Product);
            }
        }
        private void Clear()
        {
            this.EditCreatePrice.Text = string.Empty;
            this.EditCreateName.Text = string.Empty;
            this.NameTextBlockEditCreate.Text = string.Empty;
            SelectedImage.Source = null;
        }
    }
}
