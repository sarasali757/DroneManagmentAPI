using DroneManagmentAPI.Models.Repository;
using DroneManagmentAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DroneManagmentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DispatchController : ControllerBase
    {
        DispatchRepository _dispatchRepository;
        public DispatchController(DispatchRepository dispatchRepository)
        {
            _dispatchRepository = dispatchRepository;
        }

        [HttpPost]
        [Route("LoadDrone")]
        public IActionResult LoadDrone([FromBody] DroneMedCustomEntity droneMedEntity)
        {
            var result = _dispatchRepository.LoadDrone(droneMedEntity);
            if (result)
                return Ok(result);

            return BadRequest(result);
        }
        [HttpGet]
        [Route("GetLoadedDroneDetails")]
        public IActionResult GetLoadedDroneDetails(int droneId)
        {
            var result = _dispatchRepository.LoadedDroneDetails(droneId);
            if (result == null)
                return NotFound();
            return Ok(result);
        }
        [HttpPatch]
        [Route("ChangeState")]
        public IActionResult ChangeDroneState(int droneId , int state)
        {
            var result = _dispatchRepository.ChangeDroneState(droneId, state);
            if (!result)
                return NotFound();
            return Ok(result);
        }
    }
}
