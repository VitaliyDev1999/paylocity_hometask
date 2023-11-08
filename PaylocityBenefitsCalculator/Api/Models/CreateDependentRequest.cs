using Application.Entities;

namespace Api.Models
{
    public class CreateDependentRequest
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public Relationship Relationship { get; set; }
        public int EmployeeId { get; set; }
    }
}
