using Employees.Entities;
using System.Text;

namespace Employees.Repositories {
    internal class EmployeesRepository {
        private const string _path = "./employees.txt";


        public EmployeesRepository() {

        }


        public void Add(Employee employee) {
            using (StreamWriter writer = new StreamWriter(_path, true)) {

                writer.WriteLine(employee.ToString());
            }
        }


        public string GetAll() {
            if (File.Exists(_path)) {
                using (StreamReader reader = new StreamReader(_path)) {
                    StringBuilder sb = new StringBuilder();
                    string? text;
                    while ((text = reader.ReadLine()) != null) {
                        sb.Append(text);
                        sb.Append(Environment.NewLine);
                    }
                    return sb.ToString();
                }
            } else {
                return "Данные по сотрудникам отсутствуют.";
            }
        }
    }
}
