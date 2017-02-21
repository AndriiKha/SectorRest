using RBSector.CategoryPages;
using RBSector.Control_RightPanel;
using RBSector.EditModePages;
using RBSector.ImagePages;
using RBSector.ProductPages;
using RBSector.TabsPages;
using RBSector.Tools;
using RBSectorUWPBusinessLogic.Interface;
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

        #endregion

        #region[Initi Service]

        private Presenter _presenter;

        private TabsService tb_srv;
        private CategoryService cat_srv;
        private ProductService prod_srv;
        private MainSubmitService mn_srv;
        private ImageService im_srv;
        #endregion

        #region[Binding Model]

        #endregion

        #region[Events]
        public event EventHandler Loading;
        public event EventHandler Saving;
        private async void LoadImageForProducts_Page_Loading(object sender, EventArgs e)
        {
            int countTabs = _presenter.Tabs.Count;
            for (int i = 0; i < countTabs; i++)
            {
                int countCategory = _presenter.Tabs[i].Categories.Count;
                for (int j = 0; j < countCategory; j++)
                {
                    for (int l = 0; l < _presenter.Tabs[i].Categories[j].Products.Count; l++)
                    {
                        _presenter.Tabs[i].Categories[j].Products[l].Image = await im_srv.GetImage(_presenter.Tabs[i].Categories[j].Products[l].IM_Byte);
                    }
                }
            }

        }

        private async void ClickOnProduct_Event(object product, EventArgs e)
        {
            this.FrameEditCreate.Navigate(typeof(ProductPageEditMode), product);
        }
        private async void ClickOnTabOrCategory_Event(object obj, EventArgs e)
        {
            this.FrameEditCreate.Navigate(typeof(EditPage), obj);
        }

        private async void ClickEditMode_Event(object obj, EventArgs e)
        {
            _presenter.isEditMode = true;
            btn_editMode.Content = "Save";
        }
        private async void ClickReadMode_Event(object obj, EventArgs e)
        {
            _presenter.isEditMode = false;
            btn_editMode.Content = "Edit";
            FrameEditCreate.Navigate(typeof(OrderPage));
        }
        private async void Save_Event(object product, EventArgs e)
        {
            string json = JsonT.SerealizeObjWithComponent(_presenter.Tabs);
            _presenter.SatusSaving = await mn_srv.SaveResult(json, _presenter.DELETED_ITEM);

        }
        private void InitiEvents()
        {
            Loading += LoadImageForProducts_Page_Loading;
            Saving += Save_Event;
            _presenter.ClickOnProduct += ClickOnProduct_Event;
            _presenter.ClickOnTabOrCategory += ClickOnTabOrCategory_Event;
        }
        #endregion
        public MainPage()
        {
            this.InitializeComponent();


            tb_srv = new TabsService();
            mn_srv = new MainSubmitService();
            cat_srv = new CategoryService();
            prod_srv = new ProductService();
            im_srv = new ImageService();

            _presenter = Presenter.Instance();

            tb_srv.SetTabsToBindingModel(tb_srv.GetAllTabs());

            this.ProductPageView.Navigate(typeof(ProductViewPage));
            this.CategoryViewPage.Navigate(typeof(CategoryViewPage));
            this.TabsViewPage.Navigate(typeof(TabsViewPage));

            _presenter.ClickReadMode += ClickReadMode_Event;
            _presenter.ClickEditMode += ClickEditMode_Event;
            _presenter.Initi_ClickReadMode();

            InitiEvents();
            Loading(null, null);
        }

        private void btn_editMode_Click(object sender, RoutedEventArgs e)
        {
            if (_presenter.isEditMode)
            {
                Saving(null, null);
                if (_presenter.SatusSaving)
                {
                    _presenter.Initi_ClickReadMode();
                    Reload_TCP();
                }
            }
            else {
                _presenter.Initi_ClickEditMode();
            }
        }
        #region[METHODS]
        private void Reload_TCP()
        {
            _presenter.ClearCollectionForBinding();
            tb_srv.SetTabsToBindingModel(tb_srv.GetAllTabs());
            Loading(null, null);
        }
        #endregion
    }
}
