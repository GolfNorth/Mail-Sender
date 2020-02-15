using System;
using System.Threading;

/*
 * Написать приложение, считающее в раздельных потоках:
 * a. факториал числа N, которое вводится с клавиатуры;
 * b. сумму целых чисел до N.
 */

namespace ConsoleHomeWork.Lesson_5
{
    public class Task01 : ITask
    {
        private Random _random;
        private int _poolCounter;
        private static readonly object __SyncRoot = new object();

        public string Title { get; set; } = "Написать приложение, считающее в раздельных потоках факториал числа N и сумму чисел до N";

        public void Run(string[] args)
        {
            _random = new Random();
            _poolCounter = 0;

            //var max = int.MaxValue;
            var max = FindMaxValue();

            //for (var i = max; i > 0; i--)
            //{
            //    var number = i;

            //    poolCounter += 2;

            //    ThreadPool.QueueUserWorkItem(o => Factorial(number));
            //    ThreadPool.QueueUserWorkItem(o => CalcAmount(number));
            //}

            while (true)
            {
                int number;
                lock (__SyncRoot)
                    number = Common.ReadInt($"Введите число (от 0 до {max}): ", 0, max);

                if (number == 0) break;

                _poolCounter += 2;

                // number объявляется каждый раз внутри цикла, то нет смысла создавать копию
                ThreadPool.QueueUserWorkItem(o => Factorial(number));
                ThreadPool.QueueUserWorkItem(o => CalcAmount(number));
            }

            while (_poolCounter > 0)
                Thread.Sleep(10);
        }

        public void Factorial(int n)
        {
            long sum = n == 0 ? 0 : 1;

            for (var i = 1; i <= n; i++)
            {
                sum *= i;

                // Задержка для наглядности
                Thread.Sleep(_random.Next(100));
            }

            lock (__SyncRoot)
                Console.WriteLine($"Факториал числа {n} равен {sum}");

            _poolCounter--;
        }

        public void CalcAmount(int n)
        {
            var sum = 0;

            for (var i = 1; i <= n; i++)
            {
                sum += i;

                // Задержка для наглядности
                Thread.Sleep(_random.Next(100));
            }

            lock (__SyncRoot)
                Console.WriteLine($"Сумма чисел до {n} равна {sum}");

            _poolCounter--;
        }

        /// <summary>
        /// Максимальное число для факториала на машине
        /// </summary>
        /// <returns></returns>
        public int FindMaxValue()
        {
            var res = 2;
            long fact = 2;

            while (true)
            {
                if (fact < 0) break;

                res++;
                fact *= res;
            }

            return res - 1;
        }
    }
}
