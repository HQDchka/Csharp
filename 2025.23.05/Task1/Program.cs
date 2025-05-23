using System;

class RangeOfArray
{
    private int[] array;
    private int lowerBound;
    private int upperBound;

    public RangeOfArray(int lowerBound, int upperBound)
    {
        if (upperBound < lowerBound)
            throw new ArgumentException("Верхняя граница должна быть больше или равна нижней.");

        this.lowerBound = lowerBound;
        this.upperBound = upperBound;
        array = new int[upperBound - lowerBound + 1];
    }

    public int this[int index]
    {
        get
        {
            if (index < lowerBound || index > upperBound)
                throw new IndexOutOfRangeException("Индекс вне допустимого диапазона.");
            return array[index - lowerBound];
        }
        set
        {
            if (index < lowerBound || index > upperBound)
                throw new IndexOutOfRangeException("Индекс вне допустимого диапазона.");
            array[index - lowerBound] = value;
        }
    }

    public void PrintRange()
    {
        Console.WriteLine($"Диапазон индексов: от {lowerBound} до {upperBound}");
    }

    public void PrintAll()
    {
        for (int i = lowerBound; i <= upperBound; i++)
        {
            Console.WriteLine($"Элемент с индексом {i}: {this[i]}");
        }
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.Write("Введите нижнюю границу индексов: ");
        int lower = int.Parse(Console.ReadLine());

        Console.Write("Введите верхнюю границу индексов: ");
        int upper = int.Parse(Console.ReadLine());

        RangeOfArray arr = new RangeOfArray(lower, upper);
        bool running = true;

        while (running)
        {
            Console.WriteLine("\nМеню:");
            Console.WriteLine("1. Установить значение элемента");
            Console.WriteLine("2. Получить значение элемента");
            Console.WriteLine("3. Показать весь массив");
            Console.WriteLine("4. Показать диапазон индексов");
            Console.WriteLine("5. Выход");
            Console.Write("Выберите действие: ");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Введите индекс: ");
                    int indexSet = int.Parse(Console.ReadLine());
                    Console.Write("Введите значение: ");
                    int valueSet = int.Parse(Console.ReadLine());
                    try
                    {
                        arr[indexSet] = valueSet;
                        Console.WriteLine("Значение установлено.");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ошибка: " + ex.Message);
                    }
                    break;

                case "2":
                    Console.Write("Введите индекс: ");
                    int indexGet = int.Parse(Console.ReadLine());
                    try
                    {
                        Console.WriteLine($"Значение: {arr[indexGet]}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Ошибка: " + ex.Message);
                    }
                    break;

                case "3":
                    arr.PrintAll();
                    break;

                case "4":
                    arr.PrintRange();
                    break;

                case "5":
                    running = false;
                    Console.WriteLine("Выход из программы...");
                    break;

                default:
                    Console.WriteLine("Неверный выбор. Попробуйте снова.");
                    break;
            }
        }
    }
}
