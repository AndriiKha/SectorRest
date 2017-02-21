using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSectorUWPBusinessLogic.ViewModel
{
    public class OrderViewModel
    {
        public OrderViewModel() { Product_ORD = new ObservableCollection<ProductViewModel>(); }
        [JsonProperty("Ord_RECID")]
        public int Ord_RECID { get; set; }
        [JsonProperty("Ord_OrderDate")]
        public DateTime Ord_OrderDate { get; set; }
        [JsonProperty("Ord_PriceCost")]
        public decimal Ord_PriceCost { get; set; }
        [JsonProperty("PR_NaOrd_GotMoneyme")]
        public decimal Ord_GotMoney { get; set; }
        [JsonProperty("Product_ORD")]
        public List<string> ProductsRecid { get { return ((Product_ORD != null) ? Product_ORD.Select(x => x.PR_RECID.ToString()+":"+x.ORD_Count.ToString()).ToList<string>() : null); } }
        [JsonProperty("User")]
        public int UserRecid { get; set; }

        [JsonIgnore]
        public ObservableCollection<ProductViewModel> Product_ORD { get; set; }
    }
}
