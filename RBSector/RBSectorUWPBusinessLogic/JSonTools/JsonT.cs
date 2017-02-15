using Newtonsoft.Json;
using RBSectorUWPBusinessLogic.Service;
using RBSectorUWPBusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Data.Json;

namespace RBSectorUWPBusinessLogic.JSonTools
{
    public static class JsonT
    {
        public static ObservableCollection<TabViewModel> TabsDeserialize(this string json, Type type)
        {
            if (type == typeof(TabViewModel))
            {
                ObservableCollection<TabViewModel> tabViewModel = new ObservableCollection<TabViewModel>();
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
                            tbvm.Categories.Add(CategoryDeserialize(categoryJson.ToString(), tbvm));
                        }
                    }
                    tabViewModel.Add(tbvm);

                }
                return tabViewModel;
                //t["Tabs"].GetArray()[0].GetObject()["TbRecid"]
            }
            return null;
        }
        public static CategoryViewModel CategoryDeserialize(this string json, TabViewModel tabParent = null)
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
                    category.Products.Add(ProductDeserialize(productJson.ToString(), category, tabParent));
                }
            }
            category.TabParent = tabParent;
            return category;
        }
        public static ProductViewModel ProductDeserialize(this string json, CategoryViewModel categoryParent = null, TabViewModel tabParent = null)
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
                    if (a.Contains("#"))
                        product.IM_Byte = ImageService.StringToByteForDB(a);
                    //product.IM_Byte = im_srv.GetBytes(imageJson["ImByte"].ToString().Trim('\"'));
                    // product.Image = await im_srv.GetImage(product.IM_Byte);
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
    }
}
