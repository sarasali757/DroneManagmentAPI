using DroneManagmentAPI.ViewModels;
using Microsoft.AspNetCore.Http;
namespace DroneManagmentAPI.Models.Repository
{
    public class MedicationRepository
    {
        readonly DroneContext _droneContext;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MedicationRepository(DroneContext droneContext, IWebHostEnvironment webHostEnvironment)
        {
            _droneContext = droneContext;
            _webHostEnvironment = webHostEnvironment;
        }

        public List<Medication> GetAllMedications()
        {
            return (from med in _droneContext.Medications
                    select med ).ToList();
        }

        public bool SaveMedication(Medication medEntity ,IFormFile file)
        {
            /*need logic for saving image */
            try
            {
                var medication = (from med in _droneContext.Medications
                             where med.ID == medEntity.ID
                             select med).FirstOrDefault();

                if (medication == null) // add new med
                {
                    var imagePath = UploadedFile(file);
                    _droneContext.Medications.Add(new Medication()
                    {
                        Name = medEntity.Name,
                        Weight = medEntity.Weight,
                        Code = medEntity.Code,
                        Image = imagePath
                    });
                }
                else // edit med
                {
                    medication.Name = medEntity.Name; 
                    medication.Weight = medEntity.Weight; 
                    medication.Code = medEntity.Code;
                    var deleteOldImg = DeleteFile(medication.Image);
                    medication.Image = UploadedFile(file);
                   
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
        public string UploadedFile(IFormFile file)
        {
            string uniqueFileName = null;
            if (file != null)
            {
                string uploadsFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }
        public bool DeleteFile(string fileName)
        {
            try {
                string uploadsFolder = Path.Combine(_webHostEnvironment.ContentRootPath, "images");
                string filePath = Path.Combine(uploadsFolder, fileName);
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                return true;
            }
            catch (Exception) { return false; }
        }
    }
}
