using System;
using System.Collections.Generic;

namespace ConsoleHomeWork
{
    /// <summary>
    ///     Класс реализации меню приложения
    /// </summary>
    public class Menu
    {
        private string[] _args;
        private List<ITask> _menuItems;

        public Menu(string[] args)
        {
            _args = args;
            _menuItems = new List<ITask>();
        }

        /// <summary>
        ///     Выводит меню в консоль
        /// </summary>
        /// <param name="header">Сообщение для заголовка</param>
        public void Print(string header)
        {
            if (_menuItems.Count == 0) return;

            while (true)
            {
                Console.WriteLine(header);

                for (var i = 0; i < _menuItems.Count; i++)
                {
                    Console.WriteLine($"{i + 1}. {_menuItems[i].Title}");
                }

                Console.WriteLine("0. Выход");

                var choice = Common.ReadInt("Введите номер задания: ", 0, _menuItems.Count);

                if (choice == 0)
                    break;

                Console.Clear();
                Console.WriteLine($"Задание {choice:D2}. {_menuItems[choice - 1].Title}");

                _menuItems[choice - 1].Run(_args);

                Console.WriteLine();
                Console.WriteLine("Для продолжения нажмите любую клавишу...");
                Console.ReadKey(true);
                Console.Clear();
            }
        }

        /// <summary>
        ///     Добавляет элемент в меню
        /// </summary>
        /// <param name="menuItem">Объект типа ITask</param>
        public void Add(ITask menuItem)
        {
            _menuItems.Add(menuItem);
        }
    }
}
