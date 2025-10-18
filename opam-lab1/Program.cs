using System;

namespace opam_lab1;

class Program
{   
    static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=== Вітаємо до Медичниго центру Сім'я ===");
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

        Console.WriteLine("\nКількість прийомів до окуліста: ");
        int countOphthalmologist = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Кількість прийомів до уролога: ");
        int countUrologist = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Кількість прийомів до невролога: "); 
        int countNeurologist = Convert.ToInt32(Console.ReadLine());
        Console.WriteLine("Кількість прийомів до нарколога: ");
        int countNarcologist = Convert.ToInt32(Console.ReadLine());

        double totalOphthalmologist = priceOphthalmologist * countOphthalmologist;
        double totalUrologist = priceUrologist * countUrologist;
        double totalNeurologist = priceNeurologist * countNeurologist;
        double totalNarcologist = priceNarcologist * countNarcologist;

        double totalPrice = totalOphthalmologist + totalUrologist + totalNeurologist + totalNarcologist;



    }
}