using System;
using System.Linq;

// Власний виняток для неправильних оцінок
class InvalidGradeException : Exception
{
    public InvalidGradeException(string message) : base(message) { }
}

class Program
{
    static void Main()
    {
        try
        {
            Console.WriteLine("Як ви хочете реалізувати задачу?");
            Console.WriteLine("1 - Використовувати структури");
            Console.WriteLine("2 - Використовувати кортежі");
            Console.WriteLine("3 - Використовувати записи");

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 3)
            {
                Console.WriteLine("Невірний вибір. Спробуйте ще раз.");
            }

            switch (choice)
            {
                case 1:
                    RunWithStructures();
                    break;
                case 2:
                    RunWithTuples();
                    break;
                case 3:
                    RunWithRecords();
                    break;
            }
        }
        catch (OverflowException)
        {
            Console.WriteLine("Занадто велике число введено. Спробуйте знову.");
        }
        catch (InvalidGradeException ex)
        {
            Console.WriteLine($"Помилка в оцінці: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Невідома помилка: {ex.Message}");
        }
    }

    static void RunWithStructures()
    {
        Console.WriteLine("Вибір: Структури");

        SchoolboyStruct[] schoolboys = new SchoolboyStruct[]
        {
            new SchoolboyStruct("Іван Іванов", "10-А", "123456789", 5, 4, 2, 4),
            new SchoolboyStruct("Марія Петрівна", "10-Б", "987654321", 3, 4, 4, 4),
            new SchoolboyStruct("Олег Олегович", "9-А", "555555555", 2, 3, 5, 5)
        };

        // Перевірка оцінок
        foreach (var s in schoolboys)
        {
            ValidateGrades(s.MathGrade, s.PhysicsGrade, s.RussianGrade, s.LiteratureGrade);
        }

        // Видалення з оцінкою 2
        schoolboys = schoolboys.Where(s => s.MathGrade != 2 && s.PhysicsGrade != 2 && s.RussianGrade != 2 && s.LiteratureGrade != 2).ToArray();

        // Новий школяр
        var newStudent = new SchoolboyStruct("Анатолій Антонов", "11-А", "123123123", 4, 4, 3, 5);
        ValidateGrades(newStudent.MathGrade, newStudent.PhysicsGrade, newStudent.RussianGrade, newStudent.LiteratureGrade);

        Array.Resize(ref schoolboys, schoolboys.Length + 1);
        for (int i = schoolboys.Length - 1; i > 0; i--)
            schoolboys[i] = schoolboys[i - 1];
        schoolboys[0] = newStudent;

        // Вивід
        Console.WriteLine("Школярі після модифікації:");
        foreach (var s in schoolboys)
        {
            Console.WriteLine($"{s.FullName}, {s.Class}, {s.PhoneNumber}, Оцінки: {s.MathGrade}, {s.PhysicsGrade}, {s.RussianGrade}, {s.LiteratureGrade}");
        }
    }

    static void ValidateGrades(params int[] grades)
    {
        foreach (var grade in grades)
        {
            if (grade < 1 || grade > 5)
                throw new InvalidGradeException($"Оцінка {grade} не входить в діапазон 1-5.");
        }
    }

    static void RunWithTuples()
    {
        Console.WriteLine("Вибір: Кортежі");

        var schoolboys = new[]
        {
            ("Іван Іванов", "10-А", "123456789", 5, 4, 2, 4),
            ("Марія Петрівна", "10-Б", "987654321", 3, 4, 4, 4),
            ("Олег Олегович", "9-А", "555555555", 2, 3, 5, 5)
        };

        foreach (var s in schoolboys)
        {
            ValidateGrades(s.Item4, s.Item5, s.Item6, s.Item7);
        }

        schoolboys = schoolboys.Where(s => s.Item4 != 2 && s.Item5 != 2 && s.Item6 != 2 && s.Item7 != 2).ToArray();

        var newStudent = ("Анатолій Антонов", "11-А", "123123123", 4, 4, 3, 5);
        ValidateGrades(newStudent.Item4, newStudent.Item5, newStudent.Item6, newStudent.Item7);
        schoolboys = new[] { newStudent }.Concat(schoolboys).ToArray();

        Console.WriteLine("Школярі після модифікації:");
        foreach (var s in schoolboys)
        {
            Console.WriteLine($"{s.Item1}, {s.Item2}, {s.Item3}, Оцінки: {s.Item4}, {s.Item5}, {s.Item6}, {s.Item7}");
        }
    }

    static void RunWithRecords()
    {
        Console.WriteLine("Вибір: Записи");

        var schoolboys = new[]
        {
            new SchoolboyRecord("Іван Іванов", "10-А", "123456789", 5, 4, 2, 4),
            new SchoolboyRecord("Марія Петрівна", "10-Б", "987654321", 3, 4, 4, 4),
            new SchoolboyRecord("Олег Олегович", "9-А", "555555555", 2, 3, 5, 5)
        };

        foreach (var s in schoolboys)
        {
            ValidateGrades(s.MathGrade, s.PhysicsGrade, s.RussianGrade, s.LiteratureGrade);
        }

        schoolboys = schoolboys.Where(s => s.MathGrade != 2 && s.PhysicsGrade != 2 && s.RussianGrade != 2 && s.LiteratureGrade != 2).ToArray();

        var newStudent = new SchoolboyRecord("Анатолій Антонов", "11-А", "123123123", 4, 4, 3, 5);
        ValidateGrades(newStudent.MathGrade, newStudent.PhysicsGrade, newStudent.RussianGrade, newStudent.LiteratureGrade);
        schoolboys = new[] { newStudent }.Concat(schoolboys).ToArray();

        Console.WriteLine("Школярі після модифікації:");
        foreach (var s in schoolboys)
        {
            Console.WriteLine($"{s.FullName}, {s.Class}, {s.PhoneNumber}, Оцінки: {s.MathGrade}, {s.PhysicsGrade}, {s.RussianGrade}, {s.LiteratureGrade}");
        }
    }
}

struct SchoolboyStruct
{
    public string FullName;
    public string Class;
    public string PhoneNumber;
    public int MathGrade;
    public int PhysicsGrade;
    public int RussianGrade;
    public int LiteratureGrade;

    public SchoolboyStruct(string fullName, string className, string phoneNumber, int mathGrade, int physicsGrade, int russianGrade, int literatureGrade)
    {
        FullName = fullName;
        Class = className;
        PhoneNumber = phoneNumber;
        MathGrade = mathGrade;
        PhysicsGrade = physicsGrade;
        RussianGrade = russianGrade;
        LiteratureGrade = literatureGrade;
    }
}

record SchoolboyRecord(string FullName, string Class, string PhoneNumber, int MathGrade, int PhysicsGrade, int RussianGrade, int LiteratureGrade);
