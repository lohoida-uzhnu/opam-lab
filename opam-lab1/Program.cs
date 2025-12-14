using System;
using System.Collections.Generic;
using System.IO;

namespace opam_lab1
{
    public struct service
    {
        public string Name;
        public double Price;
        public double Duration;
        public int Quantity;

        public service(string name, double price, double duration, int quantity)
        {
            Name = name;
            Price = price;
            Duration = duration;
            Quantity = quantity;
        }
    }

    class Program
    {
        public static void PressAnyKey()
        {
            Console.WriteLine("Натисніть будь-яку клавішу щоб продовжити...");
            Console.ReadKey();
        }

        public static string login = "aaa";
        public static string password = "11111";

        static List<service> services = new List<service>();

        static void Main(string[] args)
        {
            AuthenticateUser();
            RenderIntro();
            ShowMainMenu();
        }

        public static void AuthenticateUser()
        {
            int attempts = 3;
            string Login, Password;

            do
            {
                Console.WriteLine($"\nУ вас залишилось спроб: {attempts}");
                Console.Write("Логін: ");
                Login = Console.ReadLine();
                Console.Write("Пароль: ");
                Password = Console.ReadLine();

                if (Login == login && Password == password)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Авторизація успішна!\n");
                    Console.ResetColor();
                    return;
                }

                attempts--;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Невірні дані!");
                Console.ResetColor();

            } while (attempts > 0);

            Console.WriteLine("Спроби вичерпано. Програма завершена.");
            Environment.Exit(0);
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
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write(prompt + " ");
                string input = Console.ReadLine();

                if (double.TryParse(input, out double c))
                {
                    Console.ResetColor();
                    return c;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ви ввели не число. Спробуйте ще раз.");
            }
        }

        public static int GetUserInputInt(string prompt = "Введіть ціле число: ")
        {
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write(prompt + " ");
                string input = Console.ReadLine();

                if (int.TryParse(input, out int c))
                {
                    Console.ResetColor();
                    return c;
                }

                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ви ввели не число або воно не ціле. Спробуйте ще раз.");
            }
        }

        public static void ShowMainMenu()
        {
            while (true)
            {
                Console.WriteLine("\nГоловне меню:");
                Console.WriteLine("1. Запис на прийом");
                Console.WriteLine("2. Меню послуг");
                Console.WriteLine("3. Наші спеціалісти");
                Console.WriteLine("4. Статистика");
                Console.WriteLine("5. Вихід");

                int choice = GetUserInputInt("Оберіть пункт (1-5): ");

                switch (choice)
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
                        Statistic();
                        break;
                    case 5:
                        Console.WriteLine("До побачення!");
                        return;
                    default:
                        Console.WriteLine("Невірний вибір.");
                        break;
                }
            }
        }

        private static void ShowServiceMenu()
        {
            while (true)
            {
                Console.WriteLine("\n===== Меню послуг =====");
                Console.WriteLine("1. Додати послуги");
                Console.WriteLine("2. Показати всі послуги");
                Console.WriteLine("3. Пошук");
                Console.WriteLine("4. Видалення");
                Console.WriteLine("5. Сортування");
                Console.WriteLine("6. Статистика");
                Console.WriteLine("7. Назад");

                int choice = GetUserInputInt("Виберіть пункт:");

                switch (choice)
                {
                    case 1:
                        AddServices();
                        break;
                    case 2:
                        ShowServices();
                        break;
                    case 3:
                        SearchMenu();
                        break;
                    case 4:
                        DeleteService();
                        break;
                    case 5:
                        SortMenu();
                        break;
                    case 6:
                        Statistic();
                        break;
                    case 7:
                        return;
                    default:
                        Console.WriteLine("Невірний вибір.");
                        break;
                }
            }
        }

        private static void AddServices()
        {
            Console.WriteLine("\nВведення послуг (мінімум 5):");
            int count = GetUserInputInt("Скільки послуг хочете додати?: ");

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine($"\nПослуга №{i + 1}");
                Console.Write("Назва: ");
                string name = Console.ReadLine();

                double price = GetUserInput("Ціна: ");
                double duration = GetUserInput("Тривалість (хв): ");
                int quantity = GetUserInputInt("Кількість доступних: ");

                services.Add(new service(name, price, duration, quantity));
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nПослуги успішно додано!");
            Console.ResetColor();
            PressAnyKey();
        }

        private static void ShowServices()
        {
            if (services.Count == 0)
            {
                Console.WriteLine("Послуг ще немає!");
                PressAnyKey();
                return;
            }

            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine($"| {"№",-3} | {"Назва",-20} | {"Ціна",-10} | {"Хв",-5} | {"К-сть",-5} |");
            Console.WriteLine("-----------------------------------------------");

            for (int i = 0; i < services.Count; i++)
            {
                var s = services[i];
                Console.WriteLine($"| {i,-3} | {s.Name,-20} | {s.Price,-10} | {s.Duration,-5} | {s.Quantity,-5} |");
            }

            Console.WriteLine("-----------------------------------------------");
            PressAnyKey();
        }

        private static void SearchMenu()
        {
            Console.Write("\nЩо ви хочете знайти: ");
            string query = Console.ReadLine().ToLower();

            for (int i = 0; i < services.Count; i++)
            {
                if (services[i].Name.ToLower().Contains(query))
                {
                    var s = services[i];
                    Console.WriteLine($"\nЗнайдено: {s.Name}, {s.Price} грн, {s.Duration} хв, {s.Quantity} шт");
                    PressAnyKey();
                    return;
                }
            }

            Console.WriteLine("Нічого не знайдено");
            PressAnyKey();
        }

        public static void DeleteService()
        {
            if (services.Count == 0)
            {
                Console.WriteLine("Немає що видаляти!");
                PressAnyKey();
                return;
            }

            ShowServices();

            int index = GetUserInputInt("Введіть індекс для видалення: ");

            if (index < 0 || index >= services.Count)
            {
                Console.WriteLine("Такого індекса не існує!");
                PressAnyKey();
                return;
            }

            services.RemoveAt(index);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Послугу видалено!");
            Console.ResetColor();
            PressAnyKey();
        }

        private static void SortMenu()
        {
            Console.WriteLine("\n1 — Сортувати за ціною");
            Console.WriteLine("2 — Сортувати за назвою");
            Console.WriteLine("3 — Бульбашкове сортування (ціна)");
            Console.WriteLine("4 — Назад");

            int choice = GetUserInputInt("Оберіть пункт:");

            switch (choice)
            {
                case 1:
                    services.Sort((a, b) => a.Price.CompareTo(b.Price));
                    ShowServices();
                    break;
                case 2:
                    services.Sort((a, b) => a.Name.CompareTo(b.Name));
                    ShowServices();
                    break;
                case 3:
                    BubbleSort();
                    ShowServices();
                    break;
                case 4:
                    return;
            }

            Console.WriteLine("Сортування завершено!");
            PressAnyKey();
        }

        private static void BubbleSort()
        {
            for (int i = 0; i < services.Count - 1; i++)
            {
                for (int j = 0; j < services.Count - i - 1; j++)
                {
                    if (services[j].Price > services[j + 1].Price)
                    {
                        var temp = services[j];
                        services[j] = services[j + 1];
                        services[j + 1] = temp;
                    }
                }
            }
        }

        private static void Statistic()
        {
            if (services.Count == 0)
            {
                Console.WriteLine("Послуги ще не додані!");
                PressAnyKey();
                return;
            }

            double totalPrice = 0;
            int totalQuantity = 0;

            double minPrice = services[0].Price;
            double maxPrice = services[0].Price;
            
            int countPriceOver500 = 0;

            foreach (var s in services)
            {
                totalPrice += s.Price;
                totalQuantity += s.Quantity;

                if (s.Price < minPrice) minPrice = s.Price;
                if (s.Price > maxPrice) maxPrice = s.Price;

                if(s.Price > 500) countPriceOver500++;
            }

            double averagePrice = totalPrice / services.Count;
            double averageQuantity = totalQuantity / services.Count;

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n=== СТАТИСТИКА ===");
            Console.WriteLine($"Кількість послуг: {services.Count}");
            Console.WriteLine($"Загальна сума цін: {totalPrice} грн");
            Console.WriteLine($"Середня ціна: {averagePrice} грн");
            Console.WriteLine($"Середня кількість: {averageQuantity}");
            Console.WriteLine($"Кількість послуг з ціною > 500 грн: {countPriceOver500}");
            Console.WriteLine($"Мінімальна ціна: {minPrice} грн");
            Console.WriteLine($"Максимальна ціна: {maxPrice} грн");
            
            Console.ResetColor();
            PressAnyKey();
        }

        private static void ShowAppointmentMenu()
        {
            if (services.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Спершу додайте послуги в меню послуг!");
                Console.ResetColor();
                return;
            }

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n=== Запис на прийом ===");
            Console.WriteLine("Оберіть кількість прийомів для кожної послуги:");

            double total = 0;

            for (int i = 0; i < services.Count; i++)
            {
                var s = services[i];
                Console.WriteLine($"\n[{i}] {s.Name} — {s.Price} грн / {s.Duration} хв / {s.Quantity} доступно");

                int count = GetUserInputInt("Скільки хочете записів: ");

                if (count > s.Quantity)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Недостатньо місць, записуємо максимум можливе.");
                    count = s.Quantity;
                    Console.ResetColor();
                }

                total += s.Price * count;
            }

            Random random = new Random();
            double discount = Math.Round(random.NextDouble() * 15, 2);
            double discountAmount = total * discount / 100;
            double finalPrice = total - discountAmount;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\nЗагальна сума: {total} грн");
            Console.WriteLine($"Знижка: {discount}% ({discountAmount} грн)");
            Console.WriteLine($"До оплати: {finalPrice} грн");
            Console.WriteLine("Дякуємо, що вибрали нас!");
            Console.ResetColor();
            PressAnyKey();
        }

        private static void ShowSpecialistMenu()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;

            while (true)
            {
                Console.WriteLine("\n=== МЕНЮ СПЕЦІАЛІСТІВ ===");
                Console.WriteLine("1. Показати всіх спеціалістів");
                Console.WriteLine("2. Пошук спеціаліста за іменем");
                Console.WriteLine("3. Назад");

                int choice = GetUserInputInt("Оберіть пункт меню:");

                switch (choice)
                {
                    case 1:
                    case 2:
                        Console.WriteLine("\nФункція ще в розробці");
                        break;
                    case 3:
                        return;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Невірний вибір, спробуйте ще раз.");
                        break;
                }
                Console.ResetColor();
            }
        }
    }
}
