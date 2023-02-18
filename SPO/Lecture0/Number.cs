namespace Lecture0;

public sealed class Number : Atom
{
    public int Value { get; }

    public Number(int value)
    {
        Value = value;
    }

    public override string ToString()
    {
        return Value.ToString();
    }
}