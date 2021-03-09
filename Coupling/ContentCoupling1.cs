using System;

namespace ExamplesApp.Coupling
{
    /// <summary>
    /// How would you reduce couplingStrength(EmployeeService, Employee)?
    /// Refactor the code to do so; TODO: Send to RepositoryCompiler to check if AccessedFieldsAndMembers is 0 and InvokedMethods is 1
    /// Which additional steps would you take to enhance Employee encapsulation?
    /// </summary>
    class EmployeeService
    {
        void RetireEmployee(Employee e)
        {
            if (e.Status != EmploymentStatus.Active) throw new InvalidOperationException();
            e.Status = EmploymentStatus.Retired;
            e.DateOfRetirement = DateTime.Now;
            e.YearsWorked = e.DateOfRetirement.Year - e.EmploymentDate.Year;
        }
    }

    class Employee
    {
        public EmploymentStatus Status { get; set; }
        public DateTime DateOfRetirement { get; set; }
        public int YearsWorked { get; set; }
        public DateTime EmploymentDate { get; set; }
        public object Name { get; internal set; }
        public object Surname { get; internal set; }
    }

    internal enum EmploymentStatus
    {
        Active,
        Retired
    }
}
