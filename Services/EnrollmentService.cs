using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace EnrollmentSystem.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private IList<Student> _students;
        public int requestId = 1;

        public EnrollmentService()
        {
            _students = new List<Student>();
        }

        public string validateData (Student student, string operation) {
            string[] houses = { "Gryffindor", "Hufflepuff", "Ravenclaw", "Slytherin" };
            string dni = student.Dni;
            var dniFound = _students.FirstOrDefault(student => student.Dni == dni);
            
            if (student == null)
            {
                return "Vacío.";
            }
            else if ((dniFound != null) && (operation == "create"))
            {
                return "DNI inválido.";
            }
            else if (student.Name.Length > 20) 
            {
                return "El nombre no puede tener más de 20 caracteres.";
            }
            else if (!Regex.IsMatch(student.Name, @"^[a-zA-Z]+$"))
            {
                return "El nombre solo puede contener letras.";
            }
            else if (student.LastName.Length > 20) 
            {
                return "El apellido no puede tener más de 20 caracteres.";
            } 
            else if (!Regex.IsMatch(student.LastName, @"^[a-zA-Z]+$"))
            {
                return "El apellido solo puede contener letras.";
            }
            else if (student.Dni.Length > 10) {
                return "El DNI no puede tener más de 10 dígitos.";
            }
            else if (!Regex.IsMatch(dni, @"^[0-9]+$")) 
            {
                return "El DNI solo puede contener números.";
            }
            else if (student.Age.Length > 2) {
                return "La edad no puede tener más de 2 dígitos.";
            }
            else if (!Regex.IsMatch(student.Age, @"^[0-9]+$")) 
            {
                return "La edad solo puede contener números.";
            }
            else if (!houses.Contains(student.House)) 
            {
                return "Casa inválida. Solo puede elegir una de las siguientes: Gryffindor, Hufflepuff, Ravenclaw, Slytherin.";
            }

            return "Yes";
        }

        public IEnumerable<Student> GetAllStudents()
        {
            return _students;
        }

        public string AddStudent(Student student)
        {
            string validData = validateData(student, "create");

            if (validData == "Yes")
            {
                student.Id = requestId++;
                _students.Add(student);
                return "Successful";
            }

            return validData;
        }

        public string EditStudent(int id, Student student) {
            var target = _students.FirstOrDefault(student => student.Id == id);

            if (target == null)
            {
                return "Not Found";
            }

            string validData = validateData(student, "update");

            if (validData == "Yes")
            {
                target.Name = student.Name;
                target.LastName = student.LastName;
                target.Dni = student.Dni;
                target.Age = student.Age;
                target.House = student.House;
                return "Successful";
            }

            return validData;
        }

        public string RemoveStudent(int id) {
            var target = _students.FirstOrDefault(student => student.Id == id);
            if (target == null)
            {
                return "Not Found";
            }

            _students.Remove(target);
             return "Successful";
        }
    }
}