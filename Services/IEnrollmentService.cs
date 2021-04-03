using System.Collections.Generic;

namespace EnrollmentSystem.Services
{
    public interface IEnrollmentService
    {
        IEnumerable<Student> GetAllStudents();

        string AddStudent(Student student);

        string EditStudent(int id, Student student);

        string RemoveStudent(int id);
    }
}