using Chalkboard;
using Playgrournd;

var renderingSize = new Size(80, 80);
var console = new ConsoleRenderer(renderingSize);
var ui = new ChalkboardUI(console, renderingSize);

var squares = new Square[,]
{
    { new(), new(), new(), new() },
    { new(), new(), new(), new() },
    { new(), new(), new(), new() },
    { new(), new(), new(), new() },
    { new(), new(), new(), new() },
    { new(), new(), new(), new() }
};

var root = new Grid(squares);
ui.Render(root);

Console.CursorVisible = false;

var rand = new Random();
var colors = Enum.GetValues(typeof(Color)).Cast<Color>().Except(new[] { Color.Black }).ToArray();

for (var i = 0; i < 600; i++)
{
    Thread.Sleep(100);

    var leftIdx = rand.Next(squares.GetWidth());
    var topIdx = rand.Next(squares.GetHeight());
    var colorIdx = rand.Next(colors.Length);
    
    var color = colors[colorIdx];
    var square = squares[leftIdx, topIdx];
    square.Background = color;
}

Console.ReadKey(true);