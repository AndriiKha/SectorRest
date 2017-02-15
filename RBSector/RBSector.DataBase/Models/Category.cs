using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using RBSector.DataBase.Tools;
using Newtonsoft.Json;

namespace RBSector.DataBase.Models
{
    public class Category: BaseModel
    {
        public Category()
        {
            Products = new List<Products>();
        }
        public virtual int CtRecid { get; set; }
        public virtual string CtName { get; set; }
        public virtual Tabs Tabs { get; set; }
        public virtual IList<Products> Products { get; set; }
        [JsonIgnore]
        public virtual string Serialize
        {
            get
            {
                return SendDataType.ConvertToString(
                    "\"CtRecid\":" + "\"" + CtRecid+ "\"",
                    "\"CtName\":" + "\""+CtName+ "\"",
                    "\"Tabs\":" + "\"" + Tabs.TbRecid+ "\"",
                    "\"Products\":{" + Products.Select(x => x.PrRecid.ToString()).ToList<string>().JSonRecid() + "}"
                    );

            }
        }
        [JsonIgnore]
        public virtual string SerializeWithComponents
        {
            get
            {
                return SendDataType.ConvertToString(
               "\"CtRecid\":" + "\"" + this.CtRecid + "\"",
               "\"CtName\":" + "\"" + this.CtName + "\"",
               SendDataType.ComponetsList<Products>(Products)
               );
            }
        }
    }
    public class CategoryMap : ClassMapping<Category>
    {

        public CategoryMap()
        {
            Table("Category");
            Id(x => x.CtRecid, map => { map.Column("CT_RECID"); map.Generator(Generators.Identity); });
            Property(x => x.CtName, map => { map.Column("CT_Name"); map.NotNullable(true); });
            ManyToOne(x => x.Tabs, map => { map.Column("CT_IDTab"); map.Cascade(Cascade.None); });

            Bag(x => x.Products, colmap => { colmap.Key(x => x.Column("PR_IDCategory")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }
}
