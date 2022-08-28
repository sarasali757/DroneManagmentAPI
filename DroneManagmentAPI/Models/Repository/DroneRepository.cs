using DroneManagmentAPI.ViewModels;

namespace DroneManagmentAPI.Models.Repository
{
    public class DroneRepository
    {
         readonly DroneContext _droneContext;

        public DroneRepository(DroneContext droneContext ) {
            _droneContext = droneContext; 
        }

        public List<Drone> GetAllDrones() {
            return (from drone in _droneContext.Drones
                          select drone
                          ).ToList();
        }

        public List<Drone> GetAvailableDrones()
        {
            var state = (int) State.IDLE;
            return (from drone in _droneContext.Drones
                    where drone.State == state && drone.BatteryCapacity >=25 
                    select drone).ToList();
        }

        public bool SaveDrone(Drone droneEntity)
        {
            try
            {
                var drone = (from drn in _droneContext.Drones
                             where drn.ID == droneEntity.ID
                             select drn).FirstOrDefault();

                if(drone == null) // add new drone
                {
                    _droneContext.Drones.Add(droneEntity);
                }
                else // edit drone
                {
                    drone.BatteryCapacity = droneEntity.BatteryCapacity;
                    drone.State = droneEntity.State;
                    drone.Model = droneEntity.Model;
                    drone.SerialNumber = droneEntity.SerialNumber;
                    drone.WeightLimit = droneEntity.WeightLimit; 
                }
                _droneContext.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public Drone GetDrone(int id)
        {
            try
            {
                var drone = (from drn in _droneContext.Drones
                             where drn.ID == id
                             select drn).FirstOrDefault();
                return drone;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool DeleteDrone(int id)
        {
            /*add validation id drone attached to med */

            try
            { 
                var drone = (from drn in _droneContext.Drones
                             where drn.ID == id
                             select drn).FirstOrDefault();
                _droneContext.Drones.Remove(drone);
                _droneContext.SaveChanges();
                return true;
            }
            catch (Exception ex) {
                return false;
            }
        }
        public bool CheckDroneBattery()
        {
            try {
                var batteryLogs = (from drone in _droneContext.Drones
                                   select new BatteryLog()
                                   {
                                       Drone_ID = drone.ID ,
                                       BatteryLevel = drone.BatteryCapacity ,
                                       TimeLog = DateTime.Now 
                                   }).ToList();

                _droneContext.BatteryLog.AddRange(batteryLogs);
                return true;
            }
            catch (Exception) {
                return false; 
            }
        }
    }
}
