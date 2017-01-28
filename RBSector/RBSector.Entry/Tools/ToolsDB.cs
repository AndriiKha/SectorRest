using RBSector.DataBase.Models;
using RBSector.Entry.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSector.Entry.Tools
{
    public static class ToolsDB
    {
        public static string AddBlockIn(this List<int> ids)
        {
            string result = string.Empty;
            if (ids != null)
            {
                foreach (int id in ids)
                {
                    if (!string.IsNullOrEmpty(result))
                    {
                        result += " , ";
                    }
                    result += id.ToString();
                }
            }
            return !string.IsNullOrEmpty(result) ? "(" + result + ")" : string.Empty;
        }
        public static List<Tabs> CreateTab(List<Tabs> tabs, List<Category> category, List<Products> products, List<Ingredients> ingredients, List<Images> images)
        {
            List<Tabs> result = new List<Tabs>();
            foreach (Tabs tab in tabs)
            {
                List<Category> _category = new List<Category>();
                foreach (Category cat in category)
                {
                    if (cat.Tabs.TbRecid == tab.TbRecid)
                    {
                        List<Products> _products = new List<Products>();
                        foreach (Products prod in products)
                        {
                            if (prod.Category.CtRecid == cat.CtRecid)
                            {
                                Ingredients ingd = ingredients != null ? ingredients.Where(x => x.IgRecid == prod.Ingredients.IgRecid).FirstOrDefault() : null;
                                Images im = images != null ? images.Where(x => x.ImRecid == prod.Images.ImRecid).FirstOrDefault() : null;
                                _products.Add(prod.CreateProduct(ingd, im));
                            }
                        }
                        _category.Add(cat.CreateCategory(_products));
                    }
                }
                result.Add(tab.CreateTab(_category));
            }
            return result;
        }

        public static Products CreateProduct(this Products products, Ingredients ingredients, Images images)
        {
            Products _products = new Products();
            _products = products;
            _products.Ingredients = ingredients;
            _products.Images = images;
            return _products;
        }
        public static Category CreateCategory(this Category category, List<Products> products)
        {
            Category _category = new Category();
            _category = category;
            _category.Products = products;
            return _category;
        }
        public static Tabs CreateTab(this Tabs tab, List<Category> category)
        {
            Tabs _tab = new Tabs();
            _tab = tab;
            _tab.Category = category;
            return _tab;
        }
    }
}
