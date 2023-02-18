namespace Lecture0;

public sealed class Add : BinaryOperator
{
    public Add(INode left, INode right) : base(left, right)
    {
    }

    public override string ToString()
    {
        return $"{Left} + {Right}";
    }
}