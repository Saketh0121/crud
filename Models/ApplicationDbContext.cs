using Microsoft.EntityFrameworkCore;

namespace Sunny_Kasuvojula_A3__11_1_S4.Models
{
    //used to interact with daatabase
    public class ApplicationDbContext : DbContext
    {
        /*this is the primary class which interacts with the database*/
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { }
        /*These are the model classes(tables)*/
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Sales> Sales { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            /*This sets the EmployeeId as priamry key for the Employee table*/
            modelBuilder.Entity<Employee>()
                .HasKey(e => e.EmployeeId);

            /*This automatically incrments the count of primary key EmployeeId on adding the employee details */
            modelBuilder.Entity<Employee>()
                .Property(e => e.EmployeeId).ValueGeneratedOnAdd();

            /*This sets the salesId as priamry key for the Sales table*/
            modelBuilder.Entity<Sales>()
                .HasKey(e => e.SalesId);

            /*This automatically incrments the count of primary key salesId on adding the sales details */
            modelBuilder.Entity<Sales>()
                .Property(s => s.SalesId).ValueGeneratedOnAdd();

            //FLUENT API CONFIGURATION
            //This sets the one-to-many relations between employee and sales entities.
            modelBuilder.Entity<Sales>()
                .HasOne(s => s.Employee) // sales can have a single employee
                .WithMany(e => e.Sales) // employee can have multiple sales
                .HasForeignKey(s => s.EmployeeId); //set the foreign key on the sales entity
             
            //self-referencing relationship
            modelBuilder.Entity<Employee>()
                .HasOne(e => e.Manager).WithMany() //specifies manager as an employee with the reverse relations
                .HasForeignKey(e => e.ManagerId) //sets ManagerID as foreign key on Employee entity
                .OnDelete(DeleteBehavior.Restrict);

            //seed data into employee table
            modelBuilder.Entity<Employee>()
                .HasData(
                new Employee
                {
                    EmployeeId = 1,
                    Firstname = "Ada",
                    Lastname = "Lovelace",
                    DOB = new DateTime(1956, 12, 10),
                    DateOfHire = new DateTime(1995, 1, 1),
                    ManagerId=null // manager Id is null
                }
                );

            //seed data into sales table
            modelBuilder.Entity<Sales>().HasData(
                new Sales
                {
                    SalesId = 1,
                    Quarter = 1,
                    Year = 2024,
                    Amount = 5000,
                    EmployeeId = 1 //Employee with id 1 becomes manager for this employee
                });
        }


    }
}
