using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Text;

namespace opam_lab1
{
    class Program
    {
        public static List<Service> Services = new List<Service> { };
        public static bool verify = false;
        public static string currentDir = Directory.GetCurrentDirectory();
        public static string[] services;

        public static void EnsureFileExists(string path, string header)
        {
            if (!File.Exists(path))
            {
                using (var create = File.Create(path)) { }
                File.WriteAllText(path, header + Environment.NewLine);
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"Файл {Path.GetFileName(path)} створено з шапкою.");
                Console.ResetColor();
            }
        }

        public static void Main()
        {
            Console.OutputEncoding = Encoding.UTF8;

            EnsureFileExists(currentDir + "/services.csv", "Id,Name,Price,Duration,Quantity");
            EnsureFileExists(currentDir + "/users.csv", "Id,Login,Password");

            services = File.ReadAllLines(currentDir + "/services.csv");

            if (services.Length == 0 || services[0].Trim() != "Id,Name,Price,Duration,Quantity")
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Некоректна шапка CSV файлу!");
                Console.ResetColor();
            }

            for (int i = 1; i < services.Length; i++)
            {
                string line = services[i];
                if (string.IsNullOrWhiteSpace(line))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Пустий рядок №{i} csv файлу");
                    Console.ResetColor();
                    continue;
                }

                string[] parts = line.Split(',');
                if (parts.Length < 5)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Некоректний рядок №{i} — недостатньо полів.");
                    Console.ResetColor();
                    continue;
                }

                if (!int.TryParse(parts[0], out int id))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Не вдалося прочитати Id в рядку №{i}.");
                    Console.ResetColor();
                    continue;
                }

                string name = parts[1];
                if (!double.TryParse(parts[2], NumberStyles.Any, CultureInfo.InvariantCulture, out double price))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Не вдалося прочитати Price в рядку №{i}.");
                    Console.ResetColor();
                    continue;
                }

                if (!double.TryParse(parts[3], NumberStyles.Any, CultureInfo.InvariantCulture, out double duration))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Не вдалося прочитати Duration в рядку №{i}.");
                    Console.ResetColor();
                    continue;
                }

                if (!int.TryParse(parts[4], out int quantity))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine($"Не вдалося прочитати Quantity в рядку №{i}.");
                    Console.ResetColor();
                    continue;
                }

                Services.Add(new Service(id, name, price, duration, quantity));
            }

            AuthenticateUser();
            if (!verify)
            {
                return;
            }

            Console.WriteLine("Підтверджено вхід в систему, натисніть будь-яку клавішу щоб продовжити:");
            Console.ResetColor();
            Console.ReadKey();
            MainMenu();
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.Write("Дякую за користування нашим сервісом! Натисніть будь-яку клавішу щоб вийти:");
            Console.ReadKey();
        }

        public static void AuthenticateUser()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Створіть або увійдіть в акаунт:");
            Console.WriteLine("1. Увійти в акаунт");
            Console.WriteLine("2. Створити акаунт");

            int choice = GetUserInputInt("Оберіть пункт: ");
            switch (choice)
            {
                case 1:
                    Login();
                    break;
                case 2:
                    Registration();
                    break;
            }
        }

        public static void Login()
        {
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            string[] lines = File.ReadAllLines(currentDir + "/users.csv");
            for (int i = 3; i > 0; i--)
            {
                Console.Write("Введіть логін: ");
                string inputlogin = Console.ReadLine();
                Console.Write("Введіть пароль: ");
                string inputpassword = Console.ReadLine();
                for (int j = 1; j < lines.Length; j++)
                {
                    string[] parts = lines[j].Split(',');
                    if (parts.Length < 3)
                        continue;
                    string login = parts[1];
                    string password = parts[2];
                    if (inputlogin == login && inputpassword == password)
                    {
                        verify = true;
                        break;
                    }
                }
                if (verify)
                {
                    break;
                }
                Console.WriteLine($"Логін або пароль введено не правильно (залишилося {i - 1} спроб)");
            }
        }

        public static void Registration()
        {
            string[] lines = File.ReadAllLines(currentDir + "/users.csv");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            string login;
            string password;
            while (true)
            {
                Console.WriteLine("Введіть логін для нового акаунтy:");
                login = Console.ReadLine();
                login = login?.Replace(" ", "") ?? "";
                bool exists = false;
                for (int i = 1; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split(',');
                    if (parts.Length >= 2 && parts[1] == login)
                    {
                        Console.WriteLine("Такий логін вже існує!");
                        exists = true;
                        break;
                    }
                }
                if (exists)
                    continue;

                if (login.Length <= 3)
                {
                    Console.WriteLine("Логін повинен бути мінімум 4 символа довжиною");
                    continue;
                }
                if (login.Contains(",") || login.Contains(";"))
                {
                    Console.WriteLine("Логін не може містити коми або крапки з комою");
                    continue;
                }
                break;
            }

            while (true)
            {
                Console.WriteLine("Введіть пароль для нового акаунтy:");
                password = Console.ReadLine();
                if (password != null && (password.Contains(",") || password.Contains(";")))
                {
                    Console.WriteLine("Пароль не може містити коми або крапки з комою");
                    continue;
                }

                if (password == null || password.Length <= 3)
                {
                    Console.WriteLine("Пароль повинен бути мінімум 4 символи довжиною");
                    continue;
                }
                break;
            }

            int id = IdGenerator.GenerateNewId(currentDir + "/users.csv");
            File.AppendAllText(currentDir + "/users.csv", "\n" + Convert.ToString(id) + "," + login + "," + password);
            verify = true;
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

        public static void PressAnyKey()
        {
            Console.WriteLine("Натисніть будь-яку клавішу, щоб продовжити...");
            Console.ReadKey();
        }

        public static void MainMenu()
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

        public static void AddServices()
        {
            string name;
            while (true)
            {
                Console.Write("Введіть назву послуги: ");
                name = Console.ReadLine() ?? "";
                if (name.Contains(",") || name.Contains(";"))
                {
                    Console.WriteLine("Помилка! Символи коми та крапки з комою заборонені");
                    continue;
                }
                if (name.Length == 0)
                {
                    Console.WriteLine("Помилка! Нічого не введено");
                    continue;
                }
                break;
            }

            double price;
            while (true)
            {
                Console.Write("Введіть ціну: ");
                if (double.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out price) && price >= 0)
                    break;
                Console.WriteLine("Помилка! Введіть коректне число для ціни");
            }

            double duration;
            while (true)
            {
                Console.Write("Введіть тривалість(хв): ");
                if (double.TryParse(Console.ReadLine(), NumberStyles.Any, CultureInfo.InvariantCulture, out duration) && duration >= 0)
                    break;
                Console.WriteLine("Помилка! Введіть коректне число для тривалості");
            }

            int quantity;
            while (true)
            {
                Console.Write("Введіть кількість вільних: ");
                if (int.TryParse(Console.ReadLine(), out quantity) && quantity >= 0)
                    break;
                Console.WriteLine("Помилка! Введіть коректне ціле число кількості");
            }

            int id = IdGenerator.GenerateNewId(currentDir + "/services.csv");
            File.AppendAllText(currentDir + "/services.csv",
                "\n" + id + "," + name + "," + price.ToString(CultureInfo.InvariantCulture) + "," + duration.ToString(CultureInfo.InvariantCulture) + "," + quantity);

            Services.Add(new Service(id, name, price, duration, quantity));
            Console.WriteLine($"Послуга '{name}' успішно додано!");
            PressAnyKey();
            MainMenu();
        }

        private static void ShowServices()
        {
            if (Services.Count == 0)
            {
                Console.WriteLine("Послуг ще немає!");
                PressAnyKey();
                return;
            }

            Console.WriteLine("\n-----------------------------------------------");
            Console.WriteLine($"| {"№",-3} | {"Назва",-20} | {"Ціна",-10} | {"Хв",-5} | {"К-сть",-5} |");
            Console.WriteLine("-----------------------------------------------");

            for (int i = 0; i < Services.Count; i++)
            {
                var s = Services[i];
                Console.WriteLine($"| {i + 1,-3} | {s.Name,-20} | {s.Price,-10} | {s.Duration,-5} | {s.Quantity,-5} |");
            }

            Console.WriteLine("-----------------------------------------------");
            PressAnyKey();
        }

        public static void SearchMenu()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("Введіть назву послуги для пошуку: ");
            string query = (Console.ReadLine() ?? "").ToLower();
            bool found = false;

            for (int i = 0; i < Services.Count; i++)
            {
                var s = Services[i];
                if (s.Name.ToLower().Contains(query))
                {
                    found = true;
                    Console.WriteLine($"{s.Id}. {s.Name} | {s.Price} грн | {s.Duration} хв | {s.Quantity} шт");
                }
            }

            if (!found)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Послугу не знайдено!");
            }

            Console.ResetColor();
            PressAnyKey();
            return;
        }

        public static void DeleteService()
        {
            ShowServices();
            int id;
            while (true)
            {
                Console.Write("Введіть ID послуги для видалення: ");
                if (int.TryParse(Console.ReadLine(), out id))
                    break;
                Console.WriteLine("Помилка! Введено не ID");
            }

            if (id >= 1 && id <= Services.Count)
            {
                Console.WriteLine($"Послугу '{Services[id - 1].Name}' видалено!");
                Services.RemoveAt(id - 1);
            }

            List<string> lines = new List<string>();
            lines.Add("Id,Name,Price,Duration,Quantity");

            for (int i = 0; i < Services.Count; i++)
            {
                lines.Add($"{i + 1},{Services[i].Name},{Services[i].Price.ToString(CultureInfo.InvariantCulture)},{Services[i].Duration.ToString(CultureInfo.InvariantCulture)},{Services[i].Quantity}");
            }

            File.WriteAllLines(currentDir + "/services.csv", lines);
            PressAnyKey();
            return;
        }

        public static void SortMenu()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            Console.WriteLine("=== МЕНЮ СОРТУВАННЯ ===");
            Console.WriteLine("1. За назвою");
            Console.WriteLine("2. За ціною");
            Console.WriteLine("3. Бульбашкове(за ціною)");
            Console.WriteLine("4. Назад");

            int choice = GetUserInputInt("Оберіть опцію (1-4): ");
            switch (choice)
            {
                case 1:
                    Services.Sort((p1, p2) => string.Compare(p1.Name, p2.Name, StringComparison.OrdinalIgnoreCase));
                    Console.WriteLine("Список відсортовано за назвою!");
                    break;
                case 2:
                    Services.Sort((a, b) => a.Price.CompareTo(b.Price));
                    Console.WriteLine("Список відсортовано за ціною!");
                    break;
                case 3:
                    BubbleSort();
                    Console.WriteLine("Список відсортовано бульбашкою за ціною!");
                    break;
                case 4:
                    return;
            }

            Console.WriteLine("Сортування завершено!");
            ShowServiceMenu();
            Console.ResetColor();
            PressAnyKey();
        }

        public static void BubbleSort()
        {
            int n = Services.Count;
            for (int i = 0; i < n - 1; i++)
            {
                for (int j = 0; j < n - i - 1; j++)
                {
                    if (Services[j].Price > Services[j + 1].Price)
                    {
                        Service s = Services[j];
                        Services[j] = Services[j + 1];
                        Services[j + 1] = s;
                    }
                }
            }
        }

        private static void Statistic()
        {
            if (Services.Count == 0)
            {
                Console.WriteLine("Послуги ще не додані!");
                PressAnyKey();
                return;
            }

            double totalPrice = 0;
            int totalQuantity = 0;

            double minPrice = Services[0].Price;
            double maxPrice = Services[0].Price;

            int countPriceOver500 = 0;

            foreach (var s in Services)
            {
                totalPrice += s.Price;
                totalQuantity += s.Quantity;

                if (s.Price < minPrice) minPrice = s.Price;
                if (s.Price > maxPrice) maxPrice = s.Price;

                if (s.Price > 500) countPriceOver500++;
            }

            double averagePrice = totalPrice / Services.Count;
            double averageQuantity = (double)totalQuantity / Services.Count;

            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine("\n=== СТАТИСТИКА ===");
            Console.WriteLine($"Кількість послуг: {Services.Count}");
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
            if (Services.Count == 0)
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

            for (int i = 0; i < Services.Count; i++)
            {
                var s = Services[i];
                Console.WriteLine($"\n[{i + 1}] {s.Name} — {s.Price} грн / {s.Duration} хв / {s.Quantity} доступно");

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

//create by zewws92qar
// Логойда Станіслав КН-23