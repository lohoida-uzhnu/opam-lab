using System;

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
        public static string login = "aaa";
        public static string password = "11111";

        static service[] services = new service[5];
        static bool servicesAdded = false;

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

        public static void Main(string[] args)
        {
            AuthenticateUser();
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
                Console.WriteLine("Ви ввели не чяисло або воно не ціле. Спробуйте ще раз.");
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

                int choice = GetUserInputInt("Оберіть пункт меню (1-5): ");

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
                Console.WriteLine("1. Додати 5 послуг");
                Console.WriteLine("2. Показати всі послуги");
                Console.WriteLine("3. Назад");

                int choice = GetUserInputInt("Виберіть пункт:");

                switch (choice)
                {
                    case 1:
                        AddServices();
                        continue;
                    case 2:
                        ShowServices();
                        continue;
                    case 3:
                        Console.WriteLine("Повернення в головне меню.");
                        return;
                    default:
                        Console.WriteLine("Невірний вибір.");
                        continue;
                }
            }
        }

        private static void AddServices()
        {
            Console.WriteLine("\nВведення 5 послуг:");

            for (int i = 0; i < 5; i++)
            {
                Console.WriteLine($"\nПослуга №{i + 1}");

                Console.Write("Назва: ");
                string name = Console.ReadLine();

                double price = GetUserInput("Ціна: ");
                double duration = GetUserInput("Тривалість (хв): ");
                int quantity = GetUserInputInt("Кількість доступних: ");

                services[i] = new service(name, price, duration, quantity);
            }

            servicesAdded = true;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\nПослуги успішно додано!");
            Console.WriteLine("Натисніть будь-яку клавішу щоб вийти в меню послуг.");
            Console.ReadKey();
            Console.ResetColor();
        }

        private static void ShowServices()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n=== СПИСОК ПОСЛУГ ===");

            if (!servicesAdded)
            {
                Console.WriteLine("Послуги ще не додані!");
                return;
            }

            Console.WriteLine("\n--- Список послуг ---");
            for (int i = 0; i < 5; i++)
            {
                var s = services[i];
                Console.WriteLine($"{i + 1}. {s.Name}, {s.Price} грн, {s.Duration} хв, {s.Quantity} шт.");
            }
            
            Console.ResetColor();
            Console.WriteLine("Натисніть будь-яку клавішу щоб вийти в меню послуг.");
            Console.ReadKey();
        }

        private static void ShowAppointmentMenu()
        { 
            if (!servicesAdded)
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

            for (int i = 0; i < 5; i++)
            {
                var s = services[i];
                Console.WriteLine($"\n[{i + 1}] {s.Name} — {s.Price} грн / {s.Duration} хв / {s.Quantity} доступно");

                int count = GetUserInputInt("Скільки хочете записів: ");

                if (count > s.Quantity)
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("❗ Недостатньо місць, записуємо максимум можливе.");
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
            Console.WriteLine("Дякуємо, що вибрали нас!(натисніть будь-яку клавішу щоб вийти в головне меню)");
            Console.ResetColor();
            Console.ReadKey();
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
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nФункція ще в розробці");
                        break;

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

        private static void Statistic()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n=== СТАТИСТИКА ПОСЛУГ ===");

            if (!servicesAdded)
            {
                Console.ForegroundColor  = ConsoleColor.Red;
                Console.WriteLine("\nПослуги ще не додані. Немає даних для статистики.");
                Console.ResetColor();
                Console.WriteLine("\nНатисніть будь-яку клавішу щоб вийти в головне меню.");
                Console.ReadKey();
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

                if (s.Price > 500) countPriceOver500++;
            }

            double averagePrice = totalPrice / services.Length;
            double averageQuantity = totalQuantity / services.Length;

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n=== СТАТИСТИКА ===");
            Console.WriteLine($"Загальна сума цін: {totalPrice} грн");
            Console.WriteLine($"Середня ціна: {averagePrice} грн");
            Console.WriteLine($"Середня кількість: {averageQuantity}");
            Console.WriteLine($"Кількість послуг з ціною > 500 грн: {countPriceOver500}");
            Console.WriteLine($"Мінімальна ціна: {minPrice} грн");
            Console.WriteLine($"Максимальна ціна: {maxPrice} грн");

            Console.ResetColor();
            Console.WriteLine("\nНатисніть будь-яку клавішу щоб вийти в головне меню.");
            Console.ReadKey();
        }

    }
}
