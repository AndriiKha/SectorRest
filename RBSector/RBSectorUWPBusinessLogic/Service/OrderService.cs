using RBSectorUWPBusinessLogic.JSonTools;
using RBSectorUWPBusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSectorUWPBusinessLogic.Service
{
    public class OrderService
    {
        #region[Service]
        private OrdersServiceClient.OrdersServiceClient orders_srv;
        #endregion

        private static OrderService _srv_order;
        public OrderViewModel Products_ORD { get; private set; }
        private Presenter _presenter;
        //private UserService user_srv;

        #region[Events]
        public event EventHandler ChangingNumberCalculator;
        public event EventHandler ChangingTotalMoney;
        public event EventHandler ChangingFrameBilling;
        public event EventHandler Saving;
        public event EventHandler EnableEvent;
        public event EventHandler LoadingOrders;

        public void Initi_ChangingNumberCalculator(string item)
        {
            ChangingNumberCalculator(item, null);
        }
        public void Initi_ChangingTotalMoney(string item)
        {
            ChangingTotalMoney(item, null);
        }
        public void Initi_ChangingFrameBilling()
        {
            ChangingFrameBilling(null, null);
        }
        public void Initi_Saving()
        {
            Saving(null, null);
        }
        public void Initi_EnableEvent()
        {
            EnableEvent(null, null);
        }
        public void Initi_LoadingOrders()
        {
            LoadingOrders(null, null);
        }
        private async void ClickLoadingOrders_Event(object product, EventArgs e)
        {
            string json = await orders_srv.GetOrdersAsync();
             _presenter.Orders = JsonT.DeserealizeOrders(json);
        }
        #endregion
        private OrderService()
        {
            Products_ORD = new OrderViewModel();
            orders_srv = new OrdersServiceClient.OrdersServiceClient(OrdersServiceClient.OrdersServiceClient.EndpointConfiguration.BasicHttpBinding_IOrdersService);
            //user_srv = UserService.Instance();
            _presenter = Presenter.Instance();
            //Products_ORD.UserRecid = user_srv.user.USR_RECID;
            LoadingOrders += ClickLoadingOrders_Event;
        }

        public static OrderService Instance()
        {
            if (_srv_order == null)
            {
                _srv_order = new OrderService();
            }
            return _srv_order;
        }
        public void Clear()
        {
            Products_ORD.Product_ORD.Clear();
            Initi_EnableEvent();
        }
        private ProductViewModel Get(ProductViewModel product)
        {
            if (product == null) return null;
            return Products_ORD.Product_ORD.Where(y => y.PR_RECID == product.PR_RECID).FirstOrDefault();
        }
        public void SetOrd_PriceCost(string nameProduct, int count, bool add = true)
        {
            if (string.IsNullOrEmpty(nameProduct)) return;
            var product = Products_ORD.Product_ORD.Where(y => y.PR_Name == nameProduct).FirstOrDefault();
            product.ORD_Count = count;
            if (add)
                Products_ORD.Ord_PriceCost += product.Price;
            else Products_ORD.Ord_PriceCost -= product.Price;
            Initi_ChangingTotalMoney(Products_ORD.Ord_PriceCost.ToString());
        }
        public void Add(ProductViewModel product)
        {
            var product_ord = Get(product);
            if (product_ord == null)
            {
                ProductViewModel pr = product;
                pr.ORD_Count = 1;
                Products_ORD.Product_ORD.Add((pr));
                Products_ORD.Ord_PriceCost += product.Price;
            }
            else
            {
                //Products_ORD.Product_ORD.Where(y => y.PR_RECID == product.PR_RECID).FirstOrDefault().ORD_Count++;
                //Products_ORD.Ord_PriceCost += product.Price;
            }
            _srv_order.Initi_ChangingTotalMoney(null);
        }
        public void Delete(ProductViewModel product)
        {
            var product_ord = Get(product);
            if (product_ord != null)
            {
                Products_ORD.Product_ORD.Remove(product_ord);
                Products_ORD.Ord_PriceCost -= product.Price * product.ORD_Count;
                _srv_order.Initi_ChangingTotalMoney(null);
            }
        }
    }
}
