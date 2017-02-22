using Newtonsoft.Json;
using NHibernate;
using RBSector.DataBase.Models;
using RBSector.Entry.Logic;
using RBSector.Entry.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSector.Entry.Entry
{
    public class MainSubmitEntry
    {
        private UniversalCRUD<Tabs> tab_crud;
        private UniversalCRUD<Category> category_crud;
        private UniversalCRUD<Products> product_crud;
        private UniversalCRUD<Orders> orders_crud;
        private UniversalCRUD<Ordersproducts> orderproduct_crud;
        private UniversalCRUD<Images> image_crud;
        private UniversalCRUD<Ingredients> ingredient_crud;

        public MainSubmitEntry()
        {
            tab_crud = new UniversalCRUD<Tabs>();
            category_crud = new UniversalCRUD<Category>();
            product_crud = new UniversalCRUD<Products>();
            orders_crud = new UniversalCRUD<Orders>();
            orderproduct_crud = new UniversalCRUD<Ordersproducts>();
            image_crud = new UniversalCRUD<Images>();
            ingredient_crud = new UniversalCRUD<Ingredients>();
        }
        public bool DeletedRecid(string deleted)
        {
            List<string> recids = new List<string>();
            if (!string.IsNullOrEmpty(deleted))
            {
                recids = deleted.Split(',').ToList<string>();
            }
            else return true;

            foreach (var item in recids)
            {
                DELETED_PART part;
                bool canDelete = false;
                int id = -1;
                try
                {
                    if (item.Contains(":") && Int32.TryParse(item.Split(':')[1], out id))
                        canDelete = true;
                    if (id != -1 && canDelete)
                    {
                        if (item.Contains(DELETED_PART.PRODUCT_DELETED.ToString()))
                        {
                            Products product = product_crud.GetObj_ID(id);
                            if (product != null && product.Images != null)
                                image_crud.Delete<Products>(product.Images.ImRecid);
                            product_crud.Delete<Products>(id);

                        }
                        if (item.Contains(DELETED_PART.CATEGORY_DELETED.ToString()))
                            product_crud.Delete<Category>(id);
                        if (item.Contains(DELETED_PART.TAB_DELETED.ToString()))
                            product_crud.Delete<Tabs>(id);
                    }
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
            return true;
        }
        public bool SaveResult(string json, string deleted)
        {
            bool result = true;
            try
            {
                if (string.IsNullOrEmpty(json))
                {
                    return DeletedRecid(deleted);
                }
                List<Tabs> obj = JsonTools.Deserelize(json);
                foreach (Tabs tab in obj)
                {
                    List<Category> category = (List<Category>)tab.Category;
                    if (tab.Status != STATUS.Nothing.ToString())
                        tab_crud.SaveOrUpdate(tab);
                    if (category != null && category.Count > 0)
                    {
                        foreach (Category cat in category)
                        {
                            List<Products> products = (List<Products>)cat.Products;
                            cat.Tabs = tab;
                            if (cat.Status != STATUS.Nothing.ToString())
                                category_crud.SaveOrUpdate(cat);
                            if (products != null && products.Count > 0)
                            {
                                foreach (Products product in products)
                                {
                                    if (product.Status != STATUS.Nothing.ToString())
                                    {
                                        product.Category = cat;
                                        product.Tabs = tab;
                                        if (product.Images != null)
                                            image_crud.SaveOrUpdate(product.Images);
                                        product_crud.SaveOrUpdate(product);
                                    }
                                }
                            }
                        }
                    }
                }
                result = DeletedRecid(deleted);
            }
            catch (Exception ex)
            {
                string exception = ex.Message;
                return false;
            }
            return result;
        }

        public bool SaveOrder(string json)
        {
            try
            {
                bool result = false;
                Orders order = JsonTools.DeserelizeOrder(json);
                if (order == null) return false;
                result= orders_crud.SaveOrUpdate(order);
                foreach(var item in order.Ordersproducts)
                {
                    result = result && orderproduct_crud.SaveOrUpdate(item);
                }
                return result;
            }
            catch(Exception exc)
            {
                return false;
            }
        }
    }
    public enum DELETED_PART { TAB_DELETED, CATEGORY_DELETED, PRODUCT_DELETED }
}
