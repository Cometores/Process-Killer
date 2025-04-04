using System.Diagnostics;
using System.Windows.Media.Imaging;

namespace ProcessKiller.Models;

public class ProcessModel
{
    public string ProcessName { get; set; }
    public int Id { get; set; }
    public string FilePath { get; set; }
    public BitmapSource Icon { get; set; }
}