using System.Collections.Generic;
using System.Linq;

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

        public IEnumerable<Student> GetAllStudents()
        {
            return _students;
        }

        public bool AddStudent(Student student)
        {
            long dni = student.Dni; 
            var dniFound = _students.FirstOrDefault(student => student.Dni == dni);
            if (dniFound == null)
            {
                student.Id = requestId++;
                _students.Add(student);
                return true;
            }
            else
            {
                return false;
            }
        }

        public string EditStudent(int id, Student student) {
            var target = _students.FirstOrDefault(student => student.Id == id);
            if (target == null)
            {
                return "Not Found";
            }

            long dni = student.Dni; 
            var dniFound = _students.FirstOrDefault(student => student.Dni == dni);
            if (dniFound == null)
            {
                target.Id = student.Id;
                target.Name = student.Name;
                target.LastName = student.LastName;
                target.Dni = student.Dni;
                target.Age = student.Age;
                target.House = student.House;
                return "successful";
            }
            else
            {
                return "Invalid DNI";
            }
        }

        public bool RemoveStudent(int id) {
            var target = _students.FirstOrDefault(student => student.Id == id);
            if (target == null)
            {
                return false;
            } 
            else
            {
                _students.Remove(target);
                return true;
            }
        }
    }
}