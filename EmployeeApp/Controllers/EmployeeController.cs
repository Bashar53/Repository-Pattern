using AutoMapper;
using EmployeeApp.DbConnection;
using EmployeeApp.Models;
using EmployeeApp.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<EmployeeController> _logger;
        protected APIResponse _response;
        public EmployeeController(ILogger<EmployeeController> logger, ApplicationDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<EmployeeDTO>>> Getemployees()
        {
            _logger.LogInformation("Getting all Employee");
            return Ok(await _dbContext.Employee.ToListAsync());
        }
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//[ProducesResponseType(400)]
        public async Task<ActionResult<EmployeeDTO>> GetEmployees(int id)
        {
            if (id < 1)
            {
                _logger.LogError("Get employee Error with Id " + id);
                return BadRequest("Id must be greater than 0");
            }
            var employee = await _dbContext.Employee.FirstOrDefaultAsync(x => x.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }
    }
          
    
}
