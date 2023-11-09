using System.ComponentModel.DataAnnotations;
using Application.Entities;

namespace Api.Models
{
    public class CreateDependentRequest
    {
        [Required]
        public string? FirstName { get; set; }
        [Required]
        public string? LastName { get; set; }
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        public Relationship Relationship { get; set; }
        [Required]
        public int EmployeeId { get; set; }
    }
}
