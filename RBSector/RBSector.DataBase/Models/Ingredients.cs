using System.Collections.Generic;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using RBSector.DataBase.Tools;
using Newtonsoft.Json;

namespace RBSector.DataBase.Models
{
    public class Ingredients: BaseModel
    {
        public Ingredients()
        {
            Products = new List<Products>();
        }
        public virtual int IgRecid { get; set; }
        public virtual string IgCount { get; set; }
        public virtual string IgName { get; set; }
        public virtual string IgDescription { get; set; }
        public virtual IList<Products> Products { get; set; }
        [JsonIgnore]
        public virtual string Serialize
        {
            get
            {
                return SendDataType.ConvertToString(
                    "\"IgRecid\":" + "\""+IgRecid+ "\"",
                    "\"IgCount\":" + "\""+IgCount+ "\"",
                    "\"IgName\":" + "\""+IgName+ "\"",
                    "\"IgDescription\":" + "\""+IgDescription+ "\""
                    );

            }
        }
    }
    public class IngredientsMap : ClassMapping<Ingredients>
    {

        public IngredientsMap()
        {
            Table("Ingredients");
            Id(x => x.IgRecid, map => { map.Column("IG_RECID"); map.Generator(Generators.Identity); });
            Property(x => x.IgCount, map => { map.Column("IG_Count"); map.NotNullable(true); });
            Property(x => x.IgName, map => { map.Column("IG_Name"); map.NotNullable(true); });
            Property(x => x.IgDescription, map => map.Column("IG_Description"));
            Bag(x => x.Products, colmap => { colmap.Key(x => x.Column("PR_IDIngredients")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }
}
