using Newtonsoft.Json;
using RBSectorUWPBusinessLogic.Service;
using RBSectorUWPBusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace RBSectorUWPBusinessLogic.JSonTools
{
    public static class JsonT
    {
        public static ObservableCollection<TabViewModel> TabsDeserialize(this string json, Type type, ObservableCollection<ImageViewModel> images = null)
        {
            if (type == typeof(TabViewModel))
            {
                ObservableCollection<TabViewModel> tabViewModel = new ObservableCollection<TabViewModel>();
                if (string.IsNullOrEmpty(json)) return tabViewModel;
                JsonObject objTabs = JsonValue.Parse(json).GetObject();
                foreach (var itemTab in objTabs["Tabs"].GetArray())
                {
                    TabViewModel tbvm = new TabViewModel();
                    JsonObject tab = itemTab.GetObject();
                    if (tab.ContainsKey("TbRecid"))
                    {
                        string recid = tab["TbRecid"].ToString().Trim('\"');
                        tbvm.TB_RECID = Convert.ToInt32(recid);
                        tbvm.Status = STATUS.Nothing.ToString();
                    }
                    if (tab.ContainsKey("TbName"))
                    {
                        tbvm.TB_Name = tab["TbName"].ToString().Trim('\"');
                        if (string.IsNullOrEmpty(tbvm.TB_Name)) continue;
                    }
                    if (tab.ContainsKey("Categories"))
                    {
                        foreach (var categoryJson in tab["Categories"].GetArray())
                        {
                            tbvm.Categories.Add(CategoryDeserialize(categoryJson.ToString(), tbvm, images));
                        }
                    }
                    tabViewModel.Add(tbvm);

                }
                return tabViewModel;
                //t["Tabs"].GetArray()[0].GetObject()["TbRecid"]
            }
            return null;
        }
        public static CategoryViewModel CategoryDeserialize(this string json, TabViewModel tabParent = null, ObservableCollection<ImageViewModel> images = null)
        {
            CategoryViewModel category = new CategoryViewModel();
            JsonObject objCategory = JsonValue.Parse(json).GetObject();
            if (objCategory.ContainsKey("CtRecid"))
            {
                string recid = objCategory["CtRecid"].ToString().Trim('\"');
                category.CT_RECID = Convert.ToInt32(recid);
                category.Status = STATUS.Nothing.ToString();
            }
            if (objCategory.ContainsKey("CtName"))
                category.CT_Name = objCategory["CtName"].ToString().Trim('\"');
            if (objCategory.ContainsKey("Products"))
            {
                foreach (var productJson in objCategory["Products"].GetArray())
                {
                    category.Products.Add(ProductDeserialize(productJson.ToString(), category, tabParent, images));
                }
            }
            category.TabParent = tabParent;
            return category;
        }
        public static ProductViewModel ProductDeserialize(this string json, CategoryViewModel categoryParent = null, TabViewModel tabParent = null, ObservableCollection<ImageViewModel> images = null)
        {
            ImageService im_srv = new ImageService();
            ProductViewModel product = new ProductViewModel();
            JsonObject objProduct = JsonValue.Parse(json).GetObject();
            if (objProduct.ContainsKey("PrRecid"))
            {
                string recid = objProduct["PrRecid"].ToString().Trim('\"');
                product.PR_RECID = Convert.ToInt32(recid);
                product.Status = STATUS.Nothing.ToString();
            }
            if (objProduct.ContainsKey("PrName"))
                product.PR_Name = objProduct["PrName"].ToString().Trim('\"');
            if (objProduct.ContainsKey("PrPrice"))
                product.Price = Convert.ToDecimal(objProduct["PrPrice"].ToString().Trim('\"'));
            if (objProduct.ContainsKey("Image"))
            {
                var imageJson = objProduct["Image"].GetObject();
                if (imageJson.ContainsKey("ImRecid"))
                    product.IM_RECID = Convert.ToInt32(imageJson["ImRecid"].ToString().Trim('\"'));
                if (imageJson.ContainsKey("ImName"))
                    product.IM_Name = imageJson["ImName"].ToString().Trim('\"');
                if (imageJson.ContainsKey("ImByte"))
                {
                    string a = imageJson["ImByte"].ToString().Trim('\"');
                    
                    if (images != null && images.Count > 0)
                    {
                        ImageViewModel imageViewModel = images.Where(x => x.IM_Name == product.IM_Name).FirstOrDefault();
                        if (imageViewModel != null)
                        {
                            product.IM_Byte = imageViewModel.BytesImage;
                            product.Image = imageViewModel.bitmapImage;
                        }
                    }
                    else
                    {
                        if (a.Contains("#"))
                            product.IM_Byte = ImageService.StringToByteForDB(a);
                    }
                    //product.IM_Byte = im_srv.GetBytes(imageJson["ImByte"].ToString().Trim('\"'));
                    // product.Image = im_srv.GetImage(product.IM_Byte).Result;
                }
                if (imageJson.ContainsKey("ImType"))
                    product.IM_Type = imageJson["ImType"].ToString().Trim('\"');
            }
            product.CategoryParent = categoryParent;
            product.TabParent = tabParent;
            return product;
        }
        public static string SerealizeObjWithComponent<T>(ObservableCollection<T> obj)
        {
            if (obj == null || obj.Count < 1) return string.Empty;
            string json = string.Empty;
            try
            {
                json = JsonConvert.SerializeObject(obj);
            }
            catch (Exception exc)
            {
                string exception = exc.Message;
                return json;
            }
            return json;
        }
        public static ObservableCollection<T> DeserealizeObjWithComponent<T>(string json)
        {
            ObservableCollection<T> obj = new ObservableCollection<T>();

            if (string.IsNullOrEmpty(json)) return obj;

            try
            {
                obj = JsonConvert.DeserializeObject<ObservableCollection<T>>(json);
            }
            catch (Exception exc)
            {
                string exception = exc.Message;
                return obj;
            }
            return obj;
        }

        public static ObservableCollection<OrderViewModel> DeserealizeOrders(string json)
        {
            ObservableCollection<OrderViewModel> orders = new ObservableCollection<OrderViewModel>();

            if (string.IsNullOrEmpty(json)) return orders;
            JsonObject objOrders = JsonValue.Parse(json).GetObject();
            foreach (var itemOrder in objOrders["Orders"].GetArray())
            {
                OrderViewModel order_vm = new OrderViewModel();
                JsonObject order_json = itemOrder.GetObject();
                if (order_json.ContainsKey("OrdRecid"))
                {
                    order_vm.Ord_RECID = ConvertStringToInteger(order_json["OrdRecid"].ToString().Trim('\"'));
                }
                if (order_json.ContainsKey("OrdOrderdate"))
                {
                    order_vm.Ord_OrderDate = ConvertStringToDate(order_json["OrdOrderdate"].ToString().Trim('\"'));
                }
                if (order_json.ContainsKey("OrdPricecost"))
                {
                    order_vm.Ord_PriceCost = ConvertStringToDecimal(order_json["OrdPricecost"].ToString().Trim('\"'));
                }
                if (order_json.ContainsKey("OrdGetmoney"))
                {
                    order_vm.Ord_GotMoney = ConvertStringToDecimal(order_json["OrdGetmoney"].ToString().Trim('\"'));
                }
                if (order_json.ContainsKey("Usersdata"))
                {
                    order_vm.UserRecid = ConvertStringToInteger(order_json["Usersdata"].ToString().Trim('\"'));
                }
                if (order_json.ContainsKey("Ordersproducts"))
                {
                    List<string> recids = order_json["Ordersproducts"].ToString().Split(',').ToList<string>().Select(x => x.Trim('\"')).ToList<string>();
                    ProductService pr_srv = new ProductService();
                    foreach (ProductViewModel prod in pr_srv.GetAllProducts())
                    {
                        foreach (string id in recids)
                        {
                            if (id.Split(':')[0].Equals(prod.PR_RECID.ToString()))
                            {
                                ProductViewModel product = new ProductViewModel();
                                product = ((ProductViewModel)prod) as ProductViewModel;
                                product.ORD_Count = ConvertStringToInteger(id.Split(':')[1]);
                                order_vm.Product_ORD.Add(product);
                            }
                        }
                    }
                }
                orders.Add(order_vm);
            }
            return orders;
        }
        public static UserViewModel DeserealizeUser(string json)
        {
            if (string.IsNullOrEmpty(json)) return null;
            UserViewModel user = new UserViewModel();

            JsonObject objUser = JsonValue.Parse(json).GetObject();
            if (objUser.ContainsKey("UsrRecid"))
            {
                user.USR_RECID = ConvertStringToInteger(objUser["UsrRecid"].ToString().Trim('\"'));
            }
            if (objUser.ContainsKey("UsrLogin"))
            {
                user.USR_Login = objUser["UsrLogin"].ToString().Trim('\"');
            }
            if (objUser.ContainsKey("UsrFname"))
            {
                user.USR_FName = objUser["UsrFname"].ToString().Trim('\"');
            }
            if (objUser.ContainsKey("UsrLname"))
            {
                user.USR_LName = objUser["UsrLname"].ToString().Trim('\"');
            }
            if (objUser.ContainsKey("UsrEmail"))
            {
                user.USR_Email = objUser["UsrEmail"].ToString().Trim('\"');
            }
            if (objUser.ContainsKey("UsrRole"))
            {
                user.USR_Role = objUser["UsrRole"].ToString().Trim('\"');
            }

            return user;
        }

        public static string SerealizeObject(object obj)
        {
            string json = string.Empty;
            if (obj == null) return json;
            try
            {
                json = JsonConvert.SerializeObject(obj);
            }
            catch (Exception exc)
            {
                string exception = exc.Message;
                return json;
            }
            return json;
        }
        public static DateTime ConvertStringToDate(this string date)
        {
            DateTime time = DateTime.MinValue;
            var timeFormat = "dd.MM.yyyy HH:mm:ss";
            try
            {
                time = DateTime.ParseExact(date, timeFormat, CultureInfo.CurrentCulture);
            }
            catch (Exception exc)
            {
                return DateTime.MinValue;
            }
            return time;
        }
        public static decimal ConvertStringToDecimal(this string numer)
        {
            if (string.IsNullOrEmpty(numer)) return -1;

            decimal result;
            if (decimal.TryParse(numer, out result))
            {
                return result;
            }
            return -1;
        }
        public static int ConvertStringToInteger(this string numer)
        {
            if (string.IsNullOrEmpty(numer)) return -1;

            Int32 result;
            if (Int32.TryParse(numer, out result))
            {
                return result;
            }
            return -1;
        }
    }
}
