using System;

namespace CalorieCalculatorApp.Models;

public class UserSettings
{
    public string Name { get; set; } = "";
    public int Age { get; set; }
    public double Weight { get; set; }
    public double Height { get; set; }
    public string Gender { get; set; } = "Жінка";
    public double DailyCalorieGoal { get; set; } = 2000;

    public double CalculateDailyCalories()
    {
        double bmr;

        if (Gender == "Чоловік")
            bmr = 10 * Weight + 6.25 * Height - 5 * Age + 5;
        else
            bmr = 10 * Weight + 6.25 * Height - 5 * Age - 161;

        return Math.Round(bmr * 1.55);
    }
}