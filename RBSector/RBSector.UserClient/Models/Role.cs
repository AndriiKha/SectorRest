using SQLite.Net.Attributes;
using System.Collections.Generic;

namespace RBSector.UserClient.Models
{
    internal class Role : Model
    {
        [Unique, NotNull]
        public string Name { get; set; }

        [NotNull]
        public uint Value { get; set; }

        public string Description { get; set; }

        public string FullName { get; set; }

        public List<User> Users { get; set; }
    }
}
