/*
 * First name : Sunny 
 * Student ID : 8898416
 */

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Sunny_Kasuvojula_A3__11_1_S4.Models;
using System.Threading.Tasks;

namespace Sunny_Kasuvojula_A3__11_1_S4.Controllers
{
    public class SalesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SalesController> _logger;

        public SalesController(ApplicationDbContext context, ILogger<SalesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            if (_context.Sales == null)
            {
                return Problem("Entity set 'Sales' is null.");
            }
            //This query is used to get the sales data fot the employee
            var sales = await _context.Sales
                                      .Include(s => s.Employee) // Load related Employee data
                                      .ToListAsync();

            return View(sales);
        }

        public IActionResult Add()
        {
            ViewBag.EmployeeListDropdown = new SelectList(_context.Employees, "EmployeeId", "Firstname");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add([Bind("EmployeeId", "Quarter", "Year", "Amount")] Sales sales)
        {
            if (ModelState.IsValid)
            {
                var existingSales = await _context.Sales.FirstOrDefaultAsync(s => s.Quarter == sales.Quarter && s.Year == sales.Year && s.EmployeeId == sales.EmployeeId);
                if (existingSales != null)
                {
                    // 
                    var employee = await _context.Employees.FindAsync(sales.EmployeeId);
                    if (employee != null)
                    {
                        //display the error message if employee exists in database
                        string displayErrorMessage = $"Sales for {employee.Firstname} {employee.Lastname} for Q{sales.Quarter} of Year {sales.Year} are already in the database.";
                        ModelState.AddModelError("EmployeeId", displayErrorMessage);
                    }
                    ViewBag.EmployeeListDropdown = new SelectList(_context.Employees, "EmployeeId", "Firstname", sales.EmployeeId);
                    return View(sales);
                }

                _context.Sales.Add(sales);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // to log errors
            foreach (var modelState in ModelState)
            {
                var errors = modelState.Value.Errors;
                foreach (var error in errors)
                {
                    _logger.LogError($"Key: {modelState.Key}, Error: {error.ErrorMessage}");
                }
            }

            ViewBag.EmployeeListDropdown = new SelectList(_context.Employees, "EmployeeId", "Firstname", sales.EmployeeId);
            return View(sales);
        }
    }
}
