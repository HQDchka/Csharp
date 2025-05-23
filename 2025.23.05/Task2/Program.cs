using System;

abstract class Storage
{
    private string name;
    private string model;

    public string Name
    {
        get => name;
        set => name = value;
    }

    public string Model
    {
        get => model;
        set => model = value;
    }

    // Абстрактные методы
    public abstract double GetCapacityGB();  // объем в Гб
    public abstract double GetFreeSpaceGB(); // свободное место (можно для упрощения сделать равно GetCapacityGB)
    public abstract double CopyDataTimeHours(double dataSizeGB); // время копирования в часах
    public abstract void CopyData(double dataSizeGB); // имитация копирования (например, просто вывод)
    public abstract string GetFullInfo();
}

class Flash : Storage
{
    public double UsbSpeedMBps { get; set; }  // скорость USB 3.0 в МБ/с
    public double CapacityGB { get; set; }    // объем памяти в Гб

    public override double GetCapacityGB() => CapacityGB;
    public override double GetFreeSpaceGB() => CapacityGB;

    public override double CopyDataTimeHours(double dataSizeGB)
    {
        // Время = объем данных (МБ) / скорость (МБ/с)
        // 1 Гб = 1024 Мб
        double dataSizeMB = dataSizeGB * 1024;
        return dataSizeMB / UsbSpeedMBps / 3600; // в часах
    }

    public override void CopyData(double dataSizeGB)
    {
        Console.WriteLine($"Копирование {dataSizeGB:F2} Гб на Flash {Name} {Model}...");
    }

    public override string GetFullInfo()
    {
        return $"Flash: {Name} {Model}, Объем: {CapacityGB} Гб, Скорость USB 3.0: {UsbSpeedMBps} МБ/с";
    }
}

class DVD : Storage
{
    public double SpeedMBps { get; set; } // скорость чтения/записи в МБ/с
    public bool IsDoubleSided { get; set; } // true = 9 Гб, false = 4.7 Гб

    public override double GetCapacityGB() => IsDoubleSided ? 9.0 : 4.7;
    public override double GetFreeSpaceGB() => GetCapacityGB();

    public override double CopyDataTimeHours(double dataSizeGB)
    {
        double dataSizeMB = dataSizeGB * 1024;
        return dataSizeMB / SpeedMBps / 3600;
    }

    public override void CopyData(double dataSizeGB)
    {
        Console.WriteLine($"Копирование {dataSizeGB:F2} Гб на DVD {Name} {Model}...");
    }

    public override string GetFullInfo()
    {
        string type = IsDoubleSided ? "двусторонний (9 Гб)" : "односторонний (4.7 Гб)";
        return $"DVD: {Name} {Model}, Тип: {type}, Скорость: {SpeedMBps} МБ/с";
    }
}

class HDD : Storage
{
    public double UsbSpeedMBps { get; set; } // скорость USB 2.0 в МБ/с
    public int PartitionsCount { get; set; } // количество разделов
    public double PartitionSizeGB { get; set; } // объем каждого раздела в Гб

    public override double GetCapacityGB() => PartitionsCount * PartitionSizeGB;
    public override double GetFreeSpaceGB() => GetCapacityGB();

    public override double CopyDataTimeHours(double dataSizeGB)
    {
        double dataSizeMB = dataSizeGB * 1024;
        return dataSizeMB / UsbSpeedMBps / 3600;
    }

    public override void CopyData(double dataSizeGB)
    {
        Console.WriteLine($"Копирование {dataSizeGB:F2} Гб на HDD {Name} {Model}...");
    }

    public override string GetFullInfo()
    {
        return $"HDD: {Name} {Model}, Разделов: {PartitionsCount}, Объем раздела: {PartitionSizeGB} Гб, Скорость USB 2.0: {UsbSpeedMBps} МБ/с";
    }
}

class Program
{
    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        double totalDataGB = 565; // общий размер данных
        double fileSizeMB = 780;  // размер файла

        // Создаем массив носителей
        Storage[] devices = new Storage[3];

        devices[0] = new Flash()
        {
            Name = "SanDisk",
            Model = "Ultra",
            CapacityGB = 64,
            UsbSpeedMBps = 100 // 100 МБ/с
        };

        devices[1] = new DVD()
        {
            Name = "Sony",
            Model = "DVD-RW",
            IsDoubleSided = false,
            SpeedMBps = 11 // около 8x DVD скорость ~11 МБ/с
        };

        devices[2] = new HDD()
        {
            Name = "Western Digital",
            Model = "My Passport",
            PartitionsCount = 2,
            PartitionSizeGB = 500,
            UsbSpeedMBps = 30 // скорость USB 2.0 ~30 МБ/с
        };

        Console.WriteLine("Носители информации:");
        foreach (var device in devices)
            Console.WriteLine(device.GetFullInfo());

        // Расчет общего объема
        double totalCapacity = 0;
        foreach (var device in devices)
            totalCapacity += device.GetCapacityGB();

        Console.WriteLine($"\nОбщий объем всех устройств: {totalCapacity:F2} Гб");

        // Расчет количества носителей каждого типа для хранения всех данных
        Console.WriteLine("\nРасчет необходимого количества носителей каждого типа:");

        foreach (var device in devices)
        {
            int needed = (int)Math.Ceiling(totalDataGB / device.GetCapacityGB());
            Console.WriteLine($"{device.Name} {device.Model} - необходимо {needed} шт.");
        }

        // Расчет времени копирования (пример: копируем все данные на каждый носитель отдельно)
        Console.WriteLine("\nВремя копирования 565 Гб на каждый носитель:");
        foreach (var device in devices)
        {
            double timeHours = device.CopyDataTimeHours(totalDataGB);
            Console.WriteLine($"{device.Name} {device.Model}: {timeHours:F2} часов");
        }

        // Имитация копирования
        Console.WriteLine("\nИмитация копирования данных:");
        foreach (var device in devices)
        {
            device.CopyData(totalDataGB);
        }
    }
}
