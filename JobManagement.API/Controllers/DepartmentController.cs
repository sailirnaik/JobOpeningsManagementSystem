using JobOpeningsManagementMS.JobManagement.BusinessManager.DTOs;
using JobOpeningsManagementMS.JobManagementAPI.BusinessManager.Interfaces;
using JobOpeningsManagementMS.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JobOpeningsManagementMS.JobManagementAPI.API.Controllers
{
    [ApiController]
    [Route("api/v1/departments")]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;

        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }



        /// <summary>
        /// Creates a new department.
        /// </summary>
        /// <param name="dto">The Department details.</param>
        /// <returns>Success code</returns>
        [Authorize]
        [HttpPost]
        public IActionResult Create([FromBody] DepartmentCreateDTO dto)
        {
            _departmentService.Create(dto);
            return StatusCode(201); 
        }


        /// <summary>
        /// Updates existing department details based on Id.
        /// </summary>
        /// <param name="dto">The department details.</param>
        /// <returns>Success or not found message.</returns>
        [Authorize]
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] DepartmentCreateDTO dto)
        {
            _departmentService.Update(id, dto);
            return Ok();
        }

        /// <summary>
        /// Gets all the existing departments.
        /// </summary>
        /// <returns>List of departments</returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _departmentService.GetAll();
            return Ok(result);
        }
    }


}
