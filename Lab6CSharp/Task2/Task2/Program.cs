using System;

namespace ProductHierarchy
{
    // Інтерфейс "Товар"
    public interface IProduct
    {
        void DisplayInfo();
        bool IsExpired();
    }

    // Клас "Продукт"
    public class Product : IProduct
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        public Product(string name, decimal price, DateTime manufactureDate, DateTime expiryDate)
        {
            Name = name;
            Price = price;
            ManufactureDate = manufactureDate;
            ExpiryDate = expiryDate;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Продукт: {Name}, Ціна: {Price}, Дата виробництва: {ManufactureDate.ToShortDateString()}, Строк придатності: {ExpiryDate.ToShortDateString()}");
        }

        public bool IsExpired()
        {
            return DateTime.Now > ExpiryDate;
        }
    }

    // Клас "Партія"
    public class Batch : IProduct
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public DateTime ManufactureDate { get; set; }
        public DateTime ExpiryDate { get; set; }

        public Batch(string name, decimal price, int quantity, DateTime manufactureDate, DateTime expiryDate)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            ManufactureDate = manufactureDate;
            ExpiryDate = expiryDate;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Партія: {Name}, Ціна: {Price}, Кількість: {Quantity}, Дата виробництва: {ManufactureDate.ToShortDateString()}, Строк придатності: {ExpiryDate.ToShortDateString()}");
        }

        public bool IsExpired()
        {
            return DateTime.Now > ExpiryDate;
        }
    }

    // Клас "Комплект"
    public class Set : IProduct
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string[] ProductNames { get; set; }

        public Set(string name, decimal price, string[] productNames)
        {
            Name = name;
            Price = price;
            ProductNames = productNames;
        }

        public void DisplayInfo()
        {
            Console.WriteLine($"Комплект: {Name}, Ціна: {Price}, Продукти: {string.Join(", ", ProductNames)}");
        }

        public bool IsExpired()
        {
            return false; // Комплект не має дати придатності, тільки продукти.
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // Створюємо масив товарів
            IProduct[] products = new IProduct[]
            {
                new Product("Молоко", 25.50m, new DateTime(2024, 1, 1), new DateTime(2025, 1, 1)),
                new Batch("Партія яблук", 15.30m, 100, new DateTime(2024, 3, 10), new DateTime(2024, 5, 1)),
                new Set("Комплект для пікніка", 200.00m, new string[] { "Ніж", "Тарілка", "Ковдра" })
            };

            // Виводимо інформацію про всі товари
            foreach (var product in products)
            {
                product.DisplayInfo();
                Console.WriteLine($"Прострочено: {product.IsExpired()}\n");
            }

            // Пошук прострочених товарів
            Console.WriteLine("Прострочені товари:");
            foreach (var product in products)
            {
                if (product.IsExpired())
                {
                    product.DisplayInfo();
                }
            }
        }
    }
}
