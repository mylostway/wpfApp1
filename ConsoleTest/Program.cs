using BaseLib.Data;
using System;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            double d1 = 1.0;
            double d2 = 1.2;
            double d3 = 9.8;

            Console.WriteLine(string.Format("ceilNum1 = {0},ceilNum2 = {1},ceilNum3 = {2}", 
                DataHelper.Ceil(d1), DataHelper.Ceil(d2), DataHelper.Ceil(d3)));
        }
    }
}
