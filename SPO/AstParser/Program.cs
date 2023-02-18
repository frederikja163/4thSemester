using AstParser;

public static class Program
{
    public static void Main(string[] args)
    {
        
        Parser<AcTokens> acParser = new Parser<AcTokens>()
        {
            { AcTokens.Blank, "^[ \r\n\t]" },
            { AcTokens.FNum, "^[0-9]+.[0-9]" },
            { AcTokens.Inum, "^[0-9]+"},
            { AcTokens.Minus, "^-" },
            { AcTokens.Plus, "^\\+" },
            { AcTokens.Assign, "^=" },
            { AcTokens.Id, "^[a-e]|[g-h]|[j-o][q-z]"},
            { AcTokens.Print, "^p"},
            { AcTokens.Intdcl, "^i"},
            { AcTokens.Floatdcl, "^f"},
        };
        acParser.Ignore(AcTokens.Blank);
        string str = File.ReadAllText("Program.ac");
        var arr = acParser.Tokenize(str).ToArray();

        Console.WriteLine(string.Join(", ", arr));

        Prog prog = acParser.Parse<Prog>(str);
    }
}

public enum AcTokens
{
    Floatdcl,
    Intdcl,
    Print,
    Id,
    Assign,
    Plus,
    Minus,
    Inum,
    FNum,
    Blank,
}

[AstNode(0, typeof(Dcls), typeof(Stmts))]
public class Prog : AstNode<AcTokens>
{
    
}


[AstNode(0, typeof(Dcl), typeof(Dcls))]
[AstNode(100)]
public class Dcls : AstNode<AcTokens>
{
    
}

[AstNode(0, AcTokens.Floatdcl, AcTokens.Id)]
[AstNode(-1, AcTokens.Intdcl, AcTokens.Id)]
public class Dcl : AstNode<AcTokens>
{
    
}

[AstNode(0, typeof(Stmt), typeof(Stmts))]
[AstNode(100)]
public class Stmts : AstNode<AcTokens>
{
    
}

[AstNode(0, AcTokens.Id, AcTokens.Assign, typeof(Val), typeof(Expr))]
[AstNode(-1, AcTokens.Print, AcTokens.Id)]
public class Stmt : AstNode<AcTokens>
{
    
}

[AstNode(0, AcTokens.Plus, typeof(Val), typeof(Expr))]
[AstNode(-1, AcTokens.Minus, typeof(Val), typeof(Expr))]
[AstNode(100)]
public class Expr : AstNode<AcTokens>
{
    
}

[AstNode(0, AcTokens.Id)]
[AstNode(-1, AcTokens.Inum)]
[AstNode(-2, AcTokens.FNum)]
public class Val : AstNode<AcTokens>
{
    
}