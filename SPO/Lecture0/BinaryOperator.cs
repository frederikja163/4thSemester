using System.Collections;
using System.Collections.Generic;

namespace Lecture0;

public abstract class BinaryOperator : INode{
    public INode Left { get; }
    public INode Right { get; }
    
    public BinaryOperator(INode left, INode right)
    {
        Left = left;
        Right = right;
    }
    
    public IEnumerator<INode> GetEnumerator()
    {
        yield return Left;
        yield return Right;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}