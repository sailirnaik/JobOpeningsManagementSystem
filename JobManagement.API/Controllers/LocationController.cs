using JobOpeningsManagementMS.JobManagement.BusinessManager.DTOs;
using JobOpeningsManagementMS.JobManagementAPI.BusinessManager.Interfaces;
using JobOpeningsManagementMS.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace JobOpeningsManagementMS.JobManagementAPI.API.Controllers
{
    [ApiController]
    [Route("api/v1/locations")]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }


        /// <summary>
        /// Creates a new location.
        /// </summary>
        /// <param name="dto">The location details.</param>
        /// <returns>Success code</returns>
        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] LocationCreateDTO dto)
        {
            _locationService.Create(dto);
            return StatusCode(201); 
        }


        /// <summary>
        /// Updates existing location details based on Id.
        /// </summary>
        /// <param name="dto">The location details.</param>
        /// <returns>Success or not found message.</returns>
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] LocationCreateDTO dto)
        {
            _locationService.Update(id, dto);
            return Ok();
        }

        /// <summary>
        /// Gets all the existing locations.
        /// </summary>
        /// <returns>List of locations</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _locationService.GetAll();
            return Ok(result);
        }
    }
}

