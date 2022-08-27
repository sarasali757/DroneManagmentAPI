using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DroneManagmentAPI.Models
{
    public partial class Medication
    {
        public Medication() {
            Name = "";
            Code = "";
            Image = "";
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [RegularExpression("[A-Za-z_-]*")]
        public string Name { get; set; }
        [Required]
        [Column(TypeName = "decimal(5,2)")]
        public decimal  Weight { get; set; }
        [Required]

        [RegularExpression("[A-Z0-9_]*")]
        public string Code { get; set; }
        [Required]
        public string Image { get; set; }
    }
}
