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
            int result;

            do
            {
                Console.Write(message);
            } while (!int.TryParse(Console.ReadLine(), out result) || result <= min || result >= max);

            return result;
        }
    }
}
