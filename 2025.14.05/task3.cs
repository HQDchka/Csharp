using System;
using System.Collections.Generic;

class City
{
    public string Name { get; set; }
    public int Population { get; set; }

    public City(string name, int population)
    {
        Name = name;
        Population = population;
    }

    public static City operator +(City city, int value)
    {
        return new City(city.Name, city.Population + value);
    }

    public static City operator -(City city, int value)
    {
        return new City(city.Name, Math.Max(0, city.Population - value));
    }

    public static bool operator ==(City a, City b)
    {
        if (ReferenceEquals(a, b)) return true;
        if (a is null || b is null) return false;
        return a.Population == b.Population;
    }

    public static bool operator !=(City a, City b) => !(a == b);
    public static bool operator >(City a, City b) => a.Population > b.Population;
    public static bool operator <(City a, City b) => a.Population < b.Population;

    public override bool Equals(object obj) => obj is City c && this == c;
    public override int GetHashCode() => Population.GetHashCode();

    public void Print() => Console.WriteLine($"Город: {Name}, Население: {Population}");
}

class Program
{
    static List<City> cities = new List<City>();

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        while (true)
        {
            Console.WriteLine("\n=== МЕНЮ ГОРОДОВ ===");
            Console.WriteLine("1. Добавить город");
            Console.WriteLine("2. Показать все города");
            Console.WriteLine("3. Увеличить население");
            Console.WriteLine("4. Уменьшить население");
            Console.WriteLine("5. Сравнить города по населению");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите пункт: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddCity(); break;
                case "2": ShowCities(); break;
                case "3": ModifyPopulation(true); break;
                case "4": ModifyPopulation(false); break;
                case "5": CompareCities(); break;
                case "0": return;
                default: Console.WriteLine("Неверный выбор."); break;
            }
        }
    }

    static void AddCity()
    {
        Console.Write("Название города: ");
        string name = Console.ReadLine();
        Console.Write("Население: ");
        if (int.TryParse(Console.ReadLine(), out int population))
        {
            cities.Add(new City(name, population));
            Console.WriteLine("Город добавлен.");
        }
    }

    static void ShowCities()
    {
        for (int i = 0; i < cities.Count; i++)
        {
            Console.Write($"#{i + 1}: ");
            cities[i].Print();
        }
    }

    static void ModifyPopulation(bool increase)
    {
        ShowCities();
        Console.Write("Номер города: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= cities.Count)
        {
            Console.Write("Введите значение изменения населения: ");
            if (int.TryParse(Console.ReadLine(), out int value))
            {
                cities[index - 1] = increase ? cities[index - 1] + value : cities[index - 1] - value;
                Console.WriteLine("Изменения применены.");
            }
        }
    }

    static void CompareCities()
    {
        ShowCities();
        Console.Write("Номер первого города: ");
        if (!int.TryParse(Console.ReadLine(), out int a) || a < 1 || a > cities.Count) return;
        Console.Write("Номер второго города: ");
        if (!int.TryParse(Console.ReadLine(), out int b) || b < 1 || b > cities.Count) return;

        var city1 = cities[a - 1];
        var city2 = cities[b - 1];

        Console.WriteLine($"Равны? {city1 == city2}");
        Console.WriteLine($"Не равны? {city1 != city2}");
        Console.WriteLine($"Город 1 > Город 2? {city1 > city2}");
        Console.WriteLine($"Город 1 < Город 2? {city1 < city2}");
    }
}
