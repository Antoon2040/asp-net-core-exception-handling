using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using School.API.Data;
using School.API.Data.Models;
using School.API.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace School.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("get-all-students")]
        public IActionResult GetAllStudents()
        {
            try
            {
                var allStudents = _context.Students.ToList();
                //throw new Exception("Could not get data from database");
                return Ok(allStudents);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
                throw;
            }
            finally
            {
                string stopHere = "Debug";
            }
        }

        [HttpGet("get-student-by-id/{id}")]
        public IActionResult GetStudentById(int id)
        {
            if (id <= 0) throw new ArgumentException($"Please, provide an id > 0");
            var allstudents = _context.Students.ToArray();
            var studentsPostion5 = allstudents[4];
            var studentinfo = _context.Students.FirstOrDefault(studentidentity =>
            studentidentity.Id == id);

            var studentFullName = studentinfo.FullName;

            return Ok($"Student name = {studentFullName}");
        }
        [HttpPost("add-new-student")]
        public IActionResult AddNewStudent([FromBody] Student payload)
        {
            try
            {
                if (Regex.IsMatch(payload.FullName, @"^\d")) throw new StudentNameExceotion("Name starts with number"
                            , payload.FullName);
                int age = CalculateAge(payload.DataOfBirth.Date);
                if (age < 20) throw new StudentAgeExceotion(@"Age lower than 20",age);
                _context.Students.Add(payload);
                _context.SaveChanges();

                return Created("", null);
            }
            catch (StudentNameExceotion ex)
            {
                return BadRequest($"{ex.StudentName} Start with a digit");
            }
            catch (StudentAgeExceotion ex)
            {
                return BadRequest($"Your age is {ex.Age} is lower then 20");
            }
        }
        static int CalculateAge(DateTime dateOfBirth)
        {
            DateTime now = DateTime.Today;
            int age = now.Year - dateOfBirth.Year;

            // Subtract a year if the birthday hasn't occurred yet this year
            if (now < dateOfBirth.AddYears(age))
            {
                age--;
            }

            return age;
        }
    }
}
