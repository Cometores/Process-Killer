using System.IO;
using System.Windows;
using System.Windows.Media.Imaging;

namespace ProcessKiller;

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