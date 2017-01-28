using RBSector.Tools;
using RBSectorUWPBusinessLogic.Service;
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
        private EditPart PartEdit;
        private ProductService _srv_product;
        public ProductPage()
        {
            this.InitializeComponent();
            _srv_product = new ProductService();
            PartEdit = EditPart.NONE;
            Text = PartEdit.ToString();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
           bool isSave = _srv_product.CreateProduct(this.EditCreateName.Text, this.EditCreatePrice.Text);
            if (!isSave) this.NameTextBlockEditCreate.Text = "Feild name!!!";
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            PartEdit = (EditPart)e.Parameter;
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
