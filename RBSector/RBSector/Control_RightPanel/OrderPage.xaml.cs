﻿using RBSector.OthersPages;
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
        public OrderPage()
        {
            this.InitializeComponent();
            mn_srv = new MainSubmitService();
            _presenter = Presenter.Instance();
            _order_srv = OrderService.Instance();
            _order_srv.Saving += Save_Event;
            _product_srv = new ProductService();
            orderViewModel = _order_srv.Products_ORD;
            List = _order_srv.Products_ORD.Product_ORD;
            _order_srv.ChangingTotalMoney += ChangingTotalMoney_Event;
        }
        private async void ChangingTotalMoney_Event(object obj, EventArgs e)
        {
            txbl_totalMoney.Text = orderViewModel.Ord_PriceCost.ToString();
        }
        private void ListOrderProducts_ItemClick(object sender, ItemClickEventArgs e)
        {
            ProductViewModel product = (ProductViewModel)e.ClickedItem;
            if (product != null)
                _order_srv.Delete(product);
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
                string json = JsonT.SerealizeObject(orderViewModel);
                _presenter.SatusSaving = await mn_srv.SaveOrder(json);
            }

        }

        private void btn_Table_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}