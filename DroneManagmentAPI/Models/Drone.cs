using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DroneManagmentAPI.Models
{
    public partial class Drone
    {
        public Drone()
        {
            SerialNumber = "";
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [Required]
        [MaxLength(100)]
        public string SerialNumber  { get; set; }
        [Required]
        public int Model { get; set; }
        [Required]
        [Column(TypeName = "decimal(5,2)")]
        [Range(typeof(decimal), "0", "500")]
        public decimal WeightLimit { get; set; }
        [Required]
        [Column(TypeName = "decimal(5,2)")]
        [Range(typeof(decimal), "0", "100")]
        public decimal BatteryCapacity  { get; set; }
        [Required]
        public int State  { get; set; }
    }
}
