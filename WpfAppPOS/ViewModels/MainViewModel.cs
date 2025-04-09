using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace WpfAppPOS.ViewModels
{
    public class MainViewModel
    {
        public string ProductCode { get; set; } = string.Empty;
        public List<Product> Products { get; set; } = new List<Product>
        {
            new Product { Code = "P001", Name = "Apple", Price = 1.00, Quantity = 1 },
            new Product { Code = "P002", Name = "Banana", Price = 0.50, Quantity = 1 },
            new Product { Code = "P003", Name = "Orange", Price = 0.75, Quantity = 1 }
        };

        public List<Product> Cart { get; set; } = new List<Product>();
        public string Total => Cart.Sum(p => p.Price * p.Quantity).ToString("C");

        public ICommand AddProductCommand { get; set; }

        public MainViewModel()
        {
            AddProductCommand = new RelayCommand(AddToCart);
        }

        private void AddToCart()
        {
            var product = Products.FirstOrDefault(p => p.Code == ProductCode);
            if (product != null)
            {
                Cart.Add(new Product
                {
                    Code = product.Code,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = 1
                });
                ProductCode = string.Empty;
            }
        }
    }

    public class Product
    {
        public required string Code { get; set; }
        public required string Name { get; set; }
        public double Price { get; set; }
        public int Quantity { get; set; }
    }

    // RelayCommand class to handle command logic
    public class RelayCommand : ICommand
    {
        private readonly Action _execute;
        private readonly Predicate<object>? _canExecute;

        public RelayCommand(Action execute) : this(execute, null) { }

        public RelayCommand(Action execute, Predicate<object>? canExecute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object? parameter) => _canExecute?.Invoke(parameter) ?? true;

        public void Execute(object? parameter) => _execute();
    }
}