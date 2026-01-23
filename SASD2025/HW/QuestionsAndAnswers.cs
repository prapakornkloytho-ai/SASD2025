using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Refactoring101
{
    // Do Refactoring the following code:
    public class QuestionsAndAnswers
    {
        // 1. Mysterious Name
        public double GetMax(double a, double b)
        {
            return Math.Max(a, b);
        }

        // 2. Duplicate Code
        public void Print()
        {
            PrintPerson("Mr.", "Harry Potter");
            PrintPerson("Ms.", "Mary Poppin");
            PrintPerson("Mr.", "Johny Black");
        }

        private void PrintPerson(string title, string name)
        {
            Console.WriteLine("***********************");
            Console.WriteLine($"   {title}{name}");
            Console.WriteLine("***********************");
            Console.WriteLine();
        }

        // 3. Shotgun Surgery
        public static class StudentInfo
        {
            public const int StudentCount = 48;
        }

        public class Shotgun1
        {
            public void DisplayStudents()
            {
                Console.WriteLine("Student Count = " + StudentInfo.StudentCount);
            }
        }

        public class Shotgun2
        {
            public void PrintTotal()
            {
                Console.WriteLine("Total Students : " + StudentInfo.StudentCount);
            }
        }

        // 4. Data Clump
        public void PrintDate(Date date)
        {
            Console.WriteLine(date.Format());
        }

        // 5. Feature Envy
        //     จากข้อที่แล้ว น่าจะได้สร้างคลาส Date ขึ้นมา
        //     ในคลาส Date นั้นให้สร้าง method: public string Format()
        //      ปรับให้ PrintDate(...) ของเดิม ไปเรียก date.Format() ดังกล่าว
    }

    public class Date
    {
        public int Day { get; }
        public int Month { get; }
        public int Year { get; }

        public Date(int day, int month, int year)
        {
            Day = day;
            Month = month;
            Year = year;
        }

        public string Format()
        {
            return $"{Day:00}/{Month:00}/{Year:0000}";
        }
    }
}
