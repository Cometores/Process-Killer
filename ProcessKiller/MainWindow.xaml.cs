using System.Diagnostics;
using System.Drawing;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ProcessKiller
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadProcesses();
        }

        private void LoadProcesses()
        {
            var processes = Process.GetProcesses().OrderBy(p => p.ProcessName)
                .Select(p => new ProcessInfo
                {
                    Id = p.Id,
                    ProcessName = p.ProcessName,
                    Icon = GetProcessIcon(p)
                }).ToList();
            ProcessList.ItemsSource = processes;
        }

        private BitmapSource GetProcessIcon(Process process)
        {
            try
            {
                using (Icon icon = System.Drawing.Icon.ExtractAssociatedIcon(process.MainModule.FileName))
                {
                    return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                        icon.Handle, System.Windows.Int32Rect.Empty,
                        BitmapSizeOptions.FromEmptyOptions());
                }
            }
            catch
            {
                return null;
            }
        }

        private void KillProcess_Click(object sender, RoutedEventArgs e)
        {
            if (ProcessList.SelectedItem is ProcessInfo selectedProcess)
            {
                try
                {
                    var process = Process.GetProcessById(selectedProcess.Id);
                    process.Kill();
                    process.WaitForExit();
                    LoadProcesses(); // Обновляем список процессов
                }
                catch (ArgumentException)
                {
                    // Процесс уже завершен, можно просто обновить список
                    LoadProcesses();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void Refresh_Click(object sender, RoutedEventArgs e)
        {
            LoadProcesses();
        }
    }
}