using Avalonia.Controls;

namespace CalorieCalculatorApp;

public partial class MainWindow : Window
{
    private ContentControl? _mainContent;
    private TextBlock? _statusBar;

    public MainWindow()
    {
        InitializeComponent();
        _mainContent = this.FindControl<ContentControl>("MainContent");
        _statusBar = this.FindControl<TextBlock>("StatusBar");

        if (_mainContent != null)
            _mainContent.Content = new HomePage();
    }

    private void BtnHome_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_mainContent != null) _mainContent.Content = new HomePage();
        if (_statusBar != null) _statusBar.Text = "Розділ: Головна";
    }

    private void BtnJournal_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_mainContent != null) _mainContent.Content = new JournalPage();
        if (_statusBar != null) _statusBar.Text = "Розділ: Журнал харчування";
    }

    private void BtnFood_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_mainContent != null) _mainContent.Content = new FoodPage();
        if (_statusBar != null) _statusBar.Text = "Розділ: База продуктів";
    }

    private void BtnSettings_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_mainContent != null) _mainContent.Content = new SettingsPage();
        if (_statusBar != null) _statusBar.Text = "Розділ: Налаштування";
    }
}