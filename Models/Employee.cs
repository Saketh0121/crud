using Sunny_Kasuvojula_A3__11_1_S4.CustomValidations;
using System.ComponentModel.DataAnnotations;
namespace Sunny_Kasuvojula_A3__11_1_S4.Models
{
    public class Employee
    {

        public int EmployeeId {  get; set; }
        [Required]
        public string? Firstname { get; set; } = null;
        [Required]
        public string? Lastname { get; set; }=null;

        [Required(ErrorMessage = "Date Of Birth is required")]
        [DataType(DataType.Date)]
        //custom validations
        [PastDateValidationAttribute(ErrorMessage = "Date of Birth must be a valid past date.")]
        public DateTime? DOB { get; set; } 

        [Required(ErrorMessage = "Date Of Hire is required")]
        [DataType(DataType.Date)]
        //custom validations
        [DateOfHireValidationAttribute(ErrorMessage = "Date of Hire must be on or after 01/01/1989.")]
        public DateTime? DateOfHire { get; set; }

        public int? ManagerId { get; set; } = null;
        //self referencing relation
        public Employee? Manager { get; set; } = null;
        

        //empty list where sales tables is linked with employee table
        public ICollection<Sales> Sales { get; set; } = new List<Sales>();
    }
}
