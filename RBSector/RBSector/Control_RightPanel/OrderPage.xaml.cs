using RBSector.OthersPages;
using RBSectorUWPBusinessLogic.JSonTools;
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

namespace RBSector.Control_RightPanel
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class OrderPage : Page
    {
        private MainSubmitService mn_srv;



        ObservableCollection<ProductViewModel> List;
        ProductService _product_srv;
        Presenter _presenter;
        OrderService _order_srv;
        OrderViewModel orderViewModel;
        UserService _user_service;
        public OrderPage()
        {
            this.InitializeComponent();
            mn_srv = new MainSubmitService();
            _presenter = Presenter.Instance();
            _order_srv = OrderService.Instance();
            _user_service = UserService.Instance();
            _order_srv.Saving += Save_Event;
            _product_srv = new ProductService();
            orderViewModel = _order_srv.Products_ORD;
            List = _order_srv.Products_ORD.Product_ORD;
            _order_srv.ChangingTotalMoney += ChangingTotalMoney_Event;
            _order_srv.EnableEvent += EnableEvent_Event;
            EnableEvent_Event(null, null);
        }
        private async void ChangingTotalMoney_Event(object obj, EventArgs e)
        {
            txbl_totalMoney.Text = orderViewModel.Ord_PriceCost.ToString();
        }
        private void EnableEvent_Event(object obj, EventArgs e)
        {
            if (_order_srv.Products_ORD.Product_ORD.Count > 0)
            {
                btn_Order.Visibility = Visibility.Visible;
                btn_Table.Visibility = Visibility.Visible;
                txbl_totalMoney.Visibility = Visibility.Visible;
            }
            else
            {
                btn_Order.Visibility = Visibility.Collapsed;
                btn_Table.Visibility = Visibility.Collapsed;
                txbl_totalMoney.Visibility = Visibility.Collapsed;
            }
            
        }
        private void ListOrderProducts_ItemClick(object sender, ItemClickEventArgs e)
        {
            ProductViewModel product = (ProductViewModel)e.ClickedItem;
            if (product != null)
                _order_srv.Delete(product);
            EnableEvent_Event(null, null);
        }

        private void UPorDOWN_btn_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            StackPanel element = (((((sender as Button).Parent) as StackPanel).Parent) as StackPanel);
            StackPanel objMoney = element.Parent as StackPanel;
            string productName = objMoney.Children.Where(x => (x is TextBlock) && (x as TextBlock).Name.Equals("Product_Name")).Select(x => (x as TextBlock).Text).FirstOrDefault();
            foreach (var item in element.Children)
            {
                if (item is TextBox)
                {
                    TextBox textBox = item as TextBox;
                    if (textBox != null)
                    {
                        int num;
                        if (string.IsNullOrEmpty(textBox.Text))
                            textBox.Text = "1";
                        if (Int32.TryParse(textBox.Text, out num))
                        {
                            if (clickedButton.Name.Contains("UP_btn"))
                            {
                                num++;
                                _order_srv.SetOrd_PriceCost(productName, num);
                            }
                            else if (clickedButton.Name.Contains("DOWN_btn"))
                            {
                                num--;
                                _order_srv.SetOrd_PriceCost(productName, num, false);
                                if (num < 1)
                                {
                                    num = 1;
                                }
                            }

                            textBox.Text = num.ToString();
                        }
                    }
                }
            }
        }
        private void Numeric_txb_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            if (!string.IsNullOrEmpty(sender.Text))
            {
                int number = 1;
                if (Int32.TryParse(sender.Text, out number))
                {
                    if (number < 1)
                        sender.Text = "1";
                }
                else sender.Text = "1";
            }
        }

        private async void btn_Order_Click(object sender, RoutedEventArgs e)
        {
            // PayPage payPage = new PayPage();
            // await payPage.ShowAsync();
            await (new BillingPage()).ShowAsync();
            if (orderViewModel != null && orderViewModel.Product_ORD != null && orderViewModel.Product_ORD.Count > 0)
                _order_srv.Initi_Saving();
        }
        private async void Save_Event(object product, EventArgs e)
        {
            if (orderViewModel != null)
            {
                orderViewModel.UserRecid = _user_service.user.USR_RECID;
                string json = JsonT.SerealizeObject(orderViewModel);
                if(await mn_srv.SaveOrder(json))
                {
                    _presenter.SatusSaving = true;
                    _order_srv.Initi_LoadingOrders();
                }
                _order_srv.Clear();
            }

        }

        private void btn_Table_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
