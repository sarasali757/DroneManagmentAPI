namespace DroneManagmentAPI.ViewModels
{
    public class DroneMedCustomEntity
    {
        public DroneMedCustomEntity()
        {
            MedicationsId = new List<int>();
        }
        public int DroneId { get; set; }
        public List<int> MedicationsId { get; set; }
    }
}
