/*
 * First name : Sunny 
 * Student ID : 8898416
 */

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sunny_Kasuvojula_A3__11_1_S4.Models;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Sunny_Kasuvojula_A3__11_1_S4.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            ViewBag.DisplayEmployeeList = new SelectList(_context.Employees, "EmployeeId", "Firstname");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(int selectedEmployeeIdFromDropdown)
        {
            //condition to check the list of employees
            if (selectedEmployeeIdFromDropdown == 0)
            {
                ViewBag.DisplayEmployeeList = new SelectList(_context.Employees, "EmployeeId", "Firstname");
                return View();
            }
            //This query fetches the sales data for the dropdown selected employee
            var sales = await _context.Sales
                                      .Where(s => s.EmployeeId == selectedEmployeeIdFromDropdown)
                                      .Include(s => s.Employee)
                                      .ToListAsync();
            //displays the sales data related to the selected employee
            ViewBag.DisplayEmployeeList = new SelectList(_context.Employees, "EmployeeId", "Firstname", selectedEmployeeIdFromDropdown);
            return View(sales);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
