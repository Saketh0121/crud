using System.ComponentModel.DataAnnotations;

namespace Sunny_Kasuvojula_A3__11_1_S4.Models
{
    public class Sales
    {

        public int SalesId {  get; set; }
        [Required]
        [Range(0,4,ErrorMessage = "Quarter must be between 1 and 4.")]
        public int Quarter {  get; set; }
        [Required]
        [Range(2003, int.MaxValue,ErrorMessage = "Year must be after the year 2002.")]
        public int Year { get; set; }
        [Required]
        [Range(12, double.MaxValue,ErrorMessage = "Amount must be greater than 12.")]
        public double Amount { get; set; }
        [Required]
        public int EmployeeId { get; set; }
        public virtual Employee? Employee { get; set; } = null;
    }
}
