using Avalonia.Controls;

namespace CalorieCalculatorApp;

public partial class MainWindow : Window
{
    private ContentControl? _mainContent;

    public MainWindow()
    {
        InitializeComponent();
        _mainContent = this.FindControl<ContentControl>("MainContent");

        if (_mainContent != null)
            _mainContent.Content = new HomePage();
    }

    private void BtnHome_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_mainContent != null) _mainContent.Content = new HomePage();
    }

    private void BtnJournal_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_mainContent != null) _mainContent.Content = new JournalPage();
    }

    private void BtnFood_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_mainContent != null) _mainContent.Content = new FoodPage();
    }

    private void BtnSettings_Click(object? sender, Avalonia.Interactivity.RoutedEventArgs e)
    {
        if (_mainContent != null) _mainContent.Content = new SettingsPage();
    }
}
