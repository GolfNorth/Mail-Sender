using System;
using System.Collections.Generic;
using System.Threading.Tasks;

/*
 * Даны 2 двумерных матрицы. Размерность 100х100 каждая. Напишите приложение, производящее параллельное умножение матриц. Матрицы заполняются случайными целыми числами от 0 до10.
 */

namespace ConsoleHomeWork.Lesson_6
{
    public class Task01 : ITask
    {
        private Random _random;
        private const int MatrixSize = 100;     // Размер матрицы
        private const int MatrixMaxValue = 100; // Максимальное значение элемента матрицы

        public string Title { get; set; } = "Напишите приложение, производящее параллельное умножение матриц.";

        public void Run(string[] args)
        {
            _random = new Random();

            var matrixGenerators = new List<Task<int[,]>>
            {
                Task.Run(() => GenerateMatrix(MatrixSize, MatrixSize, MatrixMaxValue)),
                Task.Run(() => GenerateMatrix(MatrixSize, MatrixSize, MatrixMaxValue))
            };


            var multiplyMatrices = Task.WhenAll(matrixGenerators)
                .ContinueWith(task => MultiplyMatrices(task.Result[0], task.Result[1]).Result);

            try
            {
                PrintMatrix(multiplyMatrices.Result);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine($"Ошибка выполнения перемножения матриц: {e.Message}");
            }
        }

        /// <summary>
        ///     Метод генерации матрицы заданного размера
        /// </summary>
        /// <param name="m">Количество строк</param>
        /// <param name="n">Количество столбцов</param>
        /// <param name="max">Максимальное значение членов матрицы</param>
        /// <returns></returns>
        private async Task<int[,]> GenerateMatrix(int m, int n, int max)
        {
            var matrix = new int[m, n];

            for (var k = 0; k < m; k++)
            {
                var i = k;

                Parallel.For(0, n, j =>
                {
                    matrix[i, j] = _random.Next(max);
                });
            }

            return matrix;
        }

        /// <summary>
        ///     Вывод в консоль матрицы
        /// </summary>
        /// <param name="matrix">Матрица</param>
        private void PrintMatrix(int[,] matrix)
        {
            for (var i = 0; i < matrix.GetLength(0); i++)
            {
                for (var j = 0; j < matrix.GetLength(1); j++)
                    Console.Write($"{matrix[i,j]} ");

                Console.WriteLine();
            }
        }

        /// <summary>
        ///     Производит перемножение двух матриц
        /// </summary>
        /// <param name="matrix1">Первая матрица</param>
        /// <param name="matrix2">Вторая матрица</param>
        /// <returns></returns>
        private async Task<int[,]> MultiplyMatrices(int[,] matrix1, int[,] matrix2)
        {
            if (matrix1.Length != matrix2.Length)
                throw new ArgumentException();

            var result = new int[matrix1.GetLength(0), matrix1.GetLength(1)];

            for (var k = 0; k < matrix1.GetLength(0); k++)
            {
                var i = k;

                Parallel.For(0, matrix1.GetLength(1), j =>
                {
                    result[i, j] = matrix1[i, j] * matrix2[i, j];
                });
            }

            return result;
        }
    }
}
