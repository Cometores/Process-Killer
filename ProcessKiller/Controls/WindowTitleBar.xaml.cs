using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ProcessKiller.Controls;

public partial class WindowTitleBar : UserControl
{
    public WindowTitleBar()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Event <c>Border_MouseDown</c> allows to drag and drop the window.
    /// </summary>
    private void Border_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
            Application.Current.MainWindow!.DragMove();
    }

    private void MinimizeButton_Click(object sender, RoutedEventArgs e) =>
        Application.Current.MainWindow!.WindowState = WindowState.Minimized;

    private void MaximizeButton_Click(object sender, RoutedEventArgs e)
    {
        Application.Current.MainWindow!.WindowState =
            Application.Current.MainWindow.WindowState != WindowState.Maximized
                ? WindowState.Maximized
                : WindowState.Normal;
    }

    private void CloseButton_Click(object sender, RoutedEventArgs e) =>
        Application.Current.Shutdown();
}