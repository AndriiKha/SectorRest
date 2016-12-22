using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;

namespace RBSector.DataBase.Models
{
    public class Images
    {
        public Images()
        {
            Products = new List<Products>();
        }
        public virtual int ImRecid { get; set; }
        public virtual string ImName { get; set; }
        public virtual string ImType { get; set; }
        public virtual string ImByte { get; set; }
        public virtual IList<Products> Products { get; set; }
    }
    public class ImagesMap : ClassMapping<Images>
    {

        public ImagesMap()
        {
            Id(x => x.ImRecid, map => { map.Column("IM_RECID"); map.Generator(Generators.Identity); });
            Property(x => x.ImName, map => { map.Column("IM_Name"); map.NotNullable(true); });
            Property(x => x.ImType, map => { map.Column("IM_Type"); map.NotNullable(true); });
            Property(x => x.ImByte, map => { map.Column("IM_Byte"); map.NotNullable(true); });
            Bag(x => x.Products, colmap => { colmap.Key(x => x.Column("PR_IDImage")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }

}
