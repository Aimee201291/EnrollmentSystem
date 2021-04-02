using System.Collections.Generic;

namespace EnrollmentSystem.Services
{
    public interface IEnrollmentService
    {
        IEnumerable<Student> GetAllStudents();

        bool AddStudent(Student student);

        string EditStudent(int id, Student student);

        bool RemoveStudent(int id);
    }
}