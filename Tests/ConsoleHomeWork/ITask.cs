namespace ConsoleHomeWork
{
    /// <summary>
    /// Интерфейс задачи
    /// </summary>
    public interface ITask
    {
        string Title { get; set; }

        /// <summary>
        /// Метод запуска задачи
        /// </summary>
        /// <param name="args">Аргументы запуска</param>
        void Run(string[] args);
    }
}
