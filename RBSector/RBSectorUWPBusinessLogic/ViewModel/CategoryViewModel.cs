using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSectorUWPBusinessLogic.ViewModel
{
    public class CategoryViewModel:BaseModel
    {
        public CategoryViewModel()
        {
            TabParent = new TabViewModel();
            Products = new ObservableCollection<ProductViewModel>();
        }
        [JsonProperty("CT_RECID")]
        public int CT_RECID { get; set; }
        [JsonProperty("CT_Name")]
        public string CT_Name { get; set; }
        [JsonIgnore]
        public TabViewModel TabParent { get; set; }
        [JsonProperty("Products")]
        public ObservableCollection<ProductViewModel> Products { get; set; }
    }
}
