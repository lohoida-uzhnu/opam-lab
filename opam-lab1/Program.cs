using System;

namespace opam_lab1;

class Program
{   
    static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("\n=== Вітаємо до Медичниго центру Сім'я ===");
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.Magenta;
        Console.WriteLine("\n=== Меню послуг ===");
        Console.WriteLine("1. Запис на прийом до Окуліста - 800грн.");
        Console.WriteLine("2. Запис на прийом до Уролога - 1000грн.");
        Console.WriteLine("3. Запис на прийом до Невролога - 900грн.");
        Console.WriteLine("4. Запис на прийом до Нарколога - 1200грн.");

        double priceOphthalmologist = 800;
        double priceUrologist = 1000;
        double priceNeurologist = 900;
        double priceNarcologist = 1200;
    }
}