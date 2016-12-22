using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;

namespace RBSector.DataBase.Models
{
    public class Ingredients
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
    }
    public class IngredientsMap : ClassMapping<Ingredients>
    {

        public IngredientsMap()
        {
            Id(x => x.IgRecid, map => { map.Column("IG_RECID"); map.Generator(Generators.Identity); });
            Property(x => x.IgCount, map => { map.Column("IG_Count"); map.NotNullable(true); });
            Property(x => x.IgName, map => { map.Column("IG_Name"); map.NotNullable(true); });
            Property(x => x.IgDescription, map => map.Column("IG_Description"));
            Bag(x => x.Products, colmap => { colmap.Key(x => x.Column("PR_IDIngredients")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }
}
