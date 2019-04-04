using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GradesCalculator
{
    public class Student
    {
        public string name;
        public string surname;
        List<double> homeworkResults = new List<double>();
        double examResult;
        public double finalPoints;
        public double median;

        public Student(string name, string surname, List<double> homeworkResults, double examResult, double finalPoints, double median)
        {
            this.name = name;
            this.surname = surname;
            this.homeworkResults = homeworkResults;
            this.examResult = examResult;
            this.finalPoints = finalPoints;
            this.median = median;
        }
    }
}
