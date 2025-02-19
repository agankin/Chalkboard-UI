using Chalkboard;
using Chalkboard.Samples;

ColorScheme.SetDefault(Color.White, Color.DarkBlue);

var (width, height) = (Console.WindowWidth, Console.WindowHeight);
var renderer = new WindowsConsoleRenderer(width, height);

var (headLeft, headTop) = (width / 2, height / 2);

var store = new AppStore
{
    FieldSize = new(width, height),
    Snake = new SnakeModel(
        new(headLeft, headTop),
        new(headLeft, headTop + 1),
        new(headLeft, headTop + 2),
        new(headLeft, headTop + 3),
        new(headLeft, headTop + 4),
        new(headLeft, headTop + 5)
    ),
    Direction = SnakeDirection.Up
};

var root = new Border
{
    Content = new Field()
};

var ui = new ChalkboardUI<AppStore>(root, renderer, store);
var tick = ui.AddStoreReducer<Nothing>(AppStoreReducers.OnTick);

Console.CursorVisible = false;
ui.Render();

while (true)
{
    Thread.Sleep(1000);
    if (Console.KeyAvailable)
        return;

    tick(new());
}