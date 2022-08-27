using DroneManagmentAPI.Models;

namespace DroneManagmentAPI.ViewModels
{
    public class DroneMedViewModel
    {
        public DroneMedViewModel()
        {
            Drone = new Drone();
            Medications = new List<Medication>();
        }
        public Drone Drone { get; set; } 

        public List<Medication> Medications { get; set; }
    }
}
