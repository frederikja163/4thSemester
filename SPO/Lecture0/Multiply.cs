namespace Lecture0;

public sealed class Multiply : BinaryOperator
{
    public Multiply(INode left, INode right) : base(left, right)
    {
    }

    public override string ToString()
    {
        return $"{Left} * {Right}";
    }
}