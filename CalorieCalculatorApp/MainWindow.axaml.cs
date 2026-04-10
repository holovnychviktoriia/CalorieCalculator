using Avalonia.Controls;

namespace CalorieCalculatorApp;

public partial class MainWindow : Window
{
    private TextBlock? _contentArea;
    private TextBlock? _statusBar;

    public MainWindow()
    {
        InitializeComponent();
        _contentArea = this.FindControl<TextBlock>("ContentArea");
        _statusBar = this.FindControl<TextBlock>("StatusBar");
    }

    private void BtnHome_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_contentArea != null) _contentArea.Text = "Головна сторінка";
        if (_statusBar != null) _statusBar.Text = "Розділ: Головна";
    }

    private void BtnJournal_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_contentArea != null) _contentArea.Text = "Журнал харчування";
        if (_statusBar != null) _statusBar.Text = "Розділ: Журнал харчування";
    }

    private void BtnFood_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_contentArea != null) _contentArea.Text = "База продуктів";
        if (_statusBar != null) _statusBar.Text = "Розділ: База продуктів";
    }

    private void BtnSettings_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_contentArea != null) _contentArea.Text = "Налаштування";
        if (_statusBar != null) _statusBar.Text = "Розділ: Налаштування";
    }
}