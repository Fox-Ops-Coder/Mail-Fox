using MFData.Types;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MFData.Entities
{
    [Table("Contacts")]
    public sealed class Contact
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = SQLTypes.INTEGER)]
        public int ContactId { get; set; }

        [ForeignKey("UserEmail")]
        [Column(TypeName = SQLTypes.INTEGER)]
        public int? EmailId { get; set; }

        [Required, Column(TypeName = SQLTypes.TEXT)]
        public string ContactName { get; set; }

        [Required, Column(TypeName = SQLTypes.TEXT)]
        public string ContactEmail { get; set; }

        public UserEmail? UserEmail { get; set; }

        public Contact()
        {
            ContactName = string.Empty;
            ContactEmail = string.Empty;
        }
    }
}