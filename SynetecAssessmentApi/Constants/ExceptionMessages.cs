using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Constants
{
    public class ExceptionMessages
    {
        public static string EmployeeIdNotValid (int id)=> $"Employee ID {id} is invalid";
        public static string EmployeeDoesntExist (int id)=> $"Employee doesn't exist for Employee ID {id}";
    }
}
