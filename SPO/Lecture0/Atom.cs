using System.Collections;
using System.Collections.Generic;

namespace Lecture0;

public abstract class Atom : INode
{
    public IEnumerator<INode> GetEnumerator()
    {
        yield break;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}