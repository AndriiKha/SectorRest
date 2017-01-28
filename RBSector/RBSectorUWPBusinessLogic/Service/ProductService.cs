using RBSectorUWPBusinessLogic.MainServiceClient;
using RBSectorUWPBusinessLogic.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSectorUWPBusinessLogic.Service
{
    public class ProductService
    {
        public ProductViewModel product { get; private set; }
        public ProductService()
        {
            product = new ProductViewModel();
        }
        public bool CreateProduct(string name, string price)
        {
            if (!BindingModel.CheckNameUnique(typeof(ProductViewModel), name)) return false;
            product.PR_Name = name;
            product.Price = Convert.ToDecimal(price);
            product.TabParent = BindingModel.GetParentTab();
            product.CategoryParent = BindingModel.GetParentCategory();
            product.CategoryParent.Products.Add(product);
            product.isModify = true;
            SetProductsSingleToBindingModel(product);
            return true;
        }
        public void DeleteProduct(int id)
        {
            if (FindProduct(id))
                BindingModel.Products.Remove(product);
        }
        public bool FindProduct(int id)
        {
            product = BindingModel.Products.Where(x => x.PR_RECID == id).FirstOrDefault();
            if (product != null) return true;
            return false;
        }
        public void EditProduct(ProductViewModel editProduct)
        {
            var item = BindingModel.Products.FirstOrDefault(x => x.PR_RECID == editProduct.PR_RECID);
            if (item != null)
            {
                item = editProduct;
            }
            product = editProduct;
        }
        public void SetProduct(ProductViewModel item)
        {
            this.product = item;
        }
        public int GenerateNextProductID { get { return (GetAllProducts().Select(x => x.PR_RECID).Max() + 1); } }
        public void SetProductsToBindingModel(ObservableCollection<ProductViewModel> products)
        {
            if (BindingModel.Products.Count > 0)
                BindingModel.Products.Clear();
            foreach (var item in products)
            {
                if (BindingModel.Products.Where(x => x.PR_RECID == item.PR_RECID).FirstOrDefault() == null)
                    BindingModel.Products.Add(item);
            }
        }
        public void SetProductsSingleToBindingModel(ProductViewModel product)
        {
            if (BindingModel.Products.Where(x => x.PR_RECID == product.PR_RECID).FirstOrDefault() == null)
                BindingModel.Products.Add(product);
        }
        public ObservableCollection<ProductViewModel> GetAllProducts()
        {
            ObservableCollection<ProductViewModel> products = new ObservableCollection<ProductViewModel>();
            foreach (var cat in BindingModel.Category)
            {
                products.AddRange(cat.Products);
            }
            return products;
        }
    }
}
