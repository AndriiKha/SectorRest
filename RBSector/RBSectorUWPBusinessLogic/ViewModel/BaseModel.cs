using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSectorUWPBusinessLogic.ViewModel
{
    public class BaseModel
    {
        [JsonProperty("Status")]
        public string Status { get; set; }
        [JsonProperty("DeletedRecid")]
        public string DeletedRecid { get; set; }
    }
    public enum STATUS { Edited, Created, Deleted, Nothing }
    public enum DELETED_PART { TAB_DELETED, CATEGORY_DELETED, PRODUCT_DELETED }
}
