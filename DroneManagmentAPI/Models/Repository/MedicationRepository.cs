namespace DroneManagmentAPI.Models.Repository
{
    public class MedicationRepository
    {
        readonly DroneContext _droneContext;
        public MedicationRepository(DroneContext droneContext)
        {
            _droneContext = droneContext;
        }

        public List<Medication> GetAllMedications()
        {
            return (from med in _droneContext.Medications
                    select med ).ToList();
        }

        public bool SaveMedication(Medication medEntity)
        {
            /*need logic for saving image */
            try
            {
                var medication = (from med in _droneContext.Medications
                             where med.ID == medEntity.ID
                             select med).FirstOrDefault();

                if (medication == null) // add new med
                {
                    _droneContext.Medications.Add(medEntity);
                }
                else // edit med
                {
                    medication.Name = medEntity.Name; 
                    medication.Weight = medEntity.Weight; 
                    medication.Code = medEntity.Code; 
                    medication.Image = medEntity.Image; 
                }
                _droneContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Medication GetMedication(int id)
        {
            try
            {
                var medication = (from med in _droneContext.Medications
                             where med.ID == id
                             select med).FirstOrDefault();
                return medication;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool DeleteMedication(int id)
        {
            /*add validation id med attached to drone */
            try
            {
                var medication = (from med in _droneContext.Medications
                             where med.ID == id
                             select med).FirstOrDefault();
                _droneContext.Medications.Remove(medication);
                _droneContext.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


    }
}
