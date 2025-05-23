using System;

public class Bird : IAnimal, IFlyable
{
    public string GetName() => "Птица";

    public void MakeSound() => Console.WriteLine("Чирик-чирик");

    public void Fly() => Console.WriteLine("Птица летит");
}
