using Microsoft.AspNetCore.Mvc;
using SynetecAssessmentApi.Dtos;
using SynetecAssessmentApi.Interfaces;
using System.Threading.Tasks;
using SynetecAssessmentApi.Constants;

namespace SynetecAssessmentApi.Controllers
{
    [Route("api/[controller]")]
    public class BonusPoolController : Controller
    {
        private IBonusPoolService _service;
        public BonusPoolController(IBonusPoolService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetEmployeesAsync());
        }

        [HttpPost()]
        public async Task<IActionResult> CalculateBonus([FromBody] CalculateBonusDto request)
        {
            if (request.SelectedEmployeeId == default)
                return BadRequest(ExceptionMessages.EmployeeIdNotValid(request.SelectedEmployeeId));

            var result = await _service.CalculateAsync(
                            request.TotalBonusPoolAmount,
                            request.SelectedEmployeeId);

            if (result == null)
                return BadRequest(ExceptionMessages.EmployeeDoesntExist(request.SelectedEmployeeId));

            return Ok(result);
        }
    }
}
