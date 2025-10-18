using System.Threading.Tasks.Sources;

namespace opam_lab1;

class Program
{   
    static void Main(string[] args)
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("=== Вітаємо до Медичниго центру Сім'я ===");
        Console.WriteLine("=== Меню послуг ===");
        Console.WriteLine("1. Запис на прийом до Окуліста");
        Console.WriteLine("2. Запис на прийом до Уролога");
        Console.WriteLine("3. Запис на прийом до Невролога");
        Console.WriteLine("4. Запис на прийом до Нарколога");
    }
}