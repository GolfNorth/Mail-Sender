﻿namespace ConsoleHomeWork
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var menu = new Menu(args);
            menu.Add(new Task01());
            menu.Add(new Task02());
            menu.Print("Акчулпанов В.Г.\nУрок №5. Домашнее задание\nНаписать приложение, считающее в раздельных потоках:");
        }
    }
}
