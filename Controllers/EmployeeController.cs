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
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ApplicationDbContext context, ILogger<EmployeeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            //check is the employees is enmpty, I empty display error or else display employees
            return _context.Employees != null ?
                View(await _context.Employees.ToListAsync()) :
                Problem("Employee set is null");
        }

        /*method to render the list of employees as managers in dropdown*/
        public IActionResult Add()
        {
            ViewBag.ManagerListDropdown = new SelectList(
                _context.Employees.Where(e => e.ManagerId == null),
                "EmployeeId",
                "Firstname",
                null
                );
            return View();
        }

        //Method to save and render the employee data
        [HttpPost]
        public async Task<IActionResult> Add([Bind("Firstname", "Lastname", "DOB", "DateOfHire", "ManagerId")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                //condition to check if the employee is already present in databse or not.
                var existingEmployee = await _context.Employees.FirstOrDefaultAsync(e => e.Firstname == employee.Firstname && e.Lastname == employee.Lastname && e.DOB == employee.DOB);
                if (existingEmployee != null)
                {
                    //display error message on validation
                    string displayErrorMessage = $"{employee.Firstname} {employee.Lastname} ({employee.DOB.Value.ToShortDateString()}) is already in the database.";
                    ModelState.AddModelError("DOB", displayErrorMessage);
                    return View(employee);
                }
                //Assign manager to a employee
                if (employee.ManagerId.HasValue)
                {
                    var manager = await _context.Employees.FindAsync(employee.ManagerId.Value);
                    //if firstname matches between employee and manager, set the validation error
                    if (manager != null && manager.Firstname == employee.Firstname)
                    {
                        ModelState.AddModelError("ManagerId", "Manager And Employee Cannot be same person.");
                        return View(employee);
                    }
                }
                //Add employee to database
                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            //logger to check for the errors.
            foreach (var modelState in ModelState)
            {
                var errors = modelState.Value.Errors;
                foreach (var error in errors)
                {
                    _logger.LogError($"Key: {modelState.Key}, Error: {error.ErrorMessage}");
                }
            }

            return View(employee);
        }
    }
}
