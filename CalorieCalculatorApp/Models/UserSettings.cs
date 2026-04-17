using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CalorieCalculatorApp.Models;

public class UserSettings : INotifyPropertyChanged
{
    private string _name = "";
    private string _age = "";
    private string _weight = "";
    private string _height = "";
    private string _gender = "Жінка";
    private double _dailyCalorieGoal = 2000;
    private string _errorMessage = "";

    public string Name
    {
        get => _name;
        set { _name = value; OnPropertyChanged(); }
    }

    public string Age
    {
        get => _age;
        set { _age = value; OnPropertyChanged(); }
    }

    public string Weight
    {
        get => _weight;
        set { _weight = value; OnPropertyChanged(); }
    }

    public string Height
    {
        get => _height;
        set { _height = value; OnPropertyChanged(); }
    }

    public string Gender
    {
        get => _gender;
        set { _gender = value; OnPropertyChanged(); }
    }

    public double DailyCalorieGoal
    {
        get => _dailyCalorieGoal;
        set { _dailyCalorieGoal = value; OnPropertyChanged(); }
    }

    public string ErrorMessage
    {
        get => _errorMessage;
        set { _errorMessage = value; OnPropertyChanged(); }
    }

    public double CalculateDailyCalories()
    {
        if (!int.TryParse(Age, out int age)) return 0;
        if (!double.TryParse(Weight, out double weight)) return 0;
        if (!double.TryParse(Height, out double height)) return 0;

        double bmr;

        if (Gender == "Чоловік")
            bmr = 10 * weight + 6.25 * height - 5 * age + 5;
        else
            bmr = 10 * weight + 6.25 * height - 5 * age - 161;

        return Math.Round(bmr * 1.55);
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}
