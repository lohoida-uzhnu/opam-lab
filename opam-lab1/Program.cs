using System;

namespace opam_lab1;

class Program
{   
    public static void Main(string[] args)
    {
        RenderIntro();
        ShowMainMenu();
    }

    public static void RenderIntro()
    {
        Console.ForegroundColor = ConsoleColor.DarkCyan;
        Console.WriteLine("=========================================");
        Console.WriteLine("=         Медичний центр Сім'я          =");
        Console.WriteLine("=        Заснований в 1969 році         ="); 
        Console.WriteLine("=========================================");
        Console.ResetColor();
    }

    public static double GetUserInput(string prompt = "Введіть число: ")
    {
        Console.ForegroundColor = ConsoleColor.DarkGreen;
        Console.Write(prompt + " ");
        
        bool isNumber = double.TryParse(Console.ReadLine(), out double choice);
        if (!isNumber)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Ви ввели не число.");
            Console.ResetColor();
            GetUserInput();
        }
        Console.ResetColor();
        return choice;
    }

    public static void ShowMainMenu()
    {
        Console.ForegroundColor = ConsoleColor.White;
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

    Random random = new Random();
    double discount = Math.Round(random.NextDouble() * 15, 2);
    double discountAmount = totalPrice * (discount / 100);
    double finalPrice = totalPrice - discountAmount;

    double finalPricePow = Math.Pow(finalPrice, 2);

    Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n=== Загальна вартість прийомів ===: {totalPrice} грн.");
        Console.WriteLine($"Ваша знижка: {discount}% що становить {discountAmount} грн."); 
        Console.WriteLine($"До сплати після знижки: {finalPrice} грн.");
        Console.WriteLine($"Фінальна ціна в квадраті: {finalPricePow} грн.");
        Console.ResetColor();
        Console.ForegroundColor = ConsoleColor.Yellow;
        Console.WriteLine("\nДякуємо, що обрали Медичний центр Сім'я! Бажаємо Вам міцного здоров'я!");

        // creat by kudaihashish
        // github.com/lohoida-uzhnu
    }
}