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
        Console.WriteLine("===       Медичний центр Сім'я        ===");
        Console.WriteLine("===      Заснований в 1969 році       ==="); 
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
        Console.WriteLine();
        Console.ForegroundColor = ConsoleColor.White;
        Console.WriteLine("Головне меню: ");
        Console.WriteLine("1. Запис на прйиом");
        Console.WriteLine("2. Меню послуг");
        Console.WriteLine("3. Наші спеціалісти");
        Console.WriteLine("4. Працівник місяця");
        Console.WriteLine("5. Пошук");
        Console.WriteLine("6. Вихід");

        double choice = GetUserInput("Оберіть пункт меню (1-6): ");

        switch(choice)
        {
            case 1:
                ShowAppointmentMenu();
                break;
            case 2:
                ShowServiceMenu();
                break;
            case 3:
                ShowSpecialistMenu();
                break;
            case 4:
                ShowBestSpecialist();
                break;
            case 5:
                ShowSearchMenu();
                break;
                case 6:
                Console.WriteLine("Вихід з програми. До побачення!");
                return;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
                Console.ResetColor();
                ShowMainMenu();
                break;
        }
    }

    private static void ShowAppointmentMenu()
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
   
        ShowMainMenu();
    }

   private static void ShowServiceMenu()
    {
        Console.WriteLine("===== Меню наших послуг =====");
        Console.WriteLine("1. Додати нову послугу");
        Console.WriteLine("2. Переглянути всі послуги");
        Console.WriteLine("3. Редагувати послуги");
        Console.WriteLine("4. Видалити послугу");
        Console.WriteLine("5. Пошук послуги за назвою");
        Console.WriteLine("6. Сортувати за ціною / кількістю");
        Console.WriteLine("7. Повернутись у головне меню");
        Console.WriteLine("--------------------------------");

        double choice = GetUserInput("Оберіть дію (1-7): ");

        if (choice >= 0 && choice < 6)
        {
            Console.WriteLine("Функція в розробці... Повертаємо у головне меню");
            ShowMainMenu();
            return;
        }
        else if (choice == 7)
        {
            ShowMainMenu();
            return;
        }
    }

    private static void ShowSpecialistMenu()
    {
        Console.WriteLine("Функція в розробці... Повертаємо в головне меню.");
        ShowMainMenu();
        return;
    }

    private static void ShowBestSpecialist()
    {
        Console.WriteLine("Функція в розробці... Повертаємо в головне меню.");
        ShowMainMenu();
        return;
    }

    private static void ShowSearchMenu()
    {
        Console.WriteLine("Функція в розробці... Повертаємо в головне меню.");
        ShowMainMenu();
        return;
    }

    // creat by kudaihashish
    // github.com/lohoida-uzhnu
}
