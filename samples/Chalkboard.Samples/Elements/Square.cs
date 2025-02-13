using Chalkboard;

namespace Playground;

public class Square : Element<AppStore>
{
    private const int Size = 20;

    public override RenderedRect Render(Size size)
    {
        var width = Math.Min(Size, size.Width);
        var height = Math.Min(Size, size.Height);
        var availableSize = new Size(width, height);

        var colorScheme = ColorScheme.Default.UseBackground(Store.Color);
        using (colorScheme.CreateScope())
        {
            return new RenderedRect(availableSize).Fill(' ');
        }
    }
}