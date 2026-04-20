using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CalorieCalculatorApp.Models;
using CalorieCalculatorApp.Services;

namespace CalorieCalculatorApp;

public partial class SettingsPage : UserControl
{
    private UserSettings _settings => DataService.Settings;

    public SettingsPage()
    {
        AvaloniaXamlLoader.Load(this);
        DataContext = _settings;

        var femaleRadio = this.FindControl<RadioButton>("RadioFemale");
        var maleRadio = this.FindControl<RadioButton>("RadioMale");

        if (femaleRadio != null) femaleRadio.IsChecked = _settings.Gender == "Жінка";
        if (maleRadio != null) maleRadio.IsChecked = _settings.Gender == "Чоловік";
    }

    public void BtnSave_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var maleRadio = this.FindControl<RadioButton>("RadioMale");

        _settings.ErrorMessage = "";

        if (string.IsNullOrWhiteSpace(_settings.Name) ||
            string.IsNullOrWhiteSpace(_settings.Age) ||
            string.IsNullOrWhiteSpace(_settings.Weight) ||
            string.IsNullOrWhiteSpace(_settings.Height))
        {
            _settings.ErrorMessage = "Заповніть всі поля!";
            _settings.DailyCalorieGoal = 0;
            return;
        }

        if (!int.TryParse(_settings.Age, out int age))
        {
            _settings.ErrorMessage = "Вік має бути числом!";
            _settings.DailyCalorieGoal = 0;
            return;
        }

        if (!double.TryParse(_settings.Weight, out double weight))
        {
            _settings.ErrorMessage = "Вага має бути числом!";
            _settings.DailyCalorieGoal = 0;
            return;
        }

        if (!double.TryParse(_settings.Height, out double height))
        {
            _settings.ErrorMessage = "Зріст має бути числом!";
            _settings.DailyCalorieGoal = 0;
            return;
        }

        if (age < 5 || age > 120)
        {
            _settings.ErrorMessage = "Вік має бути від 5 до 120!";
            _settings.DailyCalorieGoal = 0;
            return;
        }

        if (weight < 20 || weight > 300)
        {
            _settings.ErrorMessage = "Вага має бути від 20 до 300 кг!";
            _settings.DailyCalorieGoal = 0;
            return;
        }

        if (height < 50 || height > 250)
        {
            _settings.ErrorMessage = "Зріст має бути від 50 до 250 см!";
            _settings.DailyCalorieGoal = 0;
            return;
        }

        _settings.Gender = maleRadio?.IsChecked == true ? "Чоловік" : "Жінка";
        _settings.DailyCalorieGoal = _settings.CalculateDailyCalories();

        DataService.Save();
    }
}
