namespace CalorieCalculatorApp.Models;

public class Meal
{
    public string ProductName { get; set; } = "";
    public double WeightGrams { get; set; }
    public double Calories { get; set; }
    public string Date { get; set; } = "";
}