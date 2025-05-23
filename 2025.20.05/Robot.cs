using System;

public class Robot : IMovable1, IMovable2
{
    public void Move() => Console.WriteLine("Робот двигается (IMovable1)");

    void IMovable2.Move() => Console.WriteLine("Робот двигается (IMovable2 - явная реализация)");
}
