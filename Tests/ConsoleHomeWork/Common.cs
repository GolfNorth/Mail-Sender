using System;

namespace ConsoleHomeWork
{
    public static class Common
    {
        /// <summary>
        ///     Считывает следующую строку символов из стандартного входного потока. Преобразует в тип System.Int32
        /// </summary>
        /// <param name="message">Сообщение для выввода на экран</param>
        /// <param name="min"></param>
        /// <param name="max"></param>
        /// <returns></returns>
        public static int ReadInt(string message, int min, int max)
        {
            do
            {
                Console.Write(message);

                if (!int.TryParse(Console.ReadLine(), out var number)) continue;

                if (number >= min && number <= max)
                    return number;
            } while (true);
        }
    }
}
