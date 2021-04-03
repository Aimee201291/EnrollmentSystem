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

        [HttpGet]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            var result = _enrollmentService.GetAllStudents().ToList();
            return result;
        }

        [HttpPost]
        public ActionResult CreateStudent(Student student)
        {
            string systemMessage = _enrollmentService.AddStudent(student);
            
            if (systemMessage == "Successful")
            {
                return Ok();
            }
            else
            {
                return UnprocessableEntity(systemMessage);
            }
        }

        [HttpPut("{id}")]
        public ActionResult UpdateStudent(int id, [FromBody] Student student)
        {
            string systemMessage = _enrollmentService.EditStudent(id, student);

            if (systemMessage == "Successful")
            {
                return Ok();
            }
            if (systemMessage == "Not Found") {
                return NotFound(systemMessage);
            }

            return UnprocessableEntity(systemMessage);

        }

        [HttpDelete("{id}")]
        public ActionResult DeleteStudent(int id)
        {
            string systemMessage = _enrollmentService.RemoveStudent(id);
            
            if (systemMessage == "Successful")
            {
                return Ok();
            }
            if (systemMessage == "Not Found") {
                return NotFound(systemMessage);
            }

            return UnprocessableEntity(systemMessage);
        }       
    }
}