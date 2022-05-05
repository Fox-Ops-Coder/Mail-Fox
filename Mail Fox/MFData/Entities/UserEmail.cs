using MFData.Types;
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
        public string Password { get; set; }

        public IEnumerable<Contact> Contacts { get; set; }

        public UserEmail()
        {
            Email = string.Empty;
            Password = string.Empty;

            Contacts = new List<Contact>();
        }
    }
}