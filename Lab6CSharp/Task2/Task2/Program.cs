using System;

namespace ProductHierarchy
{
    // Інтерфейс "Товар", який успадковує IComparable для сортування за ціною
    public interface IProduct : IComparable<IProduct>
    {
        void DisplayInfo();
        bool IsExpired();
        decimal GetPrice(); // Метод для отримання ціни — для порівняння
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

        public bool IsExpired() => DateTime.Now > ExpiryDate;

        public decimal GetPrice() => Price;

        public int CompareTo(IProduct other) => Price.CompareTo(other.GetPrice());
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

        public bool IsExpired() => DateTime.Now > ExpiryDate;

        public decimal GetPrice() => Price;

        public int CompareTo(IProduct other) => Price.CompareTo(other.GetPrice());
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

        public bool IsExpired() => false;

        public decimal GetPrice() => Price;

        public int CompareTo(IProduct other) => Price.CompareTo(other.GetPrice());
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

            // Вивід початкового списку
            Console.WriteLine("Всі товари:");
            foreach (var product in products)
            {
                product.DisplayInfo();
                Console.WriteLine($"Прострочено: {product.IsExpired()}\n");
            }

            // Сортуємо за ціною (від дешевих до дорогих)
            Array.Sort(products);

            // Вивід після сортування
            Console.WriteLine("\nТовари після сортування за ціною:");
            foreach (var product in products)
            {
                product.DisplayInfo();
            }

            // Виводимо тільки прострочені товари
            Console.WriteLine("\nПрострочені товари:");
            foreach (var product in products)
            {
                if (product.IsExpired())
                    product.DisplayInfo();
            }
        }
    }
}
