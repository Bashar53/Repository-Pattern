using EmployeeApp.Data;
using EmployeeApp.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<EmployeeDTO>> GetEmployees()
        {
            return Ok(EmployeeData.EmployeeList);
        }
        [HttpGet("{id:int}", Name = "GetEmployee")]
        [ProducesResponseType(StatusCodes.Status200OK)] //, Type = typeof(EmployeeDTO))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]// [ProducesResponseType(404)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//[ProducesResponseType(400)]
        public ActionResult<EmployeeDTO> GetEmployee(int EmployeeId)
        {
            if (EmployeeId < 1)
            {
                return BadRequest("Id must be greater than 0");
            }

            var employee = EmployeeData.EmployeeList.FirstOrDefault(x => x.EmployeeId == EmployeeId);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
    }
}
