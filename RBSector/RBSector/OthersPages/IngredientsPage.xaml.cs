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

namespace RBSector.OthersPages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class IngredientsPage : ContentDialog
    {
        //public string NameProduct {
           // get { return this.tb_NameProduct.Text; }
         //   set { this.tb_NameProduct.Text = value; }
       // }
        //public IngredientViewModel Ingredients { get; set; }
        public IngredientsPage()
        {
            this.InitializeComponent();
        }

        private void SaveIngredients_Click(object sender, RoutedEventArgs e)
        {
            //string text;
            //Description_rchb.Document.GetText(Windows.UI.Text.TextGetOptions.FormatRtf, out text);
            //Ingredients.IG_Description = text;
            this.Hide();
        }
    }
}
