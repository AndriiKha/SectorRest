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
        public EditPage()
        {
            this.InitializeComponent();
            tb_srv = new TabsService();
            cat_srv = new CategoryService();
            PartEdit = EditPart.NONE;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            bool isUniqueName = false;
            if (PartEdit == EditPart.TAB) {
                TabViewModel tab = new TabViewModel();
                tab.TB_RECID = tb_srv.GenerateNextTabID;
                tab.TB_Name = EditCreateName.Text;
                tab.isModify = true;
                isUniqueName = BindingModel.CheckNameUnique(typeof(TabViewModel),tab.TB_Name);
                if (isUniqueName)
                {
                    tb_srv.SetTabsSingleToBindingModel(tab);
                    EditCreateName.Text = string.Empty;
                }
                else
                {
                    EditCreateName.Text = "Faild Name!!!";
                }
            }
            else if (PartEdit == EditPart.CATEGORY)
            {
                if (BindingModel.SelectedTab!=-1)
                {
                    CategoryViewModel category = new CategoryViewModel();
                    category.CT_Name = EditCreateName.Text;
                    category.CT_RECID = cat_srv.GenerateNextCategoryID;
                    category.TabParent = BindingModel.GetParentTab();
                    category.isModify = true;
                    category.TabParent.Categories.Add(category);
                    isUniqueName = BindingModel.CheckNameUnique(typeof(TabViewModel), category.CT_Name);
                    if (isUniqueName)
                    {
                        cat_srv.SetCategorySingleToBindingModel(category);
                        EditCreateName.Text = string.Empty;
                    }
                    else
                    {
                        EditCreateName.Text = "Faild Name!!!";
                    }
                }
            }
            else if(PartEdit == EditPart.PRODUCT) { }
           
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            PartEdit = (EditPart)e.Parameter;
        }
    }
}
