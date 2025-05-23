using System;

class Program
{
    static void Main()
    {
        Console.WriteLine("======= Задание 1: Обёртка =======");
        IAnimal dog = new Dog();
        IAnimal wrappedDog = new AnimalWrapper(dog);
        Console.WriteLine($"Имя животного: {wrappedDog.GetName()}");
        wrappedDog.MakeSound();

        Console.WriteLine("\n======= Задание 2: Кастинг =======");
        IAnimal birdAnimal = new Bird();
        Console.WriteLine($"Проверка is IFlyable: {birdAnimal is IFlyable}");

        IFlyable birdFlyable = birdAnimal as IFlyable;
        birdFlyable?.Fly();

        ((Bird)birdAnimal).MakeSound();

        Console.WriteLine("\n======= Задание 3: Множественное наследование и склейка =======");
        Robot robot = new Robot();
        robot.Move();
        ((IMovable2)robot).Move();

        Console.WriteLine("\n======= Задание 4: Переименование методов =======");
        IRunnable runner = new Dog();
        runner.Run();

        Console.WriteLine("\n======= Задание 5: Защита от изменений =======");
        IUserService service = new UserService();
        Console.WriteLine(service.GetUserInfo(1));

        UserService actualService = new UserService();
        Console.WriteLine(actualService.GetUserInfo(1, true));
    }
}
