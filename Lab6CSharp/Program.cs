using System;

// Загальні інтерфейси
interface IShowable
{
    void Show();
}

interface IHasTitle
{
    string Title { get; set; }
}

interface IHasYear
{
    int Year { get; set; }
}

// Клас Journal
class Journal : IShowable, IHasTitle, IHasYear, IDisposable
{
    public string Title { get; set; }
    public int Year { get; set; }
    public string Editor { get; set; }

    public Journal(string title, int year, string editor)
    {
        Title = title;
        Year = year;
        Editor = editor;
    }

    public void Show()
    {
        Console.WriteLine($"[Журнал] Назва: {Title}, Рік: {Year}, Редактор: {Editor}");
    }

    public void Dispose()
    {
        Console.WriteLine("Journal звільнено (Dispose)");
    }
}

// Клас Book
class Book : IShowable, IHasTitle, IHasYear, IDisposable
{
    public string Title { get; set; }
    public int Year { get; set; }
    public string Author { get; set; }

    public Book(string title, int year, string author)
    {
        Title = title;
        Year = year;
        Author = author;
    }

    public void Show()
    {
        Console.WriteLine($"[Книга] Назва: {Title}, Рік: {Year}, Автор: {Author}");
    }

    public void Dispose()
    {
        Console.WriteLine("Book звільнено (Dispose)");
    }
}

// Клас Textbook
class Textbook : IShowable, IHasTitle, IHasYear, IDisposable
{
    public string Title { get; set; }
    public int Year { get; set; }
    public string Author { get; set; }
    public string Subject { get; set; }

    public Textbook(string title, int year, string author, string subject)
    {
        Title = title;
        Year = year;
        Author = author;
        Subject = subject;
    }

    public void Show()
    {
        Console.WriteLine($"[Підручник] Назва: {Title}, Рік: {Year}, Автор: {Author}, Предмет: {Subject}");
    }

    public void Dispose()
    {
        Console.WriteLine("Textbook звільнено (Dispose)");
    }
}

// Точка входу
class Program
{
    static void Main()
    {
        Console.WriteLine("=== Перебудована ієрархія ===");

        IShowable journal = new Journal("Науковий журнал", 2024, "Іван Іванов");
        journal.Show();
        Console.WriteLine();

        IShowable book = new Book("C# для всіх", 2023, "Олена Петрівна");
        book.Show();
        Console.WriteLine();

        IShowable textbook = new Textbook("Математика", 2022, "Василь Іванович", "Алгебра");
        textbook.Show();
        Console.WriteLine();

        // Використання IDisposable через явне приведення
        ((IDisposable)journal).Dispose();
        ((IDisposable)book).Dispose();
        ((IDisposable)textbook).Dispose();
    }
}
