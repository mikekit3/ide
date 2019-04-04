using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradesCalculatorV2
{
    class Program
    {
        static void Main(string[] args)
        {
            string name;
            string surname;
            double homeworkResult;
            List<double> homeworkResults = new List<double>();
            double examResult;
            double finalPoints;
            double median;

            char studentChoice = 'y';
            char homeworkChoice = 'y';

            bool generateRandomPoints = false;
            bool readFromFile = true;
            List<Student> students = new List<Student>();

            if (readFromFile)
            {
                string[] lines;
                var list = new List<string>();
                var fileStream = new FileStream(@"../../students.txt", FileMode.Open, FileAccess.Read);
                using (var streamReader = new StreamReader(fileStream, Encoding.UTF8))
                {
                    string line;
                    while ((line = streamReader.ReadLine()) != null)
                    {
                        list.Add(line);
                    }
                }
                lines = list.ToArray();

                for (int i=1;i < lines.Length;i++)
                {
                    string line = System.Text.RegularExpressions.Regex.Replace(lines[i], @"\s+", " ");
                    string[] words = line.Split(' ');
                    surname = words[0];
                    name = words[1];

                    homeworkResult = double.Parse(words[2]);
                    homeworkResults.Add(homeworkResult);

                    homeworkResult = double.Parse(words[3]);
                    homeworkResults.Add(homeworkResult);

                    homeworkResult = double.Parse(words[4]);
                    homeworkResults.Add(homeworkResult);

                    homeworkResult = double.Parse(words[5]);
                    homeworkResults.Add(homeworkResult);

                    homeworkResult = double.Parse(words[6]);
                    homeworkResults.Add(homeworkResult);

                    examResult = double.Parse(words[7]);

                    finalPoints = GetFinalPoints(homeworkResults.Average(), examResult);
                    median = GetMedian(homeworkResults, examResult);
                    Student student = new Student(name, surname, homeworkResults, examResult, finalPoints, median);
                    students.Add(student);
                }
            }
            else
            {
                while (studentChoice == 'y')
                {
                    studentChoice = 'y';
                    homeworkChoice = 'y';
                    Console.Clear();
                    Console.WriteLine("Enter your name: ");
                    name = Console.ReadLine();
                    Console.WriteLine("Enter your surname: ");
                    surname = Console.ReadLine();

                    while (homeworkChoice == 'y')
                    {
                        if (generateRandomPoints)
                        {
                            Random random = new Random();
                            homeworkResult = random.Next(0, 100);
                            homeworkResults.Add(homeworkResult);
                        }
                        else
                        {
                            Console.WriteLine("Enter your homework result: ");
                            homeworkResult = Double.Parse((Console.ReadLine()));
                            homeworkResults.Add(homeworkResult);
                        }
                        Console.WriteLine("Add another homework result (y/n): ");
                        homeworkChoice = Char.Parse(Console.ReadLine());
                    }
                    if (generateRandomPoints)
                    {
                        Random random = new Random();
                        examResult = random.Next(0, 100);
                    }
                    else
                    {
                        Console.WriteLine("Enter your exam result: ");
                        examResult = Double.Parse((Console.ReadLine()));
                    }
                    finalPoints = GetFinalPoints(homeworkResults.Average(), examResult);
                    median = GetMedian(homeworkResults, examResult);
                    Student student = new Student(name, surname, homeworkResults, examResult, finalPoints, median);
                    students.Add(student);

                    Console.WriteLine("Add another student (y/n): ");
                    studentChoice = Char.Parse(Console.ReadLine());
                }
            }

            PrintResults(students);
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
                    (sortedNumbers.ElementAt(halfIndex-1))) / 2);
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
            students = students.OrderBy(s => s.surname).ToList(); // sorting list by surname
            Console.WriteLine("Surname\t\tName\t\t\tFinalPoints (Avg.)  /  Final Points (Med.)");
            Console.WriteLine("------------------------------------------------------------------------------");
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine("{0}\t\t{1}\t\t\t{2}\t\t\t{3}", students[i].surname, students[i].name, Math.Round(students[i].finalPoints, 1), students[i].median);
            }
        }
    }
}
