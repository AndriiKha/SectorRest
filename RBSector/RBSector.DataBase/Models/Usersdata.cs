using System.Collections.Generic;
using System.Linq;
using NHibernate.Mapping.ByCode.Conformist;
using NHibernate.Mapping.ByCode;
using RBSector.DataBase.Interfaces;
using RBSector.DataBase.Tools;
using Newtonsoft.Json;

namespace RBSector.DataBase.Models
{
    public class Usersdata: BaseModel
    {
        public Usersdata()
        {
            Orders = new List<Orders>();
        }
        public virtual int UsrRecid { get; set; }
        public virtual string UsrLogin { get; set; }
        public virtual string UsrPassword { get; set; }
        public virtual string UsrFname { get; set; }
        public virtual string UsrLname { get; set; }
        public virtual string UsrEmail { get; set; }
        public virtual string UsrRole { get; set; }
        [JsonIgnore]
        public virtual IList<Orders> Orders { get; set; }
        [JsonIgnore]
        public virtual string Serialize
        {
            get
            {
               return SendDataType.ConvertToString(
                   "\"UsrRecid\":" + "\""+ UsrRecid+ "\"",
                   "\"UsrLogin\":" + "\""+UsrLogin+ "\"",
                   "\"UsrPassword\":" + "\""+UsrPassword+ "\"",
                   "\"UsrFname\":" + "\""+UsrFname+"\"",
                   "\"UsrLname\":" + "\""+UsrLname+ "\"",
                   "\"UsrEmail\":" + "\""+ UsrEmail+ "\"",
                   "\"UsrRole\":" + "\""+UsrRole+ "\"",
                   "\"Orders\":{" + string.Join(",", Orders.Select(x=>x.OrdRecid.ToString()).ToList<string>().JSonRecid())+"}");
            }
        }
        [JsonIgnore]
        public virtual string SerializeWithComponents
        {
            get
            {
                return SendDataType.ConvertToString(
                   "\"UsrRecid\":" + "\"" + UsrRecid + "\"",
                   "\"UsrLogin\":" + "\"" + UsrLogin + "\"",
                   "\"UsrPassword\":" + "\"" + UsrPassword + "\"",
                   "\"UsrFname\":" + "\"" + UsrFname + "\"",
                   "\"UsrLname\":" + "\"" + UsrLname + "\"",
                   "\"UsrEmail\":" + "\"" + UsrEmail + "\"",
                   "\"UsrRole\":" + "\"" + UsrRole + "\"",
                  SendDataType.ComponetsList<Orders>(Orders)
                  );
            }
        }
    }
    public class UsersdataMap : ClassMapping<Usersdata>
    {

        public UsersdataMap()
        {
            Table("UsersData");
            Id(x => x.UsrRecid, map => { map.Column("USR_RECID"); map.Generator(Generators.Identity); });
            Property(x => x.UsrLogin, map => { map.Column("USR_Login"); map.NotNullable(true); });
            Property(x => x.UsrPassword, map => { map.Column("USR_Password"); map.NotNullable(true); });
            Property(x => x.UsrFname, map => { map.Column("USR_FName"); map.NotNullable(true); });
            Property(x => x.UsrLname, map => { map.Column("USR_LName"); map.NotNullable(true); });
            Property(x => x.UsrEmail, map => map.Column("USR_Email"));
            Property(x => x.UsrRole, map => { map.Column("USR_Role"); map.NotNullable(true); });
            Bag(x => x.Orders, colmap => {colmap.Key(x => x.Column("ORD_UserID")); colmap.Inverse(true); }, map => { map.OneToMany(); });
        }
    }
}
