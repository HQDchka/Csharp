using System;
using System.Collections.Generic;

delegate double MathOperation(double a, double b);

class Calculator
{
    static double Add(double a, double b) => a + b;
    static double Subtract(double a, double b) => a - b;
    static double Multiply(double a, double b) => a * b;
    static double Divide(double a, double b)
    {
        if (b == 0)
            throw new DivideByZeroException("Деление на ноль недопустимо.");
        return a / b;
    }
    static double Power(double a, double b) => Math.Pow(a, b);

    static void Main()
    {
        var operations = new Dictionary<string, MathOperation>
        {
            { "+", Add },
            { "-", Subtract },
            { "*", Multiply },
            { "/", Divide },
            { "^", Power }
        };

        try
        {
            Console.Write("Введите первое число: ");
            double x = double.Parse(Console.ReadLine());

            Console.Write("Введите второе число: ");
            double y = double.Parse(Console.ReadLine());

            Console.Write("Введите операцию (+, -, *, /, ^): ");
            string op = Console.ReadLine();

            if (operations.TryGetValue(op, out MathOperation operation))
            {
                double result = operation(x, y);
                Console.WriteLine($"Результат: {result}");
            }
            else
            {
                Console.WriteLine("Ошибка: Неизвестная операция.");
            }
        }
        catch (DivideByZeroException ex)
        {
            Console.WriteLine($"Ошибка: {ex.Message}");
        }
        catch (FormatException)
        {
            Console.WriteLine("Ошибка: Неверный формат числа.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Непредвиденная ошибка: {ex.Message}");
        }
    }
}
