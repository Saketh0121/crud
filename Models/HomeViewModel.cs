using Microsoft.AspNetCore.Mvc.Rendering;
using Sunny_Kasuvojula_A3__11_1_S4.Models;

public class HomeViewModel
{
    public List<Sales> Sales { get; set; }
    public List<Employee> Employees { get; set; }
    public SelectList EmployeeSelectList { get; set; }
    public int? selectedEmployeeIdFromDropdown { get; set; }
}
