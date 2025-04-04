using System.Windows;
using ProcessKiller.ViewModels;

namespace ProcessKiller;


public partial class App : Application
{
    private void Application_Startup(object sender, StartupEventArgs e)
    {
        MainWindow mainWindow = new MainWindow
        {
            DataContext = new MainViewModel()
        };
        mainWindow.Show();
    }
}