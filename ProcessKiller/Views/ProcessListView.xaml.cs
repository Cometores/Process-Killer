using System.Windows.Controls;
using ProcessKiller.ViewModels;

namespace ProcessKiller.Views
{
    public partial class ProcessListView : UserControl
    {
        public ProcessListView()
        {
            InitializeComponent();
            DataContext = new ProcessListViewModel();
        }
    }
}