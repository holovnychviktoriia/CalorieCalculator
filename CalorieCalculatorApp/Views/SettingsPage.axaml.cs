using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CalorieCalculatorApp.Models;

namespace CalorieCalculatorApp;

public partial class SettingsPage : UserControl
{
    private UserSettings _settings = new UserSettings();

    public SettingsPage()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public void SetSettings(UserSettings settings)
    {
        _settings = settings;

        var nameBox = this.FindControl<TextBox>("InputName");
        var ageBox = this.FindControl<TextBox>("InputAge");
        var weightBox = this.FindControl<TextBox>("InputWeight");
        var heightBox = this.FindControl<TextBox>("InputHeight");
        var femaleRadio = this.FindControl<RadioButton>("RadioFemale");
        var maleRadio = this.FindControl<RadioButton>("RadioMale");

        if (nameBox != null) nameBox.Text = settings.Name;
        if (ageBox != null) ageBox.Text = settings.Age.ToString();
        if (weightBox != null) weightBox.Text = settings.Weight.ToString();
        if (heightBox != null) heightBox.Text = settings.Height.ToString();
        if (femaleRadio != null) femaleRadio.IsChecked = settings.Gender == "Жінка";
        if (maleRadio != null) maleRadio.IsChecked = settings.Gender == "Чоловік";
    }

    public void BtnSave_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        var nameBox = this.FindControl<TextBox>("InputName");
        var ageBox = this.FindControl<TextBox>("InputAge");
        var weightBox = this.FindControl<TextBox>("InputWeight");
        var heightBox = this.FindControl<TextBox>("InputHeight");
        var maleRadio = this.FindControl<RadioButton>("RadioMale");
        var resultText = this.FindControl<TextBlock>("ResultText");

        if (string.IsNullOrWhiteSpace(nameBox?.Text) ||
            string.IsNullOrWhiteSpace(ageBox?.Text) ||
            string.IsNullOrWhiteSpace(weightBox?.Text) ||
            string.IsNullOrWhiteSpace(heightBox?.Text))
        {
            if (resultText != null)
                resultText.Text = "Заповніть всі поля!";
            return;
        }

        if (!double.TryParse(weightBox?.Text, out double weight) ||
            !double.TryParse(heightBox?.Text, out double height) ||
            !int.TryParse(ageBox?.Text, out int age))
        {
            if (resultText != null)
                resultText.Text = "Вік, вага та зріст мають бути числами!";
            return;
        }

        if (age < 5 || age > 120 || weight < 20 || weight > 300 || height < 50 || height > 250)
        {
            if (resultText != null)
                resultText.Text = "Введіть реалістичні дані!";
            return;
        }

        _settings.Name = nameBox?.Text ?? "";
        _settings.Gender = maleRadio?.IsChecked == true ? "Чоловік" : "Жінка";
        _settings.Weight = weight;
        _settings.Height = height;
        _settings.Age = age;

        _settings.DailyCalorieGoal = _settings.CalculateDailyCalories();

        if (resultText != null)
            resultText.Text = $"Ваша денна норма: {_settings.DailyCalorieGoal} ккал";
    }

    public UserSettings GetSettings() => _settings;
}
