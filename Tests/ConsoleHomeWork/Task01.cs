using System;
using System.Threading;

/*
 * Написать приложение, считающее в раздельных потоках:
 * a. факториал числа N, которое вводится с клавиатуры;
 */

namespace ConsoleHomeWork
{
    public class Task01 : ITask
    {
        private static readonly object __SyncRoot = new object();

        public string Title { get; set; } = "сумму целых чисел до N.";

        public void Run(string[] args)
        {
            while (true)
            {
                int number;
                lock (__SyncRoot)
                    number = Common.ReadInt("Введите целое положительное число: ", 0, int.MaxValue);

                if (number == 0) break;

                // number объявляется каждый раз внутри цикла, то нет смысла создавать копию
                ThreadPool.QueueUserWorkItem(o => CalcAmount(number));
            }
        }

        public void CalcAmount(int n)
        {
            var sum = 0;

            for (var i = 1; i <= n; i++)
            {
                sum += i;

                // Задержка для наглядности
                Thread.Sleep(10);
            }

            lock (__SyncRoot)
                Console.WriteLine($"Сумма положительных чисел до {n} равна {sum}");
        }
    }
}
