using System;

class Cake
{
    public string Form { get; set; }
    public string Size { get; set; }
    public string Flavor { get; set; }
    public string Glaze { get; set; }
    public string Decor { get; set; }
    public decimal Price { get; set; }
}

class Program
{
    static void Main()
    {
        List<Cake> orderList = new List<Cake>();
        decimal totalOrderPrice = 0;
        int selectedMenuItem = 0;

        Console.WriteLine("Выберите форму торта");
        Dictionary<string, decimal> formPrices = new Dictionary<string, decimal>
        {
            { "Круглая", 500M },
            { "Прямоугольная", 600M },
            { "Квадратная", 550M }
        };

        Dictionary<string, decimal> sizePrices = new Dictionary<string, decimal>
        {
            { "Маленький", 200M },
            { "Средний", 300M },
            { "Большой", 400M }
        };

        Dictionary<string, decimal> flavorPrices = new Dictionary<string, decimal>
        {
            { "Шоколад", 50M },
            { "Ваниль", 50M },
            { "Клубника", 70M },
            { "Морковный", 80M }
        };

        Dictionary<string, decimal> glazePrices = new Dictionary<string, decimal>
        {
            { "Шоколадная", 20M },
            { "Ванильная", 20M },
            { "Карамельная", 30M }
        };

        Dictionary<string, decimal> decorPrices = new Dictionary<string, decimal>
        {
            { "Цветы", 50M },
            { "Фрукты", 40M },
            { "Шоколадные стружки", 30M }
        };

        while (true)
        {
            Console.Clear();
            Console.WriteLine("Меню заказа тортов:");

            string[] menuItems = { "Собрать торт", "Посмотреть заказ", "Оформить заказ", "Выйти из программы (ESC)" };

            for (int i = 0; i < menuItems.Length; i++)
            {
                if (i == selectedMenuItem)
                {
                    Console.WriteLine("-> " + menuItems[i]);
                }
                else
                {
                    Console.WriteLine("   " + menuItems[i]);
                }
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.Escape)
            {
                break;
            }

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (selectedMenuItem > 0)
                    {
                        selectedMenuItem--;
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (selectedMenuItem < menuItems.Length - 1)
                    {
                        selectedMenuItem++;
                    }
                    break;

                case ConsoleKey.Enter:
                    switch (selectedMenuItem)
                    {
                        case 0:
                            orderList.Add(CreateCake(formPrices, sizePrices, flavorPrices, glazePrices, decorPrices));
                            break;

                        case 1:
                            DisplayOrder(orderList);
                            break;

                        case 2:
                            totalOrderPrice += CalculateTotalPrice(orderList);
                            SaveOrderToFile(orderList, totalOrderPrice);
                            orderList.Clear();
                            break;

                        case 3:
                            Environment.Exit(0);
                            break;
                    }
                    break;
            }
        }
    }

    static Cake CreateCake(Dictionary<string, decimal> formPrices,
    Dictionary<string, decimal> sizePrices,
    Dictionary<string, decimal> flavorPrices,
    Dictionary<string, decimal> glazePrices,
    Dictionary<string, decimal> decorPrices)
    {
        Cake cake = new Cake();
        cake.Form = SelectOptionFromMenu(formPrices, "форму");
        cake.Size = SelectOptionFromMenu(sizePrices, "размер");
        cake.Flavor = SelectOptionFromMenu(flavorPrices, "вкус");
        cake.Glaze = SelectOptionFromMenu(glazePrices, "глазурь");
        cake.Decor = SelectOptionFromMenu(decorPrices, "декор");
        cake.Price = CalculateTotalPriceForCake(cake, formPrices, sizePrices, flavorPrices, glazePrices, decorPrices);
        return cake;
    }


    static string SelectOptionFromMenu(Dictionary<string, decimal> optionsWithPrices, string category)
    {
        int selectedIndex = 0;

        while (true)
        {
            Console.Clear();

            Console.WriteLine($"Выберите {category}:");

            for (int i = 0; i < optionsWithPrices.Count; i++)
            {
                var option = optionsWithPrices.ElementAt(i);
                if (i == selectedIndex)
                {
                    Console.WriteLine("-> " + (i + 1) + ". " + option.Key + " - цена: " + option.Value + " руб.");
                }
                else
                {
                    Console.WriteLine("   " + (i + 1) + ". " + option.Key + " - цена: " + option.Value + " руб.");
                }
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey();

            switch (keyInfo.Key)
            {
                case ConsoleKey.UpArrow:
                    if (selectedIndex > 0)
                    {
                        selectedIndex--;
                    }
                    break;

                case ConsoleKey.DownArrow:
                    if (selectedIndex < optionsWithPrices.Count - 1)
                    {
                        selectedIndex++;
                    }
                    break;

                case ConsoleKey.Enter:
                    return optionsWithPrices.Keys.ElementAt(selectedIndex);
            }
        }
    }


    static decimal CalculateTotalPriceForCake(
        Cake cake,
        Dictionary<string, decimal> formPrices,
        Dictionary<string, decimal> sizePrices,
        Dictionary<string, decimal> flavorPrices,
        Dictionary<string, decimal> glazePrices,
        Dictionary<string, decimal> decorPrices)
    {
        decimal totalPrice = 0;

        totalPrice += formPrices[cake.Form];
        totalPrice += sizePrices[cake.Size];
        totalPrice += flavorPrices[cake.Flavor];
        totalPrice += glazePrices[cake.Glaze];
        totalPrice += decorPrices[cake.Decor];

        return totalPrice;
    }

    static void DisplayOrder(List<Cake> orderList)
    {
        Console.Clear();
        Console.WriteLine("Ваш заказ и состав торта:");
        foreach (var cake in orderList)
        {
            Console.WriteLine($"Форма-{cake.Form}\nРазмер-{cake.Size}\nВкус-{cake.Flavor}\nГлазурь-{cake.Glaze}\nДекор-{cake.Decor}");
        }
        Console.WriteLine($"Суммарная цена заказа: {CalculateTotalPrice(orderList)} руб.");
        Console.WriteLine("\nНажмите Enter для продолжения...");
        Console.ReadKey();
    }

    static decimal CalculateTotalPrice(List<Cake> orderList)
    {
        decimal total = 0;
        foreach (var cake in orderList)
        {
            total += cake.Price;
        }
        return total;
    }

    static void SaveOrderToFile(List<Cake> orderList, decimal totalOrderPrice)
    {
        Console.Write("---Введите путь к файлу для сохранения заказа: \n");
        string filePath = Console.ReadLine();

        using (StreamWriter writer = new StreamWriter(filePath))
        {
            writer.WriteLine("Ваш заказ и состав торта:");
            foreach (var cake in orderList)
            {
                writer.WriteLine($"Форма-{cake.Form}\nРазмер-{cake.Size}\nВкус-{cake.Flavor}\nГлазурь-{cake.Glaze}\nДекор-{cake.Decor}");
            }
            writer.WriteLine($"Суммарная цена заказа: {totalOrderPrice} руб.");
        }

        Console.WriteLine($"Заказ сохранен в файл: {filePath}");
        Console.WriteLine("\nНажмите Enter для продолжения...");
        Console.ReadKey();
    }
}