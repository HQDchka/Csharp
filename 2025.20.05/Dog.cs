using System;

public class Dog : IAnimal, IRunnable
{
    public string GetName() => "Собака";

    public void MakeSound() => Console.WriteLine("Гав-гав");

    void IRunnable.Run() => StartRunning();

    public void StartRunning() => Console.WriteLine("Собака побежала!");
}
