using System.Windows;
using ProcessKiller.ViewModels;

namespace ProcessKiller
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}