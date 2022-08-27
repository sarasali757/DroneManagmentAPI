using DroneManagmentAPI.ViewModels;

namespace DroneManagmentAPI.Models.Repository
{
    public class DispatchRepository
    {
        readonly DroneContext _droneContext;
        public DispatchRepository(DroneContext droneContext)
        {
            _droneContext = droneContext;
        }

        public bool LoadDrone(DroneMedCustomEntity droneMedEntity) {
            try
            {
                var medWeight = (from med in _droneContext.Medications
                                 where droneMedEntity.MedicationsId.Contains(med.ID)
                                 select med.Weight ).Sum();
                if (CheckDroneForLoading(droneMedEntity.DroneId, medWeight))
                {
                    var loadedState = (int)State.LOADED;
                    foreach(var medId in droneMedEntity.MedicationsId)
                    {
                        _droneContext.DroneMedications.Add(new DroneMedications()
                        {
                            Drone_ID = droneMedEntity.DroneId,
                            Medication_ID = medId,
                            State = loadedState 
                        });
                    }
                    ChangeDroneState(droneMedEntity.DroneId, loadedState);
                    _droneContext.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception)
            {
                return false;
            }
        }
        public DroneMedViewModel LoadedDroneDetails(int droneId)
        {
            // get drone Details for either loading or delivering state
            try { 
                var droneMedications = (from drone in _droneContext.Drones
                                        where droneId == drone.ID && (drone.State == (int)State.LOADED || drone.State == (int)State.DELIVERING) 
                                        select new DroneMedViewModel()
                                        {
                                            Drone = drone,
                                            Medications = (from droneMed in _droneContext.DroneMedications
                                                    join med in _droneContext.Medications
                                                    on droneMed.Medication_ID equals med.ID
                                                    where droneMed.Drone_ID == drone.ID
                                                    && (droneMed.State == (int)State.LOADED || droneMed.State == (int)State.DELIVERING)

                                                           select med ).ToList() 
                                        }).FirstOrDefault();
                return droneMedications;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public bool ChangeDroneState(int droneId, int state)
        {
            try
            {
                var drone = (from drn in _droneContext.Drones
                             where drn.ID == droneId
                             select drn).FirstOrDefault();

                if (drone == null)
                    return false;

                if( state == (int)State.DELIVERING || state == (int)State.DELIVERED)
                {
                    // change latest patch with  state == drone.state  
                    var droneMed = (from drnMed in _droneContext.DroneMedications
                                    where drnMed.Drone_ID == droneId && drnMed.State == drone.State
                                    select drnMed
                               ).ToList();
                    foreach(var drnMed in droneMed)
                    {
                        drnMed.State = state;
                    }
                }
                drone.State = state;
                _droneContext.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private bool CheckDroneForLoading(int droneId ,decimal medWeight)
        {
            var idelSstate = (int)State.IDLE;
            var droneAvailable= (from drone in _droneContext.Drones
                             where drone.ID == droneId && drone.WeightLimit >= medWeight
                             && drone.BatteryCapacity >= 25 && drone.State == idelSstate
                                 select drone).FirstOrDefault();

            if(droneAvailable != null)
            {
                // change drone state to loading 
                var loadingState = (int)State.LOADING;
                droneAvailable.State = loadingState;
                _droneContext.SaveChanges();
                return true;
            }
            else
            {
                return false;
            } 
        }

    }
}
