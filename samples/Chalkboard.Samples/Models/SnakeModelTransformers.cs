namespace Chalkboard.Samples;

public static class SnakeModelTransformers
{
    public static SnakeModel MoveUp(SnakeModel model) => model.Move(head => head.TranslateTop(-1));

    public static SnakeModel MoveDown(SnakeModel model) => model.Move(head => head.TranslateTop(1));

    public static SnakeModel MoveLeft(SnakeModel model) => model.Move(head => head.TranslateLeft(-1));

    public static SnakeModel MoveRight(SnakeModel model) => model.Move(head => head.TranslateLeft(1));
}