using RBSector.DataBase.Models;
using RBSector.Entry.Entry;
using System;
using System.Collections.Generic;
using System.Json;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSector.Entry.Tools
{
    public static class JsonTools
    {
        public static string Componets<T>(this List<T> list, bool isMain = false)
        {
            if (list == null || list.Count <= 0) return string.Empty;
            bool isFirst = true;
            string result = isMain ? "{" : "";
            if (list.FirstOrDefault() is Tabs)
            {
                result += "\"Tabs\":[";
            }
            foreach (var item in list)
            {
                if (!isFirst)
                {
                    result += ",";
                }
                else
                {
                    isFirst = false;
                }
                if (item is Tabs)
                {
                    result += (item as Tabs).SerializeWithComponents;
                }
            }
            result += "]" + (isMain ? "}" : "");
            return result;
        }

        public static List<Tabs> Deserelize(string json)
        {
            List<Tabs> obj = new List<Tabs>();
            var jsonOBJ = (JsonArray)JsonValue.Parse(json);
            foreach (var item in jsonOBJ)
            {
                Tabs tab = new Tabs();
                if (item.ContainsKey("TB_RECID"))
                    tab.RECID = tab.TbRecid = Convert.ToInt32(item["TB_RECID"].ToString().Trim('\"'));
                if (item.ContainsKey("TB_Name"))
                    tab.TbName = item["TB_Name"].ToString().Trim('\"');
                if (item.ContainsKey("Status"))
                    tab.Status = item["Status"].ToString().Trim('\"');
                if (item.ContainsKey("Categories"))
                {
                    string jsonCategory = item["Categories"].ToString().Trim('\"');
                    tab.Category = DeserelizeCategory(jsonCategory, tab);
                }
                if (!string.IsNullOrEmpty(tab.TbName))
                    obj.Add(tab);
            }
            return obj;
        }
        public static List<Category> DeserelizeCategory(string json, Tabs parenttab)
        {
            List<Category> obj = new List<Category>();
            var jsonOBJ = (JsonArray)JsonValue.Parse(json);
            foreach (var item in jsonOBJ)
            {
                Category category = new Category();
                if (item.ContainsKey("CT_RECID"))
                    category.RECID = category.CtRecid = Convert.ToInt32(item["CT_RECID"].ToString().Trim('\"'));
                if (item.ContainsKey("CT_Name"))
                    category.CtName = item["CT_Name"].ToString().Trim('\"');
                if (item.ContainsKey("Status"))
                    category.Status = item["Status"].ToString().Trim('\"');
                if (item.ContainsKey("Products"))
                {
                    string jsonProducts = item["Products"].ToString().Trim('\"');
                    category.Tabs = parenttab;
                    category.Products = DeserelizeProducts(jsonProducts, parenttab, category);
                }
                if (!string.IsNullOrEmpty(category.CtName))
                    obj.Add(category);
            }
            return obj;
        }
        public static List<Products> DeserelizeProducts(string json, Tabs parentTab, Category parentCategory)
        {
            List<Products> obj = new List<Products>();
            var jsonOBJ = (JsonArray)JsonValue.Parse(json);
            foreach (var item in jsonOBJ)
            {
                Products products = new Products();
                if (item.ContainsKey("PR_RECID"))
                    products.RECID = products.PrRecid = Convert.ToInt32(item["PR_RECID"].ToString().Trim('\"'));
                if (item.ContainsKey("PR_Name"))
                    products.PrName = item["PR_Name"].ToString().Trim('\"');
                if (item.ContainsKey("Status"))
                    products.Status = item["Status"].ToString().Trim('\"');
                if (item.ContainsKey("Price"))
                    products.PrPrice = Convert.ToDecimal(item["Price"].ToString().Trim('\"'));
                Images image;
                if (item.ContainsKey("IM_RECID"))
                {
                    image = new Images();
                    image.RECID = image.ImRecid = Convert.ToInt32(item["IM_RECID"].ToString());
                    if (item.ContainsKey("IM_Name") && item["IM_Name"] != null)
                        image.ImName = item["IM_Name"].ToString().Trim('"');
                    if (item.ContainsKey("IM_Type") && item["IM_Type"] != null)
                        image.ImType = item["IM_Type"].ToString().Trim('"');
                    if (item.ContainsKey("IM_Byte") && item["IM_Byte"] != null)
                        image.ImByte = item["IM_Byte"].ToString().Trim('"');//ImageTools.GetBytes(item["IM_Byte"].ToString());
                    if (item.ContainsKey("Byte_String") && item["Byte_String"] != null)
                        image.ImByte = item["Byte_String"].ToString().Trim('"');

                    if (!string.IsNullOrEmpty(image.ImName))
                    {
                        image.Products.Add(products);
                        products.Images = image;
                    }
                }
                products.Tabs = parentTab;
                products.Category = parentCategory;
                if (!string.IsNullOrEmpty(products.PrName))
                    obj.Add(products);
            }
            return obj;
        }

        public static Orders DeserelizeOrder(string json)
        {
            if (string.IsNullOrEmpty(json)) return null;
            Orders obj = new Orders();
            var jsonOBJ = (JsonArray)JsonValue.Parse(json);
            if (jsonOBJ.ContainsKey("Ord_OrderDate") && jsonOBJ["Ord_OrderDate"] != null)
            {
                obj.OrdOrderdate = ConvertStringToDate(jsonOBJ["Ord_OrderDate"].ToString());
            }
            if (jsonOBJ.ContainsKey("Ord_PriceCost") && jsonOBJ["Ord_PriceCost"] != null)
            {
                obj.OrdPricecost = ConvertStringToDecimal(jsonOBJ["Ord_PriceCost"].ToString());
            }
            if (jsonOBJ.ContainsKey("PR_NaOrd_GotMoneyme") && jsonOBJ["PR_NaOrd_GotMoneyme"] != null)
            {
                obj.OrdGetmoney = ConvertStringToDecimal(jsonOBJ["PR_NaOrd_GotMoneyme"].ToString());
            }
            if (jsonOBJ.ContainsKey("User") && jsonOBJ["User"] != null)
            {
                int recid = ConvertStringToInteger(jsonOBJ["User"].ToString());
                if (recid >= 0)
                {
                    UserEntry user_entry = new UserEntry();
                    Usersdata user = user_entry.GetUser(recid);
                    if (user != null)
                    {
                        obj.Usersdata = user;
                    }
                }
            }
            if (jsonOBJ.ContainsKey("Product_ORD") && jsonOBJ["Product_ORD"] != null)
            {
                if (obj.Ordersproducts == null) obj.Ordersproducts = new List<Ordersproducts>();
                ProductEntry prod_entry = new ProductEntry();
                foreach (var item in jsonOBJ)
                {
                    if (item != null && item.ToString().Contains(":"))
                    {
                        int RecidProduct = ConvertStringToInteger(item.ToString().Split(':')[0]);
                        int CountProduct = ConvertStringToInteger(item.ToString().Split(':')[1]);
                        Products product = prod_entry.GetProduct(RecidProduct);
                        if (product != null)
                        {
                            obj.Ordersproducts.Add(new Ordersproducts()
                            {
                                Orders = obj,
                                OrdPrCount = CountProduct,
                                OrdPrRecid = RecidProduct,
                                Products = product
                            });
                        }
                    }
                }

            }
            return obj;
        }

        public static DateTime ConvertStringToDate(string date)
        {
            DateTime time;
            if (DateTime.TryParse(date, out time))
            {
                return time;
            }
            return DateTime.MinValue;
        }
        public static decimal ConvertStringToDecimal(string numer)
        {
            if (string.IsNullOrEmpty(numer)) return -1;

            decimal result;
            if (decimal.TryParse(numer, out result))
            {
                return result;
            }
            return -1;
        }
        public static int ConvertStringToInteger(string numer)
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
