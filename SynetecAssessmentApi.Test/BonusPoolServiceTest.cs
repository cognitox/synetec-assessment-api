using Moq;
using System;
using Xunit;
using SynetecAssessmentApi.Persistence.Interfaces;
using SynetecAssessmentApi.Services;
using SynetecAssessmentApi.Domain;
using System.Collections.Generic;
using System.Linq;

namespace SynetecAssessmentApi.Test
{
    public class BonusPoolServiceTest
    {
        private Mock<IEmployeeRepository> _repository;
        private int _totalSalary;
        private List<Employee> _employees;
        public BonusPoolServiceTest() 
        {
            _employees = new List<Employee> { 
                new Employee(1, "John Smith", "Accountant (Senior)", 60000, 1),
                new Employee(2, "Janet Gone", "Temp Staff", 0, 2),
            };
            _totalSalary = 1000000;
            _repository = new Mock<IEmployeeRepository>();
            
            _repository.Setup(mock => mock.GetEmployeeByIdAsync(It.IsAny<int>())).ReturnsAsync(_employees.FirstOrDefault(e=>e.Id == 1));
            _repository.Setup(mock => mock.GetEmployeesAsync()).ReturnsAsync(_employees);
            _repository.Setup(mock => mock.GetSalaryBudgetForCompanyAsync()).ReturnsAsync(_totalSalary);
        }
        [Fact]
        public void Should_Return_The_Right_Bonus_Allocation()
        {
            // Arrange
            int bonusPoolAmount = 50000;
            int employeeId = 1;
            var employee = _employees.FirstOrDefault();

            //calculate the bonus allocation for the employee
            decimal bonusPercentage = (decimal)employee.Salary / (decimal)_totalSalary;
            int bonusAllocation = (int)(bonusPercentage * bonusPoolAmount);


            var service = new BonusPoolService(_repository.Object);

            // Act
            var result = service.CalculateAsync(bonusPoolAmount, employeeId).Result;

            // Assert
            Assert.Equal(bonusAllocation,result.Amount);
        }

        [Fact]
        public void Should_Return_Null_When_Employee_Not_Found()
        {
            // Arrange
            int bonusPoolAmount = 50000;
            int employeeId = 0;
            var employee = _employees.FirstOrDefault();


            //calculate the bonus allocation for the employee
            decimal bonusPercentage = (decimal)employee.Salary / (decimal)_totalSalary;
            int bonusAllocation = (int)(bonusPercentage * bonusPoolAmount);


            var service = new BonusPoolService(_repository.Object);
            _repository.Setup(mock => mock.GetEmployeeByIdAsync(It.IsAny<int>())).ReturnsAsync(_employees.FirstOrDefault(e=>e.Id == employeeId));

            // Act
            var result = service.CalculateAsync(bonusPoolAmount, employeeId).Result;

            // Assert
            Assert.Null(result);
        }

        /// <summary>
        /// Return 0 when Bonus Percentage or Employee Salary is Zero
        /// </summary>
        [Fact]
        public void CalculateAsync_Should_Return_Zero_When_Employee_Salary_Is_Zero()
        {
            // Arrange
            int bonusPoolAmount = 50000;
            int employeeId = 2;
            var employee = _employees.FirstOrDefault(e=>e.Id == employeeId);


            //calculate the bonus allocation for the employee
            decimal bonusPercentage = (decimal)employee.Salary / (decimal)_totalSalary;

            //BonusAllocation is 0
            int bonusAllocation = (int)(bonusPercentage * bonusPoolAmount);


            var service = new BonusPoolService(_repository.Object);
            _repository.Setup(mock => mock.GetEmployeeByIdAsync(It.IsAny<int>())).ReturnsAsync(_employees.FirstOrDefault(e => e.Id == employeeId));

            // Act
            var result = service.CalculateAsync(bonusPoolAmount, employeeId).Result;

            // Assert
            Assert.Equal(0, result.Amount);
        }



        [Fact]
        public void CalculateAsync_Should_Return_Zero_When_BonusPool_Is_Zero()
        {
            // Arrange
            int bonusPoolAmount = 0;
            int employeeId = 1;
            var employee = _employees.FirstOrDefault(e => e.Id == employeeId);


            //calculate the bonus allocation for the employee
            decimal bonusPercentage = (decimal)employee.Salary / (decimal)_totalSalary;

            //BonusAllocation is 0
            int bonusAllocation = (int)(bonusPercentage * bonusPoolAmount);


            var service = new BonusPoolService(_repository.Object);
            _repository.Setup(mock => mock.GetEmployeeByIdAsync(It.IsAny<int>())).ReturnsAsync(_employees.FirstOrDefault(e => e.Id == employeeId));

            // Act
            var result = service.CalculateAsync(bonusPoolAmount, employeeId).Result;

            // Assert
            Assert.Equal(0, result.Amount);
        }
    }
}
