using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using CalorieCalculatorApp.Models;

namespace CalorieCalculatorApp.Services;

public static class DataService
{
    private static readonly string DataFile = FindDataFilePath();

    public static UserSettings Settings { get; private set; } = new UserSettings();
    public static ObservableCollection<Product> Products { get; } = new ObservableCollection<Product>();
    public static ObservableCollection<Meal> Meals { get; } = new ObservableCollection<Meal>();

    private static string FindDataFilePath()
    {
        var current = AppContext.BaseDirectory;
        var dir = new DirectoryInfo(current);

        while (dir != null)
        {
            if (File.Exists(Path.Combine(dir.FullName, "CalorieCalculatorApp.csproj")))
                return Path.Combine(dir.FullName, "data.json");

            dir = dir.Parent;
        }

        return Path.Combine(AppContext.BaseDirectory, "data.json");
    }

    public static void Load()
    {
        try
        {
            if (!File.Exists(DataFile))
            {
                SeedDefaults();
                Save();
                return;
            }

            var json = File.ReadAllText(DataFile);
            var data = JsonSerializer.Deserialize<AppData>(json);

            if (data == null)
            {
                SeedDefaults();
                return;
            }

            Settings.Name = data.Settings.Name;
            Settings.Age = data.Settings.Age;
            Settings.Weight = data.Settings.Weight;
            Settings.Height = data.Settings.Height;
            Settings.Gender = data.Settings.Gender;
            Settings.DailyCalorieGoal = data.Settings.DailyCalorieGoal;

            Products.Clear();
            foreach (var p in data.Products) Products.Add(p);

            Meals.Clear();
            foreach (var m in data.Meals) Meals.Add(m);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка завантаження даних: {ex.Message}");
            SeedDefaults();
        }
    }

    public static void Save()
    {
        try
        {
            var data = new AppData
            {
                Settings = Settings,
                Products = new List<Product>(Products),
                Meals = new List<Meal>(Meals)
            };

            var options = new JsonSerializerOptions { WriteIndented = true };
            var json = JsonSerializer.Serialize(data, options);
            File.WriteAllText(DataFile, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Помилка збереження даних: {ex.Message}");
        }
    }

    private static void SeedDefaults()
    {
        Products.Clear();
        Products.Add(new Product { Name = "Яблуко", CaloriesPer100g = 52 });
        Products.Add(new Product { Name = "Банан", CaloriesPer100g = 89 });
        Products.Add(new Product { Name = "Куряча грудка", CaloriesPer100g = 165 });
        Products.Add(new Product { Name = "Рис варений", CaloriesPer100g = 130 });
        Products.Add(new Product { Name = "Хліб білий", CaloriesPer100g = 250 });
    }

    public static double GetCaloriesEatenToday()
    {
        var today = DateTime.Now.ToString("yyyy-MM-dd");
        double total = 0;
        foreach (var meal in Meals)
        {
            if (meal.Date == today) total += meal.Calories;
        }
        return total;
    }
}
