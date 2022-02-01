using System.Drawing;
using Olive;

OliveEngine.Initialize("Screen Resolution Example", 800, 600, DisplayMode.Windowed);
OliveEngine.Run();

Console.WriteLine("Supported resolutions: ");
foreach (Size resolution in Screen.GetSupportedResolutions())
{
    Console.WriteLine($"- {resolution.Width} x {resolution.Height}");
}
