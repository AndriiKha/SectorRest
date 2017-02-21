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

namespace RBSector.TabsPages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class TabsViewPage : Page
    {
        private ObservableCollection<TabViewModel> Tabs;
        private Presenter _presenter;
        private CategoryService _category_srv;
        public TabsViewPage()
        {
            this.InitializeComponent();
            _presenter = Presenter.Instance();
            Tabs = _presenter.Tabs;
            _category_srv = new CategoryService();

            _presenter.ClickReadMode += ClickReadMode_Event;
            _presenter.ClickEditMode += ClickEditMode_Event;
        }

        private void GridView_ItemClickTabs(object sender, ItemClickEventArgs e)
        {
            TabViewModel tab = (TabViewModel)e.ClickedItem;
            if (tab != null)
            {

                _category_srv.SetCategoryToBindingModel(_presenter.CategoryForSelectedTab(tab));

                _presenter.SelectedProductRecid = -1;
                _presenter.SelectedTabRecid = tab.TB_RECID;
                if (_presenter.isEditMode)
                    _presenter.Initi_ClickOnTabOrCategory(tab);
            }
        }
        private void btn_EditCreateFrame_Click(object sender, RoutedEventArgs e)
        {
            _presenter.Initi_ClickOnTabOrCategory(EditPart.TAB);
        }
        private async void ClickEditMode_Event(object obj, EventArgs e)
        {
            btn_AddNewTab.Visibility = Visibility.Visible;
            //TabClickItem.ItemClick += GridView_ItemClickTabs;
        }
        private async void ClickReadMode_Event(object obj, EventArgs e)
        {
            btn_AddNewTab.Visibility = Visibility.Collapsed;
            //TabClickItem.ItemClick -= GridView_ItemClickTabs;
        }
    }
}
