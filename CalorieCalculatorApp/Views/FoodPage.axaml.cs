using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CalorieCalculatorApp.Models;
using System.Collections.ObjectModel;

namespace CalorieCalculatorApp;

public partial class FoodPage : UserControl
{
    public ObservableCollection<Product> Products { get; set; } = new ObservableCollection<Product>();

    public FoodPage()
    {
        AvaloniaXamlLoader.Load(this);
        DataContext = this;

        Products.Add(new Product { Name = "Яблуко", CaloriesPer100g = 52 });
        Products.Add(new Product { Name = "Банан", CaloriesPer100g = 89 });
        Products.Add(new Product { Name = "Куряча грудка", CaloriesPer100g = 165 });
    }

    public void BtnAddProduct_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var nameBox = this.FindControl<TextBox>("InputProductName");
        var caloriesBox = this.FindControl<TextBox>("InputProductCalories");

        if (string.IsNullOrWhiteSpace(nameBox?.Text)) return;
        if (!double.TryParse(caloriesBox?.Text, out double calories)) return;

        Products.Add(new Product
        {
            Name = nameBox.Text,
            CaloriesPer100g = calories
        });

        nameBox.Text = "";
        caloriesBox!.Text = "";
    }
}
