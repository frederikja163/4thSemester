using System.Collections;
using System.Text.RegularExpressions;

namespace AstParser;

internal record AstPart(Type? Type, Enum? Token);

[AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
public class AstNodeAttribute : Attribute
{
    internal int Order { get; }
    internal AstPart[] Parts { get; }

    public AstNodeAttribute(int order = 0, params object[] objects)
    {
        Order = order;
        
        Parts = new AstPart[objects.Length];
        for (int i = 0; i < objects.Length; i++)
        {
            object obj = objects[i];
            Type type = obj.GetType();
            if (type.IsEnum)
            {
                Parts[i] = new AstPart(null, (Enum)obj);
            }
            else if (type.IsTypeDefinition)
            {
                Parts[i] = new AstPart((Type)obj, null);
            }
            else
            {
                throw new ArgumentException(
                    $"object at {i} is neither a type definition of an AstNode nor an enum value", nameof(objects));
            }
        }
    }
}

public abstract class AstNode<T> where T : Enum
{
    private AstNode<T>[] _children = Array.Empty<AstNode<T>>();
    private T[] _tokens = Array.Empty<T>();

    protected TNodeType GetChild<TNodeType>(int i)
        where TNodeType : AstNode<T>
    {
        return (TNodeType)_children[i];
    }

    internal void SetChildren(AstNode<T>[] children, T[] tokens)
    {
        _children = children;
        _tokens = tokens;
    }
}