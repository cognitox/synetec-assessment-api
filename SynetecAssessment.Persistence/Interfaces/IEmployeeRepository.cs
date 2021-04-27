using SynetecAssessmentApi.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Persistence.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployeeByIdAsync(int selectedEmployeeId);
        Task<IEnumerable<Employee>> GetEmployeesAsync();
        Task<int> GetSalaryBudgetForCompanyAsync();
    }
}