using SynetecAssessmentApi.Domain;

namespace SynetecAssessmentApi.Dtos
{
    public class DepartmentDto
    {
        public string Title { get; set; }
        public string Description { get; set; }

        public DepartmentDto(Department department)
        {
            Title = department.Title;
            Description = department.Description;
        }
    }
}
