using SQLite.Net.Attributes;
using System;
using System.ComponentModel.DataAnnotations;

namespace RBSector.UserClient.Models
{
    internal class User : Model
    {
        [Unique, Indexed]
        public string Login { get; set; }

        [NotNull, SQLite.Net.Attributes.MaxLength(32)]
        public string Password { get; set; }

        [NotNull, EmailAddress, SQLite.Net.Attributes.MaxLength(128)]
        public string Email { get; set; }

        [NotNull]
        public Role UserRole { get; set; }

        public byte[] Photo { get; set; }

        public uint Pin { get; set; }

        [SQLite.Net.Attributes.MaxLength(32)]
        public string FirstName { get; set; }

        [SQLite.Net.Attributes.MaxLength(32)]
        public string LastName { get; set; }

        [SQLite.Net.Attributes.MaxLength(32)]
        public string MiddleName { get; set; }

        public DateTime DayOfBirth { get; set; }

    }
}
