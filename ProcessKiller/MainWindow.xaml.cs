using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace ProcessKiller;

public partial class MainWindow : Window
{
    private bool _isDarkTheme = false;
    private ResourceDictionary _currentTheme = Application.Current.Resources.MergedDictionaries
        .FirstOrDefault(d => d.Source != null && 
                             (d.Source.OriginalString.Contains("DarkColors.xaml") || 
                              d.Source.OriginalString.Contains("Colors.xaml")));
    
    public MainWindow()
    {
        InitializeComponent();
    }

    private void Refresh_Click(object sender, RoutedEventArgs e)
    {
        processListView.RefreshProcesses();
    }

    private void KillProcess_Click(object sender, RoutedEventArgs e)
    {
        processListView.KillSelectedProcess();
    }
    
    private void SwitchTheme_Click(object sender, RoutedEventArgs e)
    {
        if (_isDarkTheme)
        {
            SwitchTheme(true);
            _isDarkTheme = false;
        }
        else
        {
            SwitchTheme(false);
            _isDarkTheme = true;
        }
    }
    
    private void SwitchTheme(bool isDark)
    {
        string themePath = isDark ? "Themes/Colors.xaml" : "Themes/DarkColors.xaml";

        var newTheme = new ResourceDictionary { Source = new Uri(themePath, UriKind.Relative) };

        if (_currentTheme != null)
        {
            int index = Application.Current.Resources.MergedDictionaries.IndexOf(_currentTheme);
            if (index != -1)
            {
                Application.Current.Resources.MergedDictionaries[index] = newTheme;
            }
            else
            {
                Application.Current.Resources.MergedDictionaries.Add(newTheme);
            }
        }
        else
        {
            Application.Current.Resources.MergedDictionaries.Add(newTheme);
        }

        _currentTheme = newTheme; // Запоминаем текущую тему
    }
}