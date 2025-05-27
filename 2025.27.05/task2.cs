using System;

class Product : EventArgs
{
    public string Name { get; set; }
    public double Price { get; set; }
}

class Store
{
    public event EventHandler<Product> ProductAdded;

    public void AddProduct(string name, double price)
    {
        var product = new Product { Name = name, Price = price };
        Console.WriteLine($"Товар \"{product.Name}\" добавлен в магазин.");
        ProductAdded?.Invoke(this, product);
    }
}

class Customer
{
    public string Name { get; set; }

    public Customer(string name) => Name = name;

    public void OnProductAdded(object sender, Product product)
    {
        Console.WriteLine($"Уведомление для {Name}: добавлен товар \"{product.Name}\" по цене {product.Price}р.");
    }
}

class Program
{
    static void Main()
    {
        var store = new Store();

        var customer1 = new Customer("Клиент 1");
        var customer2 = new Customer("Клиент 2");
        var customer3 = new Customer("Клиент 3");

        store.ProductAdded += customer1.OnProductAdded;
        store.ProductAdded += customer2.OnProductAdded;
        store.ProductAdded += customer3.OnProductAdded;

        store.AddProduct("Книга", 500);
        store.AddProduct("Ноутбук", 45000);
    }
}
