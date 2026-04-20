using System.Collections.Generic;

namespace CalorieCalculatorApp.Models;

public class AppData
{
    public UserSettings Settings { get; set; } = new UserSettings();
    public List<Product> Products { get; set; } = new List<Product>();
    public List<Meal> Meals { get; set; } = new List<Meal>();
}