using System;
using System.Collections.Generic;

class Employee
{
    public string Name { get; set; }
    public decimal Salary { get; set; }

    public Employee(string name, decimal salary)
    {
        Name = name;
        Salary = salary;
    }

    public static Employee operator +(Employee e, decimal amount)
    {
        return new Employee(e.Name, e.Salary + amount);
    }

    public static Employee operator -(Employee e, decimal amount)
    {
        return new Employee(e.Name, e.Salary - amount);
    }

    public static bool operator ==(Employee e1, Employee e2)
    {
        return e1.Salary == e2.Salary;
    }

    public static bool operator !=(Employee e1, Employee e2)
    {
        return e1.Salary != e2.Salary;
    }

    public static bool operator <(Employee e1, Employee e2)
    {
        return e1.Salary < e2.Salary;
    }

    public static bool operator >(Employee e1, Employee e2)
    {
        return e1.Salary > e2.Salary;
    }

    public override bool Equals(object obj)
    {
        return obj is Employee emp && Salary == emp.Salary;
    }

    public override int GetHashCode()
    {
        return Salary.GetHashCode();
    }

    public override string ToString()
    {
        return $"{Name}, зарплата: {Salary} руб.";
    }
}

class Program
{
    static List<Employee> employees = new List<Employee>();

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        while (true)
        {
            Console.WriteLine("\n--- МЕНЮ ---");
            Console.WriteLine("1. Добавить сотрудника");
            Console.WriteLine("2. Показать всех сотрудников");
            Console.WriteLine("3. Изменить зарплату сотрудника");
            Console.WriteLine("4. Сравнить зарплаты двух сотрудников");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите пункт: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddEmployee(); break;
                case "2": ShowEmployees(); break;
                case "3": ChangeSalary(); break;
                case "4": CompareEmployees(); break;
                case "0": return;
                default: Console.WriteLine("Неверный ввод."); break;
            }
        }
    }

    static void AddEmployee()
    {
        Console.Write("Имя сотрудника: ");
        string name = Console.ReadLine();

        Console.Write("Зарплата: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal salary))
        {
            employees.Add(new Employee(name, salary));
            Console.WriteLine("Сотрудник добавлен.");
        }
        else
        {
            Console.WriteLine("Некорректная зарплата.");
        }
    }

    static void ShowEmployees()
    {
        if (employees.Count == 0)
        {
            Console.WriteLine("Нет сотрудников.");
            return;
        }

        for (int i = 0; i < employees.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {employees[i]}");
        }
    }

    static void ChangeSalary()
    {
        ShowEmployees();
        Console.Write("Выберите номер сотрудника: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= employees.Count)
        {
            var emp = employees[index - 1];

            Console.Write("Введите сумму изменения зарплаты (может быть отрицательной): ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                employees[index - 1] = amount >= 0 ? emp + amount : emp - Math.Abs(amount);
                Console.WriteLine("Зарплата изменена.");
            }
            else
            {
                Console.WriteLine("Некорректная сумма.");
            }
        }
        else
        {
            Console.WriteLine("Некорректный выбор.");
        }
    }

    static void CompareEmployees()
    {
        ShowEmployees();
        Console.Write("Введите номер первого сотрудника: ");
        if (!int.TryParse(Console.ReadLine(), out int first) || first < 1 || first > employees.Count)
        {
            Console.WriteLine("Некорректный номер.");
            return;
        }

        Console.Write("Введите номер второго сотрудника: ");
        if (!int.TryParse(Console.ReadLine(), out int second) || second < 1 || second > employees.Count)
        {
            Console.WriteLine("Некорректный номер.");
            return;
        }

        var emp1 = employees[first - 1];
        var emp2 = employees[second - 1];

        Console.WriteLine($"\nСравнение {emp1.Name} и {emp2.Name}:");
        Console.WriteLine($"{emp1.Name} == {emp2.Name}? {emp1 == emp2}");
        Console.WriteLine($"{emp1.Name} != {emp2.Name}? {emp1 != emp2}");
        Console.WriteLine($"{emp1.Name} > {emp2.Name}? {emp1 > emp2}");
        Console.WriteLine($"{emp1.Name} < {emp2.Name}? {emp1 < emp2}");
    }
}
