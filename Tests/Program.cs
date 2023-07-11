using System;

namespace Tests
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }


        public bool IsTeenAge
        {
            get
            {
                return this.Age > 12 && this.Age < 18;

            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Func<Student, int, int, bool> isTeenAge =
                (e, startAge, endAge) =>
                {
                    return e.Age > startAge && e.Age < endAge;
                };


            bool result = isTeenAge(new Student
            {
                Id = 1,
                Name = "basel",
                Age = 28
            }, 12, 18);

            Student student = new Student() 
            {
                Id = 1,
                Name = "basel",
                Age = 28
            };
            Console.WriteLine(student.IsTeenAge);
            Console.WriteLine("Hello World!");
            Console.ReadLine();
        }
    }
}
