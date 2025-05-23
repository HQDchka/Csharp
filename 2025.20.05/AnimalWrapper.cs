public class AnimalWrapper : IAnimal
{
    private IAnimal _animal;

    public AnimalWrapper(IAnimal animal)
    {
        _animal = animal;
    }

    public string GetName() => _animal.GetName();

    public void MakeSound() => _animal.MakeSound();
}
