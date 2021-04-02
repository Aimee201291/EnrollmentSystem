using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EnrollmentSystem.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace EnrollmentSystem.Controllers
{
    [ApiController]
    [Route("api/enrollment/student")]
    public class EnrollmentController : ControllerBase
    {
        private IEnrollmentService _enrollmentService; 

        public EnrollmentController(IEnrollmentService enrollmentService)
        {
            _enrollmentService = enrollmentService;
        }

        public string validateData (Student student) {
            string dni = student.Dni.ToString();
            string age = student.Age.ToString();
            string[] houses = { "Gryffindor", "Hufflepuff", "Ravenclaw", "Slytherin" };

            if (student == null)
            {
                return "Vacío";
            }
            if (student.Name.Length > 20) {
                return "La longitud del nombre no debe superar los 20 caracteres";
            }
            else if (!Regex.IsMatch(student.Name, @"^[a-zA-Z]+$"))
            {
                return "El nombre solo puede contener letras";
            }
            else if (student.LastName.Length > 20) 
            {
                return "La longitud del apellido no debe superar los 20 caracteres";
            } 
            else if (!Regex.IsMatch(student.LastName, @"^[a-zA-Z]+$"))
            {
                return "El apellido solo puede contener letras";
            }
            else if (student.Dni > 9999999999) {
                return "El DNI no debe superar los 10 dígitos";
            }
            else if (!Regex.IsMatch(dni, @"^[0-9]+$")) 
            {
                return "El DNI solo puede contener números";
            }
            else if (student.Age > 99) {
                return "La edad no debe superar los 2 dígitos";
            }
            else if (!Regex.IsMatch(age, @"^[0-9]+$")) 
            {
                return "La edad solo puede contener números";
            }
            else if (!houses.Contains(student.House)) 
            {
                return "Casa inválida. Solo puede elegir una de las siguientes: Gryffindor, Hufflepuff, Ravenclaw, Slytherin";
            }

            return "Valid data";
        }

        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            /*if(_enrollmentService == null)
            {
                return NotFound();
            }*/

            var result = _enrollmentService.GetAllStudents().ToList();
            return result;
        }

        [HttpPost]
        public ActionResult CreateStudent(Student student)
        {
            /*if(_enrollmentService == null)
            {
                return NotFound();
            }*/

            string validData = validateData(student);

            if (validData == "Valid data")
            {
                bool successfulCreation = _enrollmentService.AddStudent(student);
                if (successfulCreation)
                {
                    return Ok();
                }
                else
                {
                    return UnprocessableEntity("El valor de DNI indicado es inválido");
                }
            }

            return UnprocessableEntity(validData);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateStudent(int id, [FromBody] Student student)
        {
            string validData = validateData(student);

            if (validData == "Valid data")
            {
                string successfulUpdate = _enrollmentService.EditStudent(id, student);
                if (successfulUpdate == "successful")
                {
                    return Ok();
                } 
                else if (successfulUpdate == "Not Found") 
                {
                    return NotFound();
                }
                else if (successfulUpdate == "Invalid DNI")
                {
                    return UnprocessableEntity("El valor de DNI indicado es inválido");
                }
            }

            return BadRequest(validData);
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteStudent(int id)
        {
            bool successfulDelete = _enrollmentService.RemoveStudent(id);
            
            if (successfulDelete)
            {
                return Ok();
            } else 
            {
                return NotFound();
            }
        }        
    }
}