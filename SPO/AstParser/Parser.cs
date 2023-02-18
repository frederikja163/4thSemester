using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.RegularExpressions;

namespace AstParser;

public record TokenDefinition<T>(T Token, Regex Regex)
    where T : Enum
{
    public TokenDefinition(T token, string regex) : this(token, new Regex(regex)) {
    }
}

public sealed class Parser<T> : IEnumerable<TokenDefinition<T>>
    where T : Enum
{
    private readonly Dictionary<Type, List<AstNodeAttribute>> _typeToAttributes;
    private readonly List<TokenDefinition<T>> _tokens;
    private readonly HashSet<T> _ignoreTokens = new HashSet<T>();

    public Parser(TokenDefinition<T>[] tokens)
    {
        _tokens = tokens.ToList();
        
        Dictionary<AstNodeAttribute, List<Type>> attributeToTypes = GetNodeDefinitions();
        _typeToAttributes = attributeToTypes.Reverse();
    }

    public Parser()
    {
        _tokens = new List<TokenDefinition<T>>();
        Dictionary<AstNodeAttribute, List<Type>> attributeToTypes = GetNodeDefinitions();
        _typeToAttributes = attributeToTypes.Reverse();
    }

    private static Dictionary<AstNodeAttribute, List<Type>> GetNodeDefinitions()
    {
        Dictionary<AstNodeAttribute, List<Type>> definitions = new Dictionary<AstNodeAttribute, List<Type>>();
        foreach (Type type in Assembly.GetExecutingAssembly()
                     .GetTypes()
                     .Where(t => t.IsSubclassOf(typeof(AstNode<T>))))
        {
            foreach(Attribute attribute in type.GetCustomAttributes(typeof(AstNodeAttribute)))
            {
                AstNodeAttribute attr = (AstNodeAttribute)attribute;
                if (!definitions.TryGetValue(attr, out List<Type>? nodeTypes))
                {
                    nodeTypes = new List<Type>();
                    definitions.Add(attr, nodeTypes);
                }
                nodeTypes.Add(type);
            }
        }

        return definitions;
    }

    public void Add(T token, string regex)
    {
        _tokens.Add(new TokenDefinition<T>(token, new Regex(regex)));
    }
    
    public void Add(T token, Regex regex)
    {
        _tokens.Add(new TokenDefinition<T>(token, regex));
    }
    
    public void Add(TokenDefinition<T> token)
    {
        _tokens.Add(token);
    }

    public void Ignore(T token)
    {
        _ignoreTokens.Add(token);
    }

    public IEnumerable<T> Tokenize(string str)
    {
        while (!string.IsNullOrWhiteSpace(str))
        {
            bool recognised = false;
            foreach (TokenDefinition<T> token in _tokens)
            {
                if (token.Regex.IsMatch(str))
                {
                    if (!_ignoreTokens.Contains(token.Token))
                    {
                        yield return token.Token;
                    }
                    str = token.Regex.Replace(str, "");
                    recognised = true;
                    break;
                }
            }

            if (!recognised)
                throw new ArgumentException($"Unrecognized token at {str[..10]}", nameof(str));
        }
    }

    public TRoot Parse<TRoot>(string str)
    {
        T[] tokens = Tokenize(str).ToArray();
        Type rootType = typeof(TRoot);

        if (TryParse(rootType, tokens, 0, out object? root, out int _))
        {
            return (TRoot)root;
        }

        throw new Exception("Parse failed");
    }

    private bool TryParse(Type rootType, T[] tokens, int tokenIndex, [NotNullWhen(true)] out object? root, out int usedTokens)
    {
        if (tokenIndex >= tokens.Length)
        {
            usedTokens = 0;
            root = null;
            return false;
        }
        
        List<AstNodeAttribute> possibleNodes = _typeToAttributes[rootType];
        foreach (AstNodeAttribute node in possibleNodes.OrderBy(a => a.Order))
        {
            List<AstNode<T>> children = new List<AstNode<T>>();
            List<T> childTokens = new List<T>();
            usedTokens = 0;
            for (int i = 0; i < node.Parts.Length; i++)
            {
                AstPart part = node.Parts[i];
                if (part.Token?.Equals(tokens[tokenIndex + usedTokens]) ?? false)
                {
                    childTokens.Add(tokens[tokenIndex + usedTokens]);
                    usedTokens++;
                    continue;
                }
                
                if (part.Type is not null)
                {
                    if (TryParse(part.Type, tokens, tokenIndex + usedTokens, out object? childNode, out int used))
                    {
                        usedTokens += used;
                        children.Add((AstNode<T>)childNode);
                        continue;
                    }
                }

                usedTokens = -1;
                break;
            }

            if (usedTokens != -1)
            {
                root = Activator.CreateInstance(rootType);
                if (root is not null)
                {
                    ((AstNode<T>)root).SetChildren(children.ToArray(), childTokens.ToArray());
                    return true;
                }
            }
        }

        root = null;
        usedTokens = 0;
        return false;
    }

    IEnumerator<TokenDefinition<T>> IEnumerable<TokenDefinition<T>>.GetEnumerator()
    {
        return _tokens.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_tokens).GetEnumerator();
    }
}