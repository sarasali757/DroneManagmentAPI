using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DroneManagmentAPI.Models
{
    public partial class BatteryLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [ForeignKey("Drone")]
        public int Drone_ID { get; set; }
        [Column(TypeName = "decimal(5,2)")]
        public decimal BatteryLevel { get; set; }
        public DateTime TimeLog { get; set; }
        public virtual Drone Drone { get; set; }
    }
}
