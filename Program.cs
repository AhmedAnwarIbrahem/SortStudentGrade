using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortStudentGrade
{
    class Program
    {
       public static void Main(string[] args)
        {
            var students = GetStudents(10).ToList();
            PrintStudents(students, "Random Students");
            var orderedStudents = students.OrderByDescending(a => a.Degree).ToList();
            PrintStudents(orderedStudents, "Sorted Students");
            int grouped = 1;
            var groupedStudents = orderedStudents.GroupBy(r => r.Degree).Select(group => new DegreesOrder
            {
                Degree = group.Key,
                //Students = students.Where(a=>a.Degree == group.Key).ToList(),
                Count = group.Count(),
                StudentsNames = string.Join(", ", students.Where(a => a.Degree == group.Key).Select(a=>a.Name).ToList()),
                Order = grouped == 1 ? $"{grouped++}st" :
                        grouped == 2 ? $"{grouped++}nd" :
                        grouped == 3 ? $"{grouped++}rd" : 
                        $"{grouped++} th"
            }).ToList();
            PrintResult(groupedStudents, "Result Data");


        }

        private static void PrintStudents(List<Student> students, string textMsg)
        {
            StringBuilder res = new StringBuilder();
            students.ForEach(st =>
            {
                res.Append($"{st.Name} - {st.Degree} \n");
            });
            Console.WriteLine($"{textMsg}: \n {res} \n");
        }

        private static void PrintResult(List<DegreesOrder> degreesOrder, string textMsg)
        {
            StringBuilder res = new StringBuilder();
            res.Append($"{textMsg}: \n");
            degreesOrder.ForEach(st =>
            {
                res.Append($"{st.Order}  Degree:{st.Degree} \n {st.Count} Student({st.StudentsNames}) \n");
                res.Append("------------------ \n");
            });
            Console.WriteLine($"{res} \n");
        }
        private static IEnumerable<Student> GetStudents(int nth)
        {
            var random = new Random();
            for (var i=0; i < nth; i++)
            {
                var grade = random.Next(1,10);
                var test = new Student()
                {
                    Id = Guid.NewGuid(),
                    Name = GenerateRandomName(),
                    Degree = grade
                };
              yield return  test;
            }
        }


        private static string GenerateRandomName()
        {
            var personName = string.Empty;
            string[] firstNames = { "Ahmed", "Mohamed", "Said", "Tamer", "Ashraf", "Noha", "Mai", "Adel", "Soaad", "Zeina" , "Abdelghany", "Ramadan", "Aya", "Salwa" };
            string[] lastNames = { "Anwar", "Attallah", "Hassan", "Taher", "Mahmoud" };
            personName = firstNames.OrderBy(a=>Guid.NewGuid()).FirstOrDefault() + " " + lastNames.OrderBy(a => Guid.NewGuid()).FirstOrDefault();
            return personName;
        }
    }

    public class Student
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Degree { get; set; }
        public string Grade { get; set; }

    }

    public class DegreesOrder
    {
        public string Order { get; set; }
        public int Degree { get; set; }
        public string StudentsNames { get; set; }
        public int Count { get; set; }
    }
}
