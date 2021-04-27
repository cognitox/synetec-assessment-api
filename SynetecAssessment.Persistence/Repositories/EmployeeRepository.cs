using Microsoft.EntityFrameworkCore;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Persistence.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Persistence.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _dbContext;

        public EmployeeRepository(AppDbContext context)
        {
            _dbContext = context;
        }
        public async Task<IEnumerable<Employee>> GetEmployeesAsync()
        {
            return await _dbContext
                .Employees
                .Include(e => e.Department)
                .ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int selectedEmployeeId)
        {
            return await _dbContext.Employees
                .Include(e => e.Department)
                .FirstOrDefaultAsync(item => item.Id == selectedEmployeeId);
        }

        public async Task<int> GetSalaryBudgetForCompanyAsync()
        {
            return await _dbContext.Employees.SumAsync(item => item.Salary);
        }
    }
}
