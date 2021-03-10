using System.Data;
using System.Data.SqlClient;
using System.IO;
using ExamplesApp.Coupling;

namespace ExamplesApp.ControlCoupling
{
    /// <summary>
    /// 1) Refactor EmployeeStorage to remove control coupling.
    /// 2) Compare your new design with the starting code. Explain how the new design helps create a more flexible and maintainable software solution.
    /// TODO: Return to this example in the end (After DIP). Service might want to save a backup to file - we can still use two IRepositories to maintain flexibility.
    /// </summary>
    class EmployeeService
    {
        private readonly EmployeeStorage _storage = new EmployeeStorage();

        public void SaveEmployee(Employee emp)
        {
            _storage.Save(emp, true);
        }
    }

    class EmployeeStorage
    {
        private readonly string _fileLocation = "C:/EmployeeStorage";
        private readonly string _connectionString = ConfigurationManager.AppSettings["connectionString"];
        public void Save(Employee newEmployee, bool toSQL)
        {
            if (toSQL)
            {
                using (SqlConnection connection = new SqlConnection(_connectionString))
                {
                    string insertSQL = "insert Employee(Name, Surname) values(@Name, @Surname)";
                    using (SqlCommand command = new SqlCommand(insertSQL, connection))
                    {
                        command.CommandType = CommandType.Text;
                        command.Parameters.AddWithValue("@Name", newEmployee.Name);
                        command.Parameters.AddWithValue("@Surname", newEmployee.Surname);
                        connection.Open();
                    }
                }
            }
            else
            {
                int id = GetLastId();
                string employeeForFile = id + ":" + newEmployee.Name + ":" + newEmployee.Surname + "\n";
                File.AppendAllText(_fileLocation, employeeForFile);
            }
        }

        private int GetLastId()
        {
            //TODO: Implement logic to get suitable ID.
            throw new System.NotImplementedException();
        }
    }
}
