using DroneManagmentAPI.Models;
using DroneManagmentAPI.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DroneManagmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicationController : ControllerBase
    {
        MedicationRepository _medRepository;
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
        [Route("SaveDrone")]
        public IActionResult SaveDrone([FromBody] Medication medication)
        {

            return Ok(_medRepository.SaveMedication(medication));
        }
        [HttpDelete]
        [Route("DeleteDrone")]
        public IActionResult DeleteDrone(int id)
        {
            return Ok(_medRepository.DeleteMedication(id));
        }
    }
}
