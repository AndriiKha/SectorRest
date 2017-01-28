using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSector.Entry.ViewModel
{
    public class CategoryViewModel
    {
        public string Name { get; set; }
        public List<ProductViewModel> Products { get; set; }
    }
}
