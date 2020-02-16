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
        private const string ResultFile = "result.dat";
        private readonly AutoResetEvent _waitHandler = new AutoResetEvent(true);

        public string Title { get; set; } = "Написать многопоточное приложение, выполняющее вышеуказанные действия над числами и сохраняющее результат в файл result.dat.";

        public void Run(string[] args)
        {
            var files = Directory.GetFiles("Data", "*.dat");

            if (File.Exists(ResultFile))
                File.Delete(ResultFile);

            Parallel.ForEach(files, CalcResult);
        }

        private void CalcResult(string file)
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

            using var sw = new StreamWriter(ResultFile, true);
            sw.WriteLine(resultString);
            sw.Close();

            _waitHandler.Set();
        }
    }
}
