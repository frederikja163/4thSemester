namespace Lecture0;

public sealed class Variable : Atom
{
    public char Name { get; }

    public Variable(char name)
    {
        Name = name;
    }

    public override string ToString()
    {
        return Name.ToString();
    }
}