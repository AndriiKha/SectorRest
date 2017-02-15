using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSectorUWPBusinessLogic.ViewModel
{
    public class TabViewModel : BaseModel
    {
        public TabViewModel()
        {
            Categories = new ObservableCollection<CategoryViewModel>();
        }
        [JsonProperty("TB_RECID")]
        public int TB_RECID { get; set; }
        [JsonProperty("TB_Name")]
        public string TB_Name { get; set; }
        [JsonProperty("Categories")]
        public ObservableCollection<CategoryViewModel> Categories { get; set; }
    }
}
