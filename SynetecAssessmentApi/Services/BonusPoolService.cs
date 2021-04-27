using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Dtos;
using SynetecAssessmentApi.Interfaces;
using SynetecAssessmentApi.Persistence.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Services
{
    public class BonusPoolService : IBonusPoolService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public BonusPoolService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
        {
            IEnumerable<Employee> employees = await _employeeRepository.GetEmployeesAsync();

            List<EmployeeDto> result = new List<EmployeeDto>();

            foreach (var employee in employees)
            {
                result.Add(new EmployeeDto(employee));             
            }

            return result;
        }

        public async Task<BonusPoolCalculatorResultDto> CalculateAsync(int bonusPoolAmount, int selectedEmployeeId)
        {
            //load the details of the selected employee using the Id
            Employee employee = await _employeeRepository.GetEmployeeByIdAsync(selectedEmployeeId);

            if (employee == null)
                return null;

            //get the total salary budget for the company
            int totalSalary = await _employeeRepository.GetSalaryBudgetForCompanyAsync();

            //calculate the bonus allocation for the employee
            decimal bonusPercentage = (decimal)employee.Salary / (decimal)totalSalary;
            int bonusAllocation = (int)(bonusPercentage * bonusPoolAmount);

            return new BonusPoolCalculatorResultDto
            {
                Employee = new EmployeeDto(employee),
                Amount = bonusAllocation
            };
        }
    }
}
