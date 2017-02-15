using Newtonsoft.Json;
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

namespace RBSector
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EditPage : Page
    {
        private EditPart PartEdit;
        private TabsService tb_srv;
        private CategoryService cat_srv;

        private bool isEdit = false;

        string FirtsChangeName = string.Empty;

        public EditPage()
        {
            this.InitializeComponent();
            tb_srv = new TabsService();
            cat_srv = new CategoryService();
            PartEdit = EditPart.NONE;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {

            bool result = false;
            if (PartEdit == EditPart.TAB)
            {
                if (!isEdit)
                    result = tb_srv.CreateTab(EditCreateName.Text);
                else result = tb_srv.Update(FirtsChangeName, EditCreateName.Text);
            }
            else if (PartEdit == EditPart.CATEGORY)
            {
                if (BindingModel.SelectedTab != -1)
                {
                    if (!isEdit)
                        result = cat_srv.CreateCategory(EditCreateName.Text);
                    else result = cat_srv.Update(FirtsChangeName, EditCreateName.Text);
                }
            }
            else if (PartEdit == EditPart.PRODUCT) { }

            if (!result)
            {
                EditCreateName.Text = "Faild Save!!!";
            }
            else
            {
                Clear();
            }

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter is EditPart)
            {
                PartEdit = (EditPart)e.Parameter;
            }
            else if (e.Parameter is TabViewModel)
            {
                isEdit = true;
                PartEdit = EditPart.TAB;
                txbl_NameObjToEditOrCreate.Text = EditCreateName.Text = (e.Parameter as TabViewModel).TB_Name;
                FirtsChangeName = EditCreateName.Text;
            }
            else if (e.Parameter is CategoryViewModel)
            {
                isEdit = true;
                PartEdit = EditPart.CATEGORY;
                txbl_NameObjToEditOrCreate.Text = EditCreateName.Text = (e.Parameter as CategoryViewModel).CT_Name;
                FirtsChangeName = EditCreateName.Text;
            }
        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            this.Clear();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            bool result = false;
            if (PartEdit == EditPart.TAB)
            {
                result = tb_srv.Delete(BindingModel.SelectedTab);
            }
            else if (PartEdit == EditPart.CATEGORY)
            {
                result = cat_srv.Delete(BindingModel.SelectedCategory);
            }
            if(result) this.Clear();
            else EditCreateName.Text = "Faild Delete!!!";
        }
        private void Clear()
        {
            this.EditCreateName.Text = string.Empty;
        }

        private void EditCreateName_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            txbl_NameObjToEditOrCreate.Text = EditCreateName.Text;
        }
    }
}
