using System;

namespace School.API.Exceptions
{
    public class StudentAgeExceotion:Exception
    {
        public int Age { get; set; }
        public StudentAgeExceotion(string message, int age) : base(message)
        {
            Age = age;
        }
    }
}
