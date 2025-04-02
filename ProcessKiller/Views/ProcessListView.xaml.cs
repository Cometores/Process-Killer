using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Media.Imaging;
using System.IO;

namespace ProcessKiller.Views;

public partial class ProcessListView : UserControl
{
    public ObservableCollection<ProcessModel> Processes { get; set; } = new();

    public ProcessListView()
    {
        InitializeComponent();
        this.DataContext = this; // Устанавливаем контекст данных
        LoadProcesses();
    }

    public void LoadProcesses()
    {
        Processes.Clear();
        foreach (var process in Process.GetProcesses())
        {
            try
            {
                string filePath = process.MainModule?.FileName;
                BitmapSource icon = filePath != null ? IconHelper.GetIcon(filePath) : null;
                    
                Processes.Add(new ProcessModel
                {
                    Id = process.Id,
                    ProcessName = process.ProcessName,
                    Icon = icon
                });
            }
            catch { } // Игнорируем недоступные процессы
        }
    }
    
    public void RefreshProcesses()
    {
        LoadProcesses();
    }

    public void KillSelectedProcess()
    {
        if (ProcessList.SelectedItem is ProcessModel selectedProcess)
        {
            try
            {
                Process.GetProcessById(selectedProcess.Id).Kill();
                LoadProcesses(); // Обновляем список после удаления
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при завершении процесса: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}

public class ProcessModel
{
    public int Id { get; set; }
    public string ProcessName { get; set; }
    public BitmapSource Icon { get; set; }
}

public static class IconHelper
{
    public static BitmapSource GetIcon(string filePath)
    {
        if (!File.Exists(filePath)) return null;
        try
        {
            using var icon = System.Drawing.Icon.ExtractAssociatedIcon(filePath);
            return System.Windows.Interop.Imaging.CreateBitmapSourceFromHIcon(
                icon.Handle,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());
        }
        catch
        {
            return null;
        }
    }
}