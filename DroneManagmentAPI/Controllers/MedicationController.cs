using BrunoZell.ModelBinding;
using DroneManagmentAPI.Models;
using DroneManagmentAPI.Models.Repository;
using DroneManagmentAPI.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace DroneManagmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationController : ControllerBase
    {
        private readonly MedicationRepository _medRepository;

        public MedicationController(MedicationRepository medicationRepository)
        {
            _medRepository = medicationRepository;
        }

        [HttpGet]
        public IActionResult GetMedications()
        {
            return Ok(_medRepository.GetAllMedications());
        }
        [HttpGet]
        [Route("GetMedication")]
        public IActionResult GetMedication(int id)
        {
            var drone = _medRepository.GetMedication(id);
            if (drone == null)
                return NotFound();

            return Ok(drone);
        }

        [HttpPost]
        [Route("SaveMedication")]
        public IActionResult SaveMedication( IFormFile File, [FromForm] Medication medication)
        {
            return Ok(_medRepository.SaveMedication(medication, File));
        }
        //[HttpDelete]
        //[Route("DeleteMedication")]
        //public IActionResult DeleteMedication(int id)
        //{
        //    return Ok(_medRepository.DeleteMedication(id));
        //}

     
    }

}
