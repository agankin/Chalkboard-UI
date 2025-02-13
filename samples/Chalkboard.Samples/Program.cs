using Chalkboard;
using Playground;

ColorScheme.SetDefault(Color.White, Color.DarkBlue);

var renderer = new WindowsConsoleRenderer(Console.WindowWidth, Console.WindowHeight);

var root = new Border
{
    Content = new Square()
};

var ui = new ChalkboardUI<AppStore>(root, renderer, new AppStore());
var tick = ui.AddStoreReducer((AppStore store, int _) =>
{
    return new AppStore
    {
        Color = (Color)new Random().Next(16)
    };
});

Console.CursorVisible = false;
ui.Render();

var i = 0;
while (i++ < 1000)
{
    Thread.Sleep(1000);
    if (Console.KeyAvailable)
        return;

    tick(i);
}