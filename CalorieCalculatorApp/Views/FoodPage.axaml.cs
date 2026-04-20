using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CalorieCalculatorApp.Models;
using CalorieCalculatorApp.Services;

namespace CalorieCalculatorApp;

public partial class FoodPage : UserControl
{
    public ObservableCollection<Product> Products => DataService.Products;

    public FoodPage()
    {
        AvaloniaXamlLoader.Load(this);
        DataContext = this;
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

        DataService.Save();
    }
}
