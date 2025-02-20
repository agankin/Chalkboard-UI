using Chalkboard;
using Chalkboard.Samples;

ColorScheme.SetDefault(Color.White, Color.Black);

var (width, height) = (Console.WindowWidth, Console.WindowHeight);
var renderer = new WindowsConsoleRenderer(width, height);

var (fieldWidth, fieldHeight) = (width - 2, height - 2);
var (headLeft, headTop) = (fieldWidth / 2, fieldHeight / 2);

var store = new AppStore
{
    FieldSize = new(fieldWidth, fieldHeight),
    Snake = new SnakeModel(new(headLeft, headTop), p => p.TranslateTop(1), 5),
    Direction = SnakeDirection.Up
};

var root = new Border { Content = new Field() };

var ui = new ChalkboardUI<AppStore>(root, renderer, store);
var messageReducer = ui.AddStoreReducer<AppMessage>(AppStoreReducers.OnMessage);

Console.CursorVisible = false;
ui.Render();

var messageLoop = new AppMessageLoop();

using var appTimer = AppTimer.Start(messageLoop.Add, 100);
using var keyboardListener = KeyboardListener.Start(messageLoop.Add);

messageLoop.OnMessage(messageReducer);