using System.Windows.Media.Imaging;

namespace ProcessKiller;

public class ProcessInfo
{
    public int Id { get; set; }
    public string ProcessName { get; set; }
    public BitmapSource Icon { get; set; }
}