using System;

namespace Lab3
{
    public class Task1
    {
        static void Main(string[] args)
        {
            PrintResults(5, 35);
            PrintResults(0, 15);
            PrintResults(1, 1);
            PrintResults(6, 1);
            PrintResults(4, 14);
            PrintResults(-1, 10);
            PrintResults(8, -15);
            PrintResults(9, 40);
            PrintResults(3, 0);
            PrintResults(Double.NaN, 5);
            PrintResults(6, Double.NaN);
            PrintResults(7, Double.MinValue);
            PrintResults(8, Double.MaxValue);
            PrintResults(Double.MinValue, 9);
            PrintResults(Double.MaxValue, 10);
        }

        public static double sum_1(double s, double k)
        {
            double sum = 0;

            if (s <= 0)
            {
                Console.WriteLine("\nThe value of s is out of range!");
            }

            else if (double.IsNaN(s) || double.IsNaN(k))
            {
                Console.WriteLine("\n");
                throw new ArgumentOutOfRangeException();
            }

            else if (s == double.MaxValue || k == double.MinValue)
            {
                Console.WriteLine("\n");
                throw new ArgumentOutOfRangeException();
            }

            else if (s == double.MinValue || k == double.MaxValue)
            {
                Console.WriteLine("\n");
                throw new ArgumentOutOfRangeException();
            }

            else if (k < 1 || k > 35)
            {
                Console.WriteLine("\nThe value of k is out of range!");
            }

            else
            {
                for (int i = 1; i <= k; i++)
                {
                    sum += Math.Log10(Math.Sqrt(s / Math.Pow(i, 2)));
                }
            }

            return sum;

        }

        static void PrintResults(double s, double k)
        {
            Console.Write($"s:{s}, k:{k}, result: ");
            try
            {
                Console.WriteLine(sum_1(s, k));
                Console.WriteLine("\n");
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("EXCEPTION! {0}", e.Message);
            }
        }
    }

    public class Task2
    {
        static void Main(string[] args)
        {
            PrintResults(5, 6, 8);
            PrintResults(5, 6, 7);
            PrintResults(-1, 6, 8);
            PrintResults(0, 6, 8);
            PrintResults(Double.NaN, 6, 8);
            PrintResults(3, Double.NaN, 8);
            PrintResults(3, 4, Double.NaN);
            PrintResults(Double.MaxValue, 6, 8);
            PrintResults(3, Double.MaxValue, 8);
            PrintResults(3, 4, Double.MaxValue);
            PrintResults(Double.MinValue, 6, 8);
            PrintResults(3, Double.MinValue, 8);
            PrintResults(3, 4, Double.MinValue);
        }
        public static double sum_2(double t, double n, double l)
        {
            double sum = 0;
            if (t <= 0)
            {
                Console.WriteLine("\n");
                throw new ArgumentOutOfRangeException();
            }

            else if (Double.IsNaN(t) || Double.IsNaN(n) ||  Double.IsNaN(l))
            {
                Console.WriteLine("\n");
                throw new ArgumentOutOfRangeException();
            }

            else if (t == double.MinValue || n == double.MinValue || l == double.MinValue)
            {
                Console.WriteLine("\n");
                throw new ArgumentOutOfRangeException();
            }

            else if (t == double.MaxValue || n == double.MaxValue || l == double.MaxValue)
            {
                Console.WriteLine("\n");
                throw new ArgumentOutOfRangeException();
            }

            else
            {
                if (l % 2 != 0)
                {
                    for (int i = 1; i <= n; i++)
                    {
                        sum += l / t;
                    }
                }

                else
                {
                    for (int i = 1; i <= n; i++)
                    {
                        sum += l / Math.Sqrt(t);
                    }
                }
            }

            return sum;
        }

        static void PrintResults(double t, double n, double l)
        {
            Console.Write($"t:{t}, n:{n}, l:{l}, result: ");
            try
            {
                Console.WriteLine(sum_2(t, n, l));
                Console.WriteLine("\n");
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("EXCEPTION! {0}", e.Message);
            }
        }
    }

    public class Task3
    {
        static void Main(string[] args)
        {
            PrintResults(0.1);
            PrintResults(0.5);
            PrintResults(0.9);
            PrintResults(1);
            PrintResults(0.00001);
            PrintResults(2);
            PrintResults(-1);
            PrintResults(Double.NaN);
            PrintResults(Double.MinValue);
            PrintResults(Double.MaxValue);

        }

        public static double sum_3(double precision)
        {
            double sum = 0;
            if (Double.IsNaN(precision) || precision == Double.MaxValue || precision == Double.MinValue)
            {
                Console.WriteLine("\n");
                throw new ArgumentOutOfRangeException();
            }

            else if (precision > 1 || precision <= 0) 
            {
                Console.WriteLine("\n");
                throw new ArgumentOutOfRangeException();
            }


            else
            {
                int i = 1;
                double term = Double.MaxValue;
                {
                    while(Math.Abs(term) > precision)
                    {
                        term = 1 / (Math.Pow(i, 2));
                        sum += term;
                        i++;
                    }
                }
            }

            return sum;
        }
        static void PrintResults(double precision)
        {
            Console.Write($"Precision:{precision} \nResult: ");
            try
            {
                Console.WriteLine(sum_3(precision));
                Console.WriteLine("\n");
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine("EXCEPTION! {0}", e.Message);
            }
        }
    }
}
