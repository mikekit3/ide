using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradesCalculatorV4
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();

            Console.WriteLine("Students Count: 100");
            stopwatch.Start();
            CreateStudents(100);
            stopwatch.Stop();
            Console.WriteLine("Total Time Taken: {0}", stopwatch.Elapsed + Environment.NewLine);
            stopwatch.Reset();

            Console.WriteLine("Students Count: 1000");
            stopwatch.Start();
            CreateStudents(1000);
            stopwatch.Stop();
            Console.WriteLine("Total Time Taken: {0}", stopwatch.Elapsed + Environment.NewLine);
            stopwatch.Reset();

            Console.WriteLine("Students Count: 10000");
            stopwatch.Start();
            CreateStudents(10000);
            stopwatch.Stop();
            Console.WriteLine("Total Time Taken: {0}", stopwatch.Elapsed + Environment.NewLine);
            stopwatch.Reset();

            Console.WriteLine("Students Count: 100000");
            stopwatch.Start();
            CreateStudents(100000);
            stopwatch.Stop();
            Console.WriteLine("Total Time Taken: {0}", stopwatch.Elapsed + Environment.NewLine);
            stopwatch.Reset();

            Console.ReadKey();
        }
        public static double GetFinalPoints(double homeworkAvg, double examResult)
        {
            return (0.3 * homeworkAvg) + (0.7 * examResult);
        }

        public static double GetMedian(List<double> homeworkResults, double examResult)
        {
            homeworkResults.Add(examResult);
            List<int> integers = homeworkResults.Select(d => (int)d).ToList();
            int[] numbers = integers.ToArray();

            int numberCount = numbers.Count();
            int halfIndex = numbers.Count() / 2;
            var sortedNumbers = numbers.OrderBy(n => n);
            double median;
            if ((numberCount % 2) == 0)
            {
                median = ((sortedNumbers.ElementAt(halfIndex) +
                    (sortedNumbers.ElementAt(halfIndex - 1))) / 2);
            }
            else
            {
                median = sortedNumbers.ElementAt(halfIndex);
            }
            return median;
        }

        public static void PrintResults(List<Student> students)
        {
            Console.Clear();
            students = students.OrderBy(s => s.fullName.surname).ToList(); // sorting list by surname
            Console.WriteLine("Surname\t\tName\t\t\tFinalPoints (Avg.)  /  Final Points (Med.)");
            Console.WriteLine("------------------------------------------------------------------------------");
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine("{0}\t\t{1}\t\t\t{2}\t\t\t{3}", students[i].fullName.surname, students[i].fullName.name, Math.Round(students[i].finalPoints, 1), students[i].median);
            }
        }

        public static void WriteToFile(List<Student> students)
        {
            students = students.OrderBy(s => s.fullName.surname).ToList(); // sorting list by surname
            List<Student> studentsPass = new List<Student>();
            List<Student> studentsFail = new List<Student>();
            Stopwatch stopwatch = new Stopwatch();

            string fileName = "students" + students.Count + ".txt";

            Console.WriteLine("Making a text file of all students.");
            stopwatch.Start();
            using (StreamWriter writetext = new StreamWriter(fileName))
            {
                writetext.WriteLine("Surname\t\tName\t\t\tFinalPoints (Avg.)  /  Final Points (Med.)");
                writetext.WriteLine("------------------------------------------------------------------------------");
                for (int i = 0; i < students.Count; i++)
                {
                    writetext.WriteLine("{0}\t\t{1}\t\t\t{2}\t\t\t{3}", students[i].fullName.surname, students[i].fullName.name, Math.Round(students[i].finalPoints, 1), students[i].median);
                }
            }
            stopwatch.Stop();
            Console.WriteLine("Time taken for writing passed students file: {0}", stopwatch.Elapsed);
            stopwatch.Reset();


            Console.WriteLine("Sorting students by pass/fail.");
            stopwatch.Start();
            for (int i=0;i<students.Count;i++)
            {
                if(students[i].finalPoints >= 5.0)
                {
                    studentsPass.Add(students[i]);
                }
                else
                {
                    studentsFail.Add(students[i]);
                }

            }

            stopwatch.Stop();
            Console.WriteLine("Time taken for sorting: {0}", stopwatch.Elapsed);
            stopwatch.Reset();

            fileName = "students_pass" + students.Count + ".txt";

            Console.WriteLine("Making a text file of passed students.");
            stopwatch.Start();
            using (StreamWriter writetext = new StreamWriter(fileName))
            {
                writetext.WriteLine("Surname\t\tName\t\t\tFinalPoints (Avg.)  /  Final Points (Med.)");
                writetext.WriteLine("------------------------------------------------------------------------------");
                for (int i = 0; i < studentsPass.Count; i++)
                {
                    writetext.WriteLine("{0}\t\t{1}\t\t\t{2}\t\t\t{3}", studentsPass[i].fullName.surname, studentsPass[i].fullName.name, Math.Round(studentsPass[i].finalPoints, 1), studentsPass[i].median);
                }
            }
            stopwatch.Stop();
            Console.WriteLine("Time taken for writing passed students file: {0}", stopwatch.Elapsed);
            stopwatch.Reset();

            fileName = "students_fail" + students.Count + ".txt";

            Console.WriteLine("Making a text file of failed students.");
            stopwatch.Start();

            using (StreamWriter writetext = new StreamWriter(fileName))
            {
                writetext.WriteLine("Surname\t\tName\t\t\tFinalPoints (Avg.)  /  Final Points (Med.)");
                writetext.WriteLine("------------------------------------------------------------------------------");
                for (int i = 0; i < studentsFail.Count; i++)
                {
                    writetext.WriteLine("{0}\t\t{1}\t\t\t{2}\t\t\t{3}", studentsFail[i].fullName.surname, studentsFail[i].fullName.name, Math.Round(studentsFail[i].finalPoints, 1), studentsFail[i].median);
                }

            }
            stopwatch.Stop();
            Console.WriteLine("Time taken for writing passed students file: {0}", stopwatch.Elapsed + Environment.NewLine);
            stopwatch.Reset();
        }

        public static void CreateStudents(int count)
        {
            string name, surname;
            double homeworkResult;
            List<double> homeworkResults = new List<double>();
            List<Student> students = new List<Student>();
            double examResult;
            double finalPoints;
            double median;
            Random random = new Random();

            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine("Creating random students list.");
            stopwatch.Start();
            for (int i = 0; i < count; i++)
            {
                homeworkResults.Clear();
                name = "Name" + (i + 1);
                surname = "Surname" + (i + 1);

                for (int j = 0; j < 4; j++)
                {
                    homeworkResult = random.Next(0, 10);
                    homeworkResults.Add(homeworkResult);
                }
                examResult = random.Next(0, 10);
                finalPoints = GetFinalPoints(homeworkResults.Average(), examResult);
                median = GetMedian(homeworkResults, examResult);
                Student student = new Student(name, surname, homeworkResults, examResult, finalPoints, median);
                students.Add(student);
            }
            stopwatch.Stop();
            Console.WriteLine("Time taken for writing creating students list: {0}", stopwatch.Elapsed);
            WriteToFile(students);
        }
    }
}
