using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using CalorieCalculatorApp.Controls;
using CalorieCalculatorApp.Services;

namespace CalorieCalculatorApp;

public partial class HomePage : UserControl
{
    public HomePage()
    {
        AvaloniaXamlLoader.Load(this);
        UpdateStats();
    }

    public void UpdateStats()
    {
        double eaten = DataService.GetCaloriesEatenToday();
        double goal = DataService.Settings.DailyCalorieGoal;
        double remaining = goal - eaten;

        var eatenCard = this.FindControl<StatCard>("EatenCard");
        if (eatenCard != null) eatenCard.Value = Math.Round(eaten) + " ккал";

        var goalCard = this.FindControl<StatCard>("GoalCard");
        if (goalCard != null) goalCard.Value = Math.Round(goal) + " ккал";

        var remainingCard = this.FindControl<StatCard>("RemainingCard");
        if (remainingCard != null)
        {
            if (remaining < 0)
                remainingCard.Value = "перевищено на " + Math.Round(-remaining) + " ккал";
            else
                remainingCard.Value = Math.Round(remaining) + " ккал";
        }

        var statusText = this.FindControl<TextBlock>("StatusText");
        if (statusText != null)
        {
            if (goal <= 0)
            {
                statusText.Text = "Вкажіть персональні параметри у Налаштуваннях";
                statusText.Foreground = new SolidColorBrush(Color.Parse("#888"));
            }
            else if (eaten == 0)
            {
                statusText.Text = "Почніть день — додайте перший прийом їжі";
                statusText.Foreground = new SolidColorBrush(Color.Parse("#888"));
            }
            else if (remaining > goal * 0.3)
            {
                statusText.Text = "Продовжуйте у тому ж дусі!";
                statusText.Foreground = new SolidColorBrush(Color.Parse("#2C3E50"));
            }
            else if (remaining > 0)
            {
                statusText.Text = "Ви майже досягли денної норми";
                statusText.Foreground = new SolidColorBrush(Color.Parse("#2C3E50"));
            }
            else if (remaining == 0)
            {
                statusText.Text = "Ви точно досягли денної норми калорій!";
                statusText.Foreground = new SolidColorBrush(Color.Parse("#27AE60"));
            }
            else
            {
                statusText.Text = "Денну норму перевищено";
                statusText.Foreground = new SolidColorBrush(Color.Parse("#E74C3C"));
            }
        }
    }
}