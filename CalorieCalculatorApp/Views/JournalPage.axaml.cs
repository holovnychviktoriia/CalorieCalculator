using System;
using System.Collections.ObjectModel;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CalorieCalculatorApp.Models;
using CalorieCalculatorApp.Services;

namespace CalorieCalculatorApp;

public partial class JournalPage : UserControl
{
    private ObservableCollection<Meal> _todayMeals = new ObservableCollection<Meal>();
    private string? _editingMealId = null;

    public JournalPage()
    {
        AvaloniaXamlLoader.Load(this);

        var productSelect = this.FindControl<ComboBox>("ProductSelect");
        if (productSelect != null) productSelect.ItemsSource = DataService.Products;

        var mealsList = this.FindControl<ItemsControl>("MealsList");
        if (mealsList != null) mealsList.ItemsSource = _todayMeals;

        ReloadTodayMeals();
    }

    private void ReloadTodayMeals()
    {
        string today = DateTime.Now.ToString("yyyy-MM-dd");

        _todayMeals.Clear();
        foreach (var meal in DataService.Meals)
        {
            if (meal.Date == today) _todayMeals.Add(meal);
        }

        UpdateTotal();
    }

    private void UpdateTotal()
    {
        double total = 0;
        foreach (var meal in _todayMeals) total += meal.Calories;

        var totalText = this.FindControl<TextBlock>("TotalText");
        if (totalText != null) totalText.Text = Math.Round(total) + " ккал";

        var emptyLabel = this.FindControl<TextBlock>("EmptyLabel");
        if (emptyLabel != null) emptyLabel.IsVisible = _todayMeals.Count == 0;
    }

    public void BtnSaveMeal_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var productSelect = this.FindControl<ComboBox>("ProductSelect");
        var weightBox = this.FindControl<TextBox>("InputWeight");
        var errorLabel = this.FindControl<TextBlock>("FormError");

        if (errorLabel != null) errorLabel.Text = "";

        if (productSelect?.SelectedItem is not Product selectedProduct)
        {
            if (errorLabel != null) errorLabel.Text = "Оберіть продукт!";
            return;
        }

        if (string.IsNullOrWhiteSpace(weightBox?.Text))
        {
            if (errorLabel != null) errorLabel.Text = "Введіть вагу!";
            return;
        }

        if (!double.TryParse(weightBox.Text, out double weight))
        {
            if (errorLabel != null) errorLabel.Text = "Вага має бути числом!";
            return;
        }

        if (weight <= 0 || weight > 5000)
        {
            if (errorLabel != null) errorLabel.Text = "Вага має бути від 1 до 5000 грамів!";
            return;
        }

        double calories = Math.Round(selectedProduct.CaloriesPer100g * weight / 100);

        if (_editingMealId != null)
        {
            foreach (var meal in DataService.Meals)
            {
                if (meal.Id == _editingMealId)
                {
                    meal.ProductName = selectedProduct.Name;
                    meal.WeightGrams = weight;
                    meal.Calories = calories;
                    break;
                }
            }
            ExitEditMode();
        }
        else
        {
            var newMeal = new Meal
            {
                ProductName = selectedProduct.Name,
                WeightGrams = weight,
                Calories = calories,
                Date = DateTime.Now.ToString("yyyy-MM-dd")
            };
            DataService.Meals.Add(newMeal);
        }

        ClearForm();
        ReloadTodayMeals();
        DataService.Save();
    }

    public void BtnEditMeal_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (sender is not Button btn) return;
        if (btn.Tag is not string mealId) return;

        Meal? meal = null;
        foreach (var m in DataService.Meals)
        {
            if (m.Id == mealId) { meal = m; break; }
        }
        if (meal == null) return;

        var productSelect = this.FindControl<ComboBox>("ProductSelect");
        var weightBox = this.FindControl<TextBox>("InputWeight");
        var formTitle = this.FindControl<TextBlock>("FormTitle");
        var saveBtn = this.FindControl<Button>("BtnSaveMeal");
        var cancelBtn = this.FindControl<Button>("BtnCancelEdit");

        if (productSelect != null)
        {
            foreach (var p in DataService.Products)
            {
                if (p.Name == meal.ProductName)
                {
                    productSelect.SelectedItem = p;
                    break;
                }
            }
        }

        if (weightBox != null) weightBox.Text = meal.WeightGrams.ToString();
        if (formTitle != null) formTitle.Text = "Редагувати прийом їжі";
        if (saveBtn != null) saveBtn.Content = "Зберегти";
        if (cancelBtn != null) cancelBtn.IsVisible = true;

        _editingMealId = mealId;
    }

    public void BtnCancelEdit_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        ExitEditMode();
        ClearForm();
    }

    public void BtnDeleteMeal_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (sender is not Button btn) return;
        if (btn.Tag is not string mealId) return;

        Meal? toRemove = null;
        foreach (var m in DataService.Meals)
        {
            if (m.Id == mealId) { toRemove = m; break; }
        }

        if (toRemove != null) DataService.Meals.Remove(toRemove);

        if (_editingMealId == mealId)
        {
            ExitEditMode();
            ClearForm();
        }

        ReloadTodayMeals();
        DataService.Save();
    }

    private void ExitEditMode()
    {
        _editingMealId = null;

        var formTitle = this.FindControl<TextBlock>("FormTitle");
        var saveBtn = this.FindControl<Button>("BtnSaveMeal");
        var cancelBtn = this.FindControl<Button>("BtnCancelEdit");

        if (formTitle != null) formTitle.Text = "Додати прийом їжі";
        if (saveBtn != null) saveBtn.Content = "+ Додати";
        if (cancelBtn != null) cancelBtn.IsVisible = false;
    }

    private void ClearForm()
    {
        var productSelect = this.FindControl<ComboBox>("ProductSelect");
        var weightBox = this.FindControl<TextBox>("InputWeight");
        var errorLabel = this.FindControl<TextBlock>("FormError");

        if (productSelect != null) productSelect.SelectedItem = null;
        if (weightBox != null) weightBox.Text = "";
        if (errorLabel != null) errorLabel.Text = "";
    }
}