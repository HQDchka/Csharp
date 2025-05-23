using System;
using System.Collections.Generic;

class CreditCard
{
    public string Holder { get; set; }
    public string CVC { get; set; }
    public decimal Balance { get; set; }

    public CreditCard(string holder, string cvc, decimal balance)
    {
        Holder = holder;
        CVC = cvc;
        Balance = balance;
    }

    public static CreditCard operator +(CreditCard card, decimal amount)
        => new CreditCard(card.Holder, card.CVC, card.Balance + amount);

    public static CreditCard operator -(CreditCard card, decimal amount)
        => new CreditCard(card.Holder, card.CVC, Math.Max(0, card.Balance - amount));

    public static bool operator ==(CreditCard a, CreditCard b)
    {
        if (ReferenceEquals(a, b)) return true;
        if (a is null || b is null) return false;
        return a.CVC == b.CVC;
    }

    public static bool operator !=(CreditCard a, CreditCard b) => !(a == b);
    public static bool operator >(CreditCard a, CreditCard b) => a.Balance > b.Balance;
    public static bool operator <(CreditCard a, CreditCard b) => a.Balance < b.Balance;

    public override bool Equals(object obj) => obj is CreditCard card && this == card;
    public override int GetHashCode() => CVC.GetHashCode();

    public void Print() => Console.WriteLine($"Владелец: {Holder}, CVC: {CVC}, Баланс: {Balance}₽");
}

class Program
{
    static List<CreditCard> cards = new List<CreditCard>();

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        while (true)
        {
            Console.WriteLine("\n=== МЕНЮ КРЕДИТНЫХ КАРТ ===");
            Console.WriteLine("1. Добавить карту");
            Console.WriteLine("2. Показать все карты");
            Console.WriteLine("3. Пополнить карту");
            Console.WriteLine("4. Снять деньги");
            Console.WriteLine("5. Сравнить карты (по балансу и CVC)");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите пункт: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1": AddCard(); break;
                case "2": ShowCards(); break;
                case "3": ModifyBalance(true); break;
                case "4": ModifyBalance(false); break;
                case "5": CompareCards(); break;
                case "0": return;
                default: Console.WriteLine("Неверный выбор."); break;
            }
        }
    }

    static void AddCard()
    {
        Console.Write("Имя владельца: ");
        string name = Console.ReadLine();
        Console.Write("CVC-код (3 цифры): ");
        string cvc = Console.ReadLine();
        Console.Write("Начальный баланс: ");
        if (decimal.TryParse(Console.ReadLine(), out decimal balance))
        {
            cards.Add(new CreditCard(name, cvc, balance));
            Console.WriteLine("Карта добавлена.");
        }
    }

    static void ShowCards()
    {
        for (int i = 0; i < cards.Count; i++)
        {
            Console.Write($"#{i + 1}: ");
            cards[i].Print();
        }
    }

    static void ModifyBalance(bool deposit)
    {
        ShowCards();
        Console.Write("Номер карты: ");
        if (int.TryParse(Console.ReadLine(), out int index) && index > 0 && index <= cards.Count)
        {
            Console.Write("Сумма: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal amount))
            {
                cards[index - 1] = deposit ? cards[index - 1] + amount : cards[index - 1] - amount;
                Console.WriteLine("Операция выполнена.");
            }
        }
    }

    static void CompareCards()
    {
        ShowCards();
        Console.Write("Номер первой карты: ");
        if (!int.TryParse(Console.ReadLine(), out int a) || a < 1 || a > cards.Count) return;
        Console.Write("Номер второй карты: ");
        if (!int.TryParse(Console.ReadLine(), out int b) || b < 1 || b > cards.Count) return;

        var card1 = cards[a - 1];
        var card2 = cards[b - 1];

        Console.WriteLine($"CVC одинаковые? {card1 == card2}");
        Console.WriteLine($"Баланс 1 > Баланса 2? {card1 > card2}");
        Console.WriteLine($"Баланс 1 < Баланса 2? {card1 < card2}");
    }
}
