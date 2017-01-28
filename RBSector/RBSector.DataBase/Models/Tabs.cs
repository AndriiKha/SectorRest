using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using RBSector.DataBase.Interfaces;
using RBSector.DataBase.Tools;

namespace RBSector.DataBase.Models
{
    public class Tabs: BaseModel
    {
        public Tabs()
        {
            Category = new List<Category>();
            Products = new List<Products>();
        }
        public virtual int TbRecid { get; set; }
        public virtual string TbName { get; set; }
        public virtual IList<Category> Category { get; set; }
        public virtual IList<Products> Products { get; set; }

        public virtual string Serialize
        {
           get{ return SendDataType.ConvertToString(
               "\"TbRecid\":" + "\""+this.TbRecid+ "\"",
               "\"TbName\":" + "\""+ this.TbName+ "\"",
               "\"Category\":{" + Category.Select(x=>x.CtRecid.ToString()).ToList<string>().JSonRecid()+ "}",
               "\"Products\":{" + Products.Select(x => x.PrRecid.ToString()).ToList<string>().JSonRecid() + "}"
               ); }
        }
        public virtual string SerializeWithComponents
        {
            get
            {
                return SendDataType.ConvertToString(
               "\"TbRecid\":" + "\"" + this.TbRecid + "\"",
               "\"TbName\":" + "\"" + this.TbName + "\"",
               SendDataType.Componets<Category>(Category)
               );
            }
        }
    }
    public class TabsMap : ClassMapping<Tabs>
    {

        public TabsMap()
        {
            Table("Tabs");
            Id(x => x.TbRecid, map => { map.Column("TB_RECID"); map.Generator(Generators.Identity); });
            Property(x => x.TbName, map => { map.Column("TB_Name"); map.NotNullable(true); });
            Bag(x => x.Category, colmap => { colmap.Key(x => x.Column("CT_IDTab")); colmap.Inverse(true); }, map => { map.OneToMany(); });
            Bag(x => x.Products, colmap => { colmap.Key(x => x.Column("PR_IDTab")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }

    }
}
