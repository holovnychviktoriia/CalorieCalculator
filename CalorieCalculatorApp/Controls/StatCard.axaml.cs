using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CalorieCalculatorApp.Controls;

public partial class StatCard : UserControl
{
    public static readonly StyledProperty<string> LabelProperty =
        AvaloniaProperty.Register<StatCard, string>(nameof(Label), "");

    public static readonly StyledProperty<string> ValueProperty =
        AvaloniaProperty.Register<StatCard, string>(nameof(Value), "");

    public string Label
    {
        get => GetValue(LabelProperty);
        set => SetValue(LabelProperty, value);
    }

    public string Value
    {
        get => GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public StatCard()
    {
        AvaloniaXamlLoader.Load(this);
    }

    protected override void OnPropertyChanged(AvaloniaPropertyChangedEventArgs change)
    {
        base.OnPropertyChanged(change);

        if (change.Property == LabelProperty)
        {
            var labelText = this.FindControl<TextBlock>("LabelText");
            if (labelText != null) labelText.Text = Label;
        }

        if (change.Property == ValueProperty)
        {
            var valueText = this.FindControl<TextBlock>("ValueText");
            if (valueText != null) valueText.Text = Value;
        }
    }
}
