using Newtonsoft.Json;
using RBSectorUWPBusinessLogic.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Media.Imaging;

namespace RBSectorUWPBusinessLogic.ViewModel
{
    public class ProductViewModel : BaseModel
    {
        public ProductViewModel()
        {
            TabParent = new TabViewModel();
            CategoryParent = new CategoryViewModel();
        }
        [JsonProperty("PR_RECID")]
        public int PR_RECID { get; set; }
        [JsonProperty("PR_Name")]
        public string PR_Name { get; set; }
        [JsonProperty("IM_RECID")]
        public int IM_RECID { get; set; }
        [JsonProperty("IM_Name")]
        public string IM_Name { get; set; }
        [JsonProperty("IM_Type")]
        public string IM_Type { get; set; }
        [JsonProperty("IM_Byte")]
        [JsonIgnore]
        public byte[] IM_Byte { get; set; }
        [JsonProperty("Byte_String")]
        public string ByteString { get; set; }
        [JsonIgnore]
        public BitmapImage Image { get; set; }
        [JsonProperty("Price")]
        public decimal Price { get; set; }
        [JsonProperty("IG_RECID")]
        public int IG_RECID { get; set; }
        [JsonProperty("IG_Count")]
        public int? Products { get; set; }
        [JsonProperty("IG_Name")]
        public string IG_Name { get; set; }
        [JsonProperty("IG_Description")]
        public string IG_Description { get; set; }
        [JsonProperty("TabParent")]
        [JsonIgnore]
        public TabViewModel TabParent { get; set; }
        [JsonProperty("CategoryParent")]
        [JsonIgnore]
        public CategoryViewModel CategoryParent { get; set; }
    }
}
