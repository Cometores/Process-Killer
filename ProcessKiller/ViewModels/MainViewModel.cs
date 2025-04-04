using System.Windows;
using System.Windows.Input;

namespace ProcessKiller.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private bool _isDarkTheme = false;
        private ResourceDictionary _currentTheme;
        public ProcessListViewModel ProcessList { get; }
        public ICommand SwitchThemeCommand { get; }

        public MainViewModel()
        {
            ProcessList = new ProcessListViewModel();
            SwitchThemeCommand = new RelayCommand(SwitchTheme);

            // Устанавливаем текущую тему
            _currentTheme = Application.Current.Resources.MergedDictionaries
                .FirstOrDefault(d => d.Source != null &&
                                     (d.Source.OriginalString.Contains("DarkColors.xaml") ||
                                      d.Source.OriginalString.Contains("Colors.xaml")));
        }

        // Метод для переключения темы
        private void SwitchTheme(object obj)
        {
            _isDarkTheme = !_isDarkTheme;
            string themePath = _isDarkTheme ? "Themes/DarkColors.xaml" : "Themes/Colors.xaml";

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

            _currentTheme = newTheme;
        }
    }
}
