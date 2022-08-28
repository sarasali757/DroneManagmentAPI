using DroneManagmentAPI.Models;
using DroneManagmentAPI.Models.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DroneManagmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DroneController : ControllerBase
    {
        DroneRepository _droneRepository;
        public DroneController(DroneRepository droneRepository)
        {
            _droneRepository = droneRepository;
        }

        [HttpGet]
        public IActionResult GetDrones()
        {
            return Ok(_droneRepository.GetAllDrones());
        }
        [HttpGet]
        [Route("AvailableDrone")]
        public IActionResult GetAvailableDrones()
        {
            return Ok(_droneRepository.GetAvailableDrones());
        }
        [HttpGet]
        [Route("GetDrone")]
        public IActionResult GetDrone(int id)
        {
            var drone = _droneRepository.GetDrone(id);
            if (drone == null) 
                return NotFound(); 

            return Ok(drone);
        }

        [HttpPost]
        [Route("SaveDrone")]
        public IActionResult SaveDrone([FromBody]Drone drone) {

            return Ok(_droneRepository.SaveDrone(drone));
        }
        //[HttpDelete]
        //[Route("DeleteDrone")]
        //public IActionResult DeleteDrone(int id)
        //{
        //    return Ok(_droneRepository.DeleteDrone(id));
        //}
    }
}
