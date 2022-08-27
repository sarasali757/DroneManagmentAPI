using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DroneManagmentAPI.Models
{
    public partial class DroneMedications
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        [ForeignKey("Drone")]
        public int Drone_ID { get; set; }
        [ForeignKey("Medication")]
        public int Medication_ID { get; set; }
        public string State { get; set; }
        public virtual Drone Drone { get; set; } 
        public virtual Medication Medication { get; set; }
    }
}
