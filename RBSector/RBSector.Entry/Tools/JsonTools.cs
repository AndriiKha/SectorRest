using RBSector.DataBase.Models;
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
                if (item.ContainsKey("Price"))
                    products.PrPrice = Convert.ToDecimal(item["Price"].ToString().Trim('\"'));
                products.Tabs = parentTab;
                products.Category = parentCategory;

                /* if (item.ContainsKey("Products"))
                 {
                     string jsonProducts = item["Products"].ToString().Trim('\"');
                     products.Ingredients= DeserelizeProducts(jsonProducts);
                 }*/
                if (!string.IsNullOrEmpty(products.PrName))
                    obj.Add(products);
            }
            return obj;
        }
    }
}
