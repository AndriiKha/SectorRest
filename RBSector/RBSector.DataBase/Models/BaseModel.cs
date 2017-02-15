using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RBSector.DataBase.Models
{
    public class BaseModel
    {
        [JsonIgnore]
        public virtual int RECID { get; set; }
        [JsonIgnore]
        public virtual string Status { get; set; }
    }
    public enum STATUS { Edited, Created, Deleted, Nothing }
}
