namespace Chalkboard;

public abstract class Element
{
    private Option<Size> _size = Option.None<Size>();
    private Option<Margin> _margin = Option.None<Margin>();
    private Option<Padding> _padding = Option.None<Padding>();

    protected Element()
    {        
        Children = new ChildCollection(this);
        Children.ElementsChanged += (_, _) => RaiseRenderRequired();
        Children.ChildRenderRequired += (_, childArgs) => RenderRequired?.Invoke(this, childArgs);
    }

    internal event EventHandler<RenderRequiredEventArgs>? RenderRequired;

    public Element? Parent { get; internal set; }

    public ChildCollection Children { get; }

    public Option<Size> Size
    {
        get => _size;
        set => SetRenderableProperty(ref _size, value);
    }
    
    public Option<Margin> Margin
    {
        get => _margin;
        set => SetRenderableProperty(ref _margin, value);
    }

    public Option<Padding> Padding
    {
        get => _padding;
        set => SetRenderableProperty(ref _padding, value);
    }

    public void Render(RenderingRect rect) => RenderTo(rect);

    protected abstract void RenderTo(RenderingRect rect);

    protected void SetRenderableProperty<TProperty>(ref TProperty prop, TProperty value)
    {
        var comparer = EqualityComparer<TProperty>.Default;
        if (!comparer.Equals(prop, value))
        {
            prop = value;
            RaiseRenderRequired();
        }
    }

    private void RaiseRenderRequired()
    {
        var eventArgs = new RenderRequiredEventArgs(this);
        RenderRequired?.Invoke(this, eventArgs);
    }
}