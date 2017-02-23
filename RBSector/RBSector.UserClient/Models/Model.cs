using SQLite.Net.Attributes;

namespace RBSector.UserClient.Models
{
    internal abstract class Model
    {
        [Unique, PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
