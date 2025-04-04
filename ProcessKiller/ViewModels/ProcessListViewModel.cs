using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using ProcessKiller.Models;

namespace ProcessKiller.ViewModels
{
    public class ProcessListViewModel : BaseViewModel
    {
        public ObservableCollection<ProcessModel> Processes { get; private set; }

        public ICommand KillProcessCommand { get; }
        public ICommand OpenFileLocationCommand { get; }
        public ICommand RefreshCommand { get; }

        public Action<string>? ShowErrorMessage { get; set; }

        private ProcessModel _selectedProcess;
        public ProcessModel SelectedProcess
        {
            get => _selectedProcess;
            set
            {
                _selectedProcess = value;
                OnPropertyChanged(nameof(SelectedProcess));
            }
        }

        public ProcessListViewModel()
        {
            Processes = new ObservableCollection<ProcessModel>();
            LoadProcesses();

            KillProcessCommand = new RelayCommand(KillProcess, CanExecuteProcessCommand);
            OpenFileLocationCommand = new RelayCommand(OpenFileLocation, CanExecuteProcessCommand);
            RefreshCommand = new RelayCommand(_ => LoadProcesses());
        }

        private void LoadProcesses()
        {
            Processes.Clear();
            foreach (var process in Process.GetProcesses().OrderBy(p => p.ProcessName))
            {
                try
                {
                    string filePath = process.MainModule?.FileName;
                    BitmapSource icon = filePath != null ? IconHelper.GetIcon(filePath) : null;

                    Processes.Add(new ProcessModel
                    {
                        Id = process.Id,
                        ProcessName = process.ProcessName,
                        Icon = icon,
                        FilePath = filePath
                    });
                }
                catch { }
            }
            OnPropertyChanged(nameof(Processes)); // Обновляем UI
        }

        private void KillProcess(object obj)
        {
            if (obj is ProcessModel processModel)
            {
                try
                {
                    Process.GetProcessById(processModel.Id)?.Kill();
                    LoadProcesses();
                }
                catch (Exception ex)
                {
                    ShowErrorMessage?.Invoke($"Ошибка при завершении процесса: {ex.Message}");
                }
            }
        }

        private void OpenFileLocation(object obj)
        {
            if (obj is ProcessModel processModel && !string.IsNullOrEmpty(processModel.FilePath) && File.Exists(processModel.FilePath))
            {
                Process.Start("explorer.exe", $"/select,\"{processModel.FilePath}\"");
            }
        }

        private bool CanExecuteProcessCommand(object obj) => obj is ProcessModel;
    }
}
