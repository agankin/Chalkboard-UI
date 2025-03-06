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

var tickReducer = ui.AddMessage<Nothing>(AppStoreReducers.OnTick);
var keyPressedReducer = ui.AddMessage<ConsoleKeyInfo>(AppStoreReducers.OnKeyPressed);

Console.CursorVisible = false;

using var timerObservable = TimerObservable.Start(100);
var timerObserver = new DispatchObserver<Nothing>().OnNext(tickReducer);
using var timerSubscription = timerObservable.Subscribe(timerObserver);

using var keyPressedObservable = KeyPressedObservable.Start();
var keyPressedObserver = new DispatchObserver<ConsoleKeyInfo>().OnNext(keyPressedReducer);
using var keyPressedSubscription = keyPressedObservable.Subscribe(keyPressedObserver);

ui.EnterMessageLoop();