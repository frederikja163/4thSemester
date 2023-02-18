namespace Lecture0;

public class Program
{
    public delegate T4 Func<T1, T2, T3, T4>(T1 a, T2 b, T3 c);

    public delegate void Action<T1, T2, T3, T4>(T1 a, T2 b, T3 c, T4 d);

    // Example 1.
    public int MyProperty { get; set; }

    // Example 2.
    private int _myProperty1;
    public int MyProperty1
    {
        get
        {
            return _myProperty1;
        }
        set
        {
            _myProperty1 = value;
        }
    }

    // Example 3.
    private int _myProperty2;

    public int GetMyProperty2()
    {
        return _myProperty2;
    }

    public void SetMyProperty2(int value)
    {
        _myProperty2 = value;
    }

    public void Main()
    {
        // Example 1.
        MyProperty++;
        
        // Example 2.
        MyProperty1++;
        
        // Example 3.
        SetMyProperty2(GetMyProperty2() + 1);
    }
}