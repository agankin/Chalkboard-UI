using Chalkboard;
using Playground;

var renderer = new ConsoleRenderer(new Size(80, 80));
var storeValue = new SquareStore(rows: 4, cols: 6);

var ui = new ChalkboardUI<SquareStore>(store => new Grid(store), renderer, storeValue);
ui.Render();

Console.CursorVisible = false;

var rand = new Random();
var colors = Enum.GetValues(typeof(Color)).Cast<Color>().Except(new[] { Color.Black }).ToArray();

for (var i = 0; i < 600; i++)
{
    Thread.Sleep(100);

    var store = ui.Store;

    var left = rand.Next(store.Value.Cols);
    var top = rand.Next(store.Value.Rows);
    var colorIdx = rand.Next(colors.Length);
    
    var color = colors[colorIdx];
    store.Update(value => value.SetBackground(left, top, color));
}

Console.ReadKey(true);