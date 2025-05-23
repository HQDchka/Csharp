using System;
using System.Collections.Generic;

enum Color
{
    Red,
    Green,
    Blue,
    Yellow,
    White
}

abstract class Shape
{
    public Color Color { get; set; }
    public char Symbol { get; set; }

    public Shape(Color color, char symbol)
    {
        Color = color;
        Symbol = symbol;
    }

    public abstract void Draw();

    protected ConsoleColor GetConsoleColor()
    {
        return Color switch
        {
            Color.Red => ConsoleColor.Red,
            Color.Green => ConsoleColor.Green,
            Color.Blue => ConsoleColor.Blue,
            Color.Yellow => ConsoleColor.Yellow,
            Color.White => ConsoleColor.White,
            _ => ConsoleColor.White,
        };
    }
}

class Rectangle : Shape
{
    private int width = 10;
    private int height = 5;

    public Rectangle(Color color, char symbol) : base(color, symbol) { }

    public override void Draw()
    {
        Console.ForegroundColor = GetConsoleColor();
        for (int i = 0; i < height; i++)
        {
            Console.WriteLine(new string(Symbol, width));
        }
        Console.ResetColor();
    }

    public override string ToString() => "Прямоугольник";
}

class Rhombus : Shape
{
    private int size = 10;

    public Rhombus(Color color, char symbol) : base(color, symbol) { }

    public override void Draw()
    {
        Console.ForegroundColor = GetConsoleColor();

        for (int i = 1; i <= size; i += 2)
        {
            Console.WriteLine(new string(' ', (size - i) / 2) + new string(Symbol, i));
        }
        for (int i = size - 2; i >= 1; i -= 2)
        {
            Console.WriteLine(new string(' ', (size - i) / 2) + new string(Symbol, i));
        }

        Console.ResetColor();
    }

    public override string ToString() => "Ромб";
}

class Triangle : Shape
{
    private int height = 7;

    public Triangle(Color color, char symbol) : base(color, symbol) { }

    public override void Draw()
    {
        Console.ForegroundColor = GetConsoleColor();

        for (int i = 1; i <= height; i++)
        {
            Console.WriteLine(new string(' ', height - i) + new string(Symbol, 2 * i - 1));
        }

        Console.ResetColor();
    }

    public override string ToString() => "Треугольник";
}

class Trapezoid : Shape
{
    private int topWidth = 6;
    private int bottomWidth = 12;
    private int height = 5;

    public Trapezoid(Color color, char symbol) : base(color, symbol) { }

    public override void Draw()
    {
        Console.ForegroundColor = GetConsoleColor();

        double step = (double)(bottomWidth - topWidth) / (height - 1);
        for (int i = 0; i < height; i++)
        {
            int currentWidth = (int)(topWidth + step * i);
            int spaces = (bottomWidth - currentWidth) / 2;
            Console.WriteLine(new string(' ', spaces) + new string(Symbol, currentWidth));
        }

        Console.ResetColor();
    }

    public override string ToString() => "Трапеция";
}

class Polygon : Shape
{
    private int sides;
    private int size;

    public Polygon(Color color, char symbol, int sides = 6, int size = 6) : base(color, symbol)
    {
        this.sides = sides < 3 ? 3 : sides;
        this.size = size < 3 ? 3 : size;
    }

    public override void Draw()
    {
        // Очень простая имитация многоугольника - выводим фигуру из символов в форме многоугольника.
        // Для демонстрации — просто квадрат из символов с отступами, так как рисовать многоугольник в консоли сложно.
        Console.ForegroundColor = GetConsoleColor();

        for (int i = 0; i < size; i++)
        {
            Console.WriteLine(new string(Symbol, size));
        }

        Console.ResetColor();
    }

    public override string ToString() => $"Многоугольник ({sides} сторон)";
}

class GeneralShape
{
    private List<Shape> shapes = new List<Shape>();

    public void Add(Shape shape)
    {
        shapes.Add(shape);
    }

    public void DisplayAll()
    {
        if (shapes.Count == 0)
        {
            Console.WriteLine("Список фигур пуст.");
            return;
        }

        Console.WriteLine("\nОтображение всех выбранных фигур:");

        for (int i = 0; i < shapes.Count; i++)
        {
            var shape = shapes[i];
            Console.WriteLine($"\nФигура #{i + 1}: {shape}");
            Console.WriteLine($"Цвет фигуры: {shape.Color}");
            shape.Draw();
        }
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        GeneralShape generalShape = new GeneralShape();

        while (true)
        {
            Console.WriteLine("\nВыберите фигуру для добавления:");
            Console.WriteLine("1 - Прямоугольник");
            Console.WriteLine("2 - Ромб");
            Console.WriteLine("3 - Треугольник");
            Console.WriteLine("4 - Трапеция");
            Console.WriteLine("5 - Многоугольник");
            Console.WriteLine("6 - Показать все фигуры");
            Console.WriteLine("0 - Выход");
            Console.Write("Ваш выбор: ");

            string choice = Console.ReadLine();

            if (choice == "0") break;

            if (choice == "6")
            {
                generalShape.DisplayAll();
                continue;
            }

            Color color = ChooseColor();
            char symbol = ChooseSymbol();

            Shape shape = null;

            try
            {
                switch (choice)
                {
                    case "1":
                        shape = new Rectangle(color, symbol);
                        break;
                    case "2":
                        shape = new Rhombus(color, symbol);
                        break;
                    case "3":
                        shape = new Triangle(color, symbol);
                        break;
                    case "4":
                        shape = new Trapezoid(color, symbol);
                        break;
                    case "5":
                        Console.Write("Введите количество сторон многоугольника (не меньше 3): ");
                        if (!int.TryParse(Console.ReadLine(), out int sides) || sides < 3)
                            sides = 6; // По умолчанию 6
                        shape = new Polygon(color, symbol, sides);
                        break;
                    default:
                        Console.WriteLine("Некорректный выбор.");
                        continue;
                }

                generalShape.Add(shape);
                Console.WriteLine("Фигура добавлена.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }

    static Color ChooseColor()
    {
        Console.WriteLine("\nВыберите цвет фигуры:");
        Console.WriteLine("0 - Красный");
        Console.WriteLine("1 - Зелёный");
        Console.WriteLine("2 - Синий");
        Console.WriteLine("3 - Желтый");
        Console.WriteLine("4 - Белый");
        Console.Write("Цвет: ");

        if (int.TryParse(Console.ReadLine(), out int colorNum) && colorNum >= 0 && colorNum <= 4)
            return (Color)colorNum;

        Console.WriteLine("Некорректный ввод, выбран белый цвет по умолчанию.");
        return Color.White;
    }

    static char ChooseSymbol()
    {
        Console.Write("Введите символ для рисования фигуры (по умолчанию '*'): ");
        string input = Console.ReadLine();
        if (!string.IsNullOrEmpty(input))
            return input[0];
        return '*';
    }
}
