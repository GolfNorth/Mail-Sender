using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

/*
 * В некой директории лежат файлы. По структуре они содержат 3 числа, разделенные пробелами. Первое число — целое, обозначает действие, 1 — умножение и 2 — деление, остальные два — числа с плавающей точкой. Написать многопоточное приложение, выполняющее вышеуказанные действия над числами и сохраняющее результат в файл result.dat. Количество файлов в директории заведомо много.
 */

namespace ConsoleHomeWork.Lesson_6
{
    public class Task02 : ITask
    {
        private readonly AutoResetEvent _waitHandler = new AutoResetEvent(true);

        public string Title { get; set; } = "Написать многопоточное приложение, выполняющее вышеуказанные действия над числами и сохраняющее результат в файл result.dat.";

        public void Run(string[] args)
        {
            /*
            var random = new Random();

            for (var i = 0; i < 100; i++)
            {
                using var sw = new StreamWriter($"data_{i}.dat");
                sw.WriteLine($"{random.Next(1,3)} {random.Next(1, 11)} {random.Next(1, 11)}");
                sw.Close();
            }
            */

            const string path = "Data";
            const string resultFile = "result.dat";
            var files = Directory.GetFiles(path, "*.dat");

            if (File.Exists(resultFile))
                File.Delete(resultFile);

            using var sw = new StreamWriter(resultFile, true);
            Parallel.ForEach(files, (file) => CalcResult (file, sw));
            sw.Close();
        }

        /// <summary>
        ///     Открывает файл, вычисляет результат и записывает в файл result.dat
        /// </summary>
        /// <param name="file">Файл с данными</param>
        /// <param name="sw">Экземпляр потока записи</param>
        private async void CalcResult(string file, StreamWriter sw)
        {
            using var sr = new StreamReader(file);
            var operators = sr.ReadLine()?.Split();

            sr.Close();

            if (operators?.Length != 3)
                throw new FileLoadException();

            var result = int.Parse(operators[0]) == 1
                ? int.Parse(operators[1]) * int.Parse(operators[2])
                : float.Parse(operators[1]) / float.Parse(operators[2]);
            var resultString = $"{operators[1]} {(operators[0] == "1" ? "*" : "/")} {operators[2]} = {result:F2}";

            _waitHandler.WaitOne();

            await sw.WriteLineAsync(resultString);

            _waitHandler.Set();

            Console.WriteLine($"Файл {file} обработан");
        }
    }
}
