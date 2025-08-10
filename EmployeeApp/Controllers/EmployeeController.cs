using EmployeeApp.Data;
using EmployeeApp.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<EmployeeDTO>> GetEmployee()
        {   
            return Ok(EmployeeData.EmployeeList);
        }
        [HttpGet("{id:int}", Name = "GetEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)] //, Type = typeof(EmployeeDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]// [ProducesResponseType(404)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//[ProducesResponseType(400)]
        public ActionResult<EmployeeDTO> GetEmployee(int id)
        {
            if (id < 1)
            {
                return BadRequest("Id must be greater than 0");
            }
            var employee = EmployeeData.EmployeeList.FirstOrDefault(x => x.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<EmployeeDTO> CreateEmployee([FromBody] EmployeeDTO employeeDTO)
        {
            // not required with data annotations?
            // wont hit breakpoint if model is invalid
            if (!ModelState.IsValid) // checks if the model is valid, i.e. if the data annotations are respected
            {
                return BadRequest(ModelState); // ModelState will contain the errors
            }
            // check if Employee name already exists
            if (EmployeeData.EmployeeList.Any(x => x.Name == employeeDTO.Name))
            {
                ModelState.AddModelError("", "Employee name must be unique"); // key can be empty string
                return StatusCode(StatusCodes.Status409Conflict, ModelState);
            }
            if (employeeDTO == null)
            {
                return BadRequest(employeeDTO);
            }

            if (employeeDTO.EmployeeId > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            var employeeData = new EmployeeDTO
            {
                EmployeeId = EmployeeData.EmployeeList.Max(x => x.EmployeeId) + 1,
                Name = employeeDTO.Name
            };
            EmployeeData.EmployeeList.Add(employeeData); // EmployeeDto
            return CreatedAtAction(nameof(GetEmployee), new { id = employeeData.EmployeeId }, employeeData);
            // CreatedAtRoute: return CreatedAtRoute("GetEmployee", new { id = employeeData.Id }, employeeData);
            // CreatedAtAction - returns a 201 status code and a location header with the URI of the newly created resource
        }
    }
}
