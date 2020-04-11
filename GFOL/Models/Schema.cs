using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using GFOL.Data;

namespace GFOL.Models
{
    public class Schema : IEntity
    {
        [Required]
        [Key]
        public int Id { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(MAX)")]
        public string SchemaJson { get; set; }
        [Required]
        [Display(Name = "Saved Date")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        [DataType(DataType.Date)]
        public DateTime SavedDate { get; set; }
    }
}
