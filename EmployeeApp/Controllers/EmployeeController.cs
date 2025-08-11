using AutoMapper;
using EmployeeApp.DbConnection;
using EmployeeApp.Models;
using EmployeeApp.Models.DTO;
using EmployeeApp.Repository.IRepository;
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
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        protected APIResponse _response;
        public EmployeeController(ILogger<EmployeeController> logger, ApplicationDbContext dbContext, IEmployeeRepository employeeRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _dbContext = dbContext;
            _employeeRepository = employeeRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _response = new();
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetEmployees()
        {
            _logger.LogInformation("Getting all employees");

            var employee = await _unitOfWork.employee.GetAllAsync();
            _response.Result = _mapper.Map<List<EmployeeDTO>>(employee);
            _response.StatusCode = System.Net.HttpStatusCode.OK;
            _response.IsSuccess = true;
            return Ok(_response);
        }

        [HttpGet("{id:int}", Name = "GetEmployeeById")]
        [ProducesResponseType(StatusCodes.Status200OK)] //, Type = typeof(VillaDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]// [ProducesResponseType(404)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]//[ProducesResponseType(400)]
        public async Task<ActionResult<EmployeeDTO>> GetEmployeeById(int id)
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


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> CreateEmployee([FromBody] EmployeeCreateDTO createDto)
        {
            // not required with data annotations?
            // wont hit breakpoint if model is invalid
            if (!ModelState.IsValid) // checks if the model is valid, i.e. if the data annotations are respected
            {
                return BadRequest(ModelState); // ModelState will contain the errors
            }

            // check if villa name already exists
            if (_dbContext.Employee.Any(x => x.Name == createDto.Name))
            {
                ModelState.AddModelError("", "Employee name must be unique"); // key can be empty string
                return StatusCode(StatusCodes.Status409Conflict, ModelState);
            }

            if (createDto == null)
            {
                return BadRequest(createDto);
            }

            var model = _mapper.Map<Employee>(createDto);

            await _unitOfWork.employee.CreateAsync(model);

            _response.Result = _mapper.Map<EmployeeDTO>(model);
            _response.StatusCode = System.Net.HttpStatusCode.Created;

            return CreatedAtAction(nameof(GetEmployeeById), new { id = model.EmployeeId }, _response);
        }

    }
}



