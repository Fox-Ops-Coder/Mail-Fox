using MFData.Types;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MFData.Entities
{
    [Table("UserEmails")]
    public sealed class UserEmail
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = SQLTypes.INTEGER)]
        public int EmailId { get; set; }

        [Required, Column(TypeName = SQLTypes.TEXT)]
        public string Email { get; set; }

        [Required, Column(TypeName = SQLTypes.TEXT)]
        public string Service { get; set; }

        [Required, Column(TypeName = SQLTypes.BLOB)]
        public byte[] Password { get; set; }

        public IEnumerable<Contact> Contacts { get; set; }

        public UserEmail()
        {
            Email = string.Empty;
            Service = string.Empty;
            Password = Array.Empty<byte>();

            Contacts = new List<Contact>();
        }
    }
}