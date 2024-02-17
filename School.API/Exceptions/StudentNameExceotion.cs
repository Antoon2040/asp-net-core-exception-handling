using System;

namespace School.API.Exceptions
{
    public class StudentNameExceotion:Exception
    {
        public string StudentName { get; set; }
        public StudentNameExceotion(string message):base (message) 
        {
        
        }
        public StudentNameExceotion(string message ,Exception innerException):base (message, innerException) 
        {

        }
        public StudentNameExceotion(string message, string studentName) : base(message)
        {
            StudentName = studentName;
        }
    }
}
