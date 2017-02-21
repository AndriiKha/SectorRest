using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSectorUWPBusinessLogic.ViewModel
{
    public class IngredientViewModel : BaseModel
    {
        [JsonProperty("IF_RECID")]
        public int IF_RECID { get; set; }
        [JsonProperty("IG_Name")]
        public string IG_Name { get; set; }
        [JsonProperty("IG_Description")]
        public string IG_Description { get; set; }
    }
}
