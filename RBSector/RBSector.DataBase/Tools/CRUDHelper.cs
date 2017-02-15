using RBSector.DataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSector.DataBase.Tools
{
    public class CRUDHelper
    {
        public static void CopyProduct(ref Products product, Products newProducts)
        {
            if (newProducts != null)
            {
                product.PrName = newProducts.PrName;
                product.PrPrice = newProducts.PrPrice;
                product.Images = newProducts.Images;
                product.Ingredients = newProducts.Ingredients;
                product.Ordersproducts = newProducts.Ordersproducts;
                product.Category = newProducts.Category;
                product.Tabs = newProducts.Tabs;
            }
        }

        public static void CopyImages(ref Images image, Images newImage)
        {
            if (newImage != null)
            {
                image.ImName = newImage.ImName;
                image.ImType = newImage.ImType;
                image.ImByte = newImage.ImByte;
                image.Products = newImage.Products;
            }
        }
    }
}
