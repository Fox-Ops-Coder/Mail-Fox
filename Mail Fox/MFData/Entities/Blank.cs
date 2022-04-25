using MFData.Types;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MFData.Entities
{
    [Table("Blanks")]
    public sealed class Blank
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = SQLTypes.INTEGER)]
        public int BlankId { get; set; }

        [Required, Column(TypeName = SQLTypes.TEXT)]
        public string BlankText { get; set; }

        public Blank() => BlankText = string.Empty;
    }
}