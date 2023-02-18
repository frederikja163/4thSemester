using System;
using System.Diagnostics;
using Lecture0;
using Xunit;
using Xunit.Abstractions;

namespace TestProject1;

public class Lecture0
{
    private readonly ITestOutputHelper _testOutputHelper;
    public Lecture0(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    
    // Familiarize yourself with Java e.g. by following the links above.
    [Fact]
    public void Exercise1()
    {
        
    }
    
    // Write a Java program that implements a data structure for the following tree
    [Fact]
    public void Exercise2()
    {
        Number num = new Number(1);
        Variable a = new Variable('a');
        Variable n = new Variable('n');
        Add add = new Add(a, n);
        Multiply mul = new Multiply(add, num);
    }
    
    // Extend your Java program to traverse the tree depth-first and print out information in nodes and leaves as it goes along. (Possible solutions to exercise 2 and 3 can be found on
    // http://people.cs.aau.dk/~boegholm/spo/spo_01/ )
    [Fact]
    public void Exercise3()
    {
        Number num = new Number(1);
        Variable a = new Variable('a');
        Variable n = new Variable('n');
        Add add = new Add(a, n);
        Multiply mul = new Multiply(add, num);
        _testOutputHelper.WriteLine(mul.ToString());
    }
    
    // Write a Java program that can read the string "a + n * 1" and produce a collection of objects containing the individual symbols when blank spaces are ignored (or used as separator).
    [Fact]
    public void Exercise4()
    {
        string str = "a + n * 1";

        INode root = Parse(str.Replace(" ", ""));
        _testOutputHelper.WriteLine(root.ToString());
        Assert.Equal(str, root.ToString());

        INode Parse(string str)
        {
            int mulIndex = str.IndexOf('*');
            if (mulIndex != -1)
            {
                string leftStr = str[..mulIndex];
                string rightStr = str[(mulIndex + 1)..];
                INode left = Parse(leftStr);
                INode right = Parse(rightStr);
                return new Multiply(left, right);
            }

            int addIndex = str.IndexOf('+');
            if (addIndex != -1)
            {
                string leftStr = str[..(addIndex)];
                string rightStr = str[(addIndex + 1)..];
                INode left = Parse(leftStr);
                INode right = Parse(rightStr);
                return new Add(left, right);
            }

            if (int.TryParse(str, out int val))
            {
                return new Number(val);
            }

            return new Variable(str[0]);
        }
    }
    
    // Make a list of all the computer languages you know. (Remember to include command, scripting and text processing languages)
    [Fact]
    public void Exercise5()
    {
        // C#
        // C
        // C++
        // JS
        // Lua
        // Typescript
        // Python
        // Shell script
        // Bash
        // PHP
        // HTML
        // CSS
        // MD
        // LaTEX
        // XML
        // JSon
        // Minecraft command blocks
        // GLSL
        // Scratch
        // F#
        
        // Ruby
        // Pearl
        // Haskell
        // Go
        // Fortran
        // Lisp
        // Smalltalk
        // X86
        // ARM
        // X86-64
        // RISC-V
        // HLSL
    }
    
    // How well do you know the above languages?
    [Fact]
    public void Exercise6()
    {
        // I know them all to different levels.
    }
    
    
    // Categorize them according to the following:
    // Daily use
    // Often
    // Written a few programs
    // Looked at it once or twice
    [Fact]
    public void Exercise7()
    {
        // --Daily use--
        // C#
        // XML
        // JSon
        // MD
        
        // --Often--
        // GLSL
        // Typescript
        
        // --Written a few programs--
        // C
        // C++
        // Minecraft command blocks
        // HTML
        // CSS
        // JS
        // Lua
        // F#
        // Scratch
        
        // --Looked at it once or twice--
        // Shell script
        // Bash
        // PHP
        // LaTEX
        // Python
        
        // The rest i don't have any personal experience with.
    }
    
    // Which language was your first programming language?
    [Fact]
    public void Exercise8()
    {
        // Technically C# although i would say either Scratch or Cpp as those are the ones where i learned to program.
    }

    // Use the statements associated with the Hammer Principle to evaluate each of the programming languages on your list http://web.archive.org/web/20170709000745/http://www.hammerprinciple.com:80/therighttool
    [Fact]
    public void Exercise9()
    {
        // For this exercise i will only consider the programming languages i have written in during the past year.
        // C#, TS, JS, GLSL, C, CPP, Lua, PHP
        /*
            C#, TS - I know this language well
            C#, GLSL, TS, CPP, C - I enjoy using this language
            C#, GLSL, I regularly use this language
            C#, CPP, C - I find it easy to write efficient code in this language
            C#, GLSL, TS - I find this language easy to prototype in
            C# - When I write code in this language I can be very sure it is correct
            C# - I rarely have difficulty abstracting patterns I find in my code
            C# - It's unusual for me to discover unfamiliar features
            C# - I usually use this language on solo projects
            C#, TS - I usually use this language on projects with many other members
            TS - I would use this language for a web project
            C# - I would use this language for a desktop GUI project
            I would use this language for mobile applications
            C# - I would use this language for casual scripting
            C# - It is easy to tell at a glance what code in this language does
            C# - I find code written in this language very elegant
            C#, TS, C, CPP, Lua - Code written in this language tends to be terse
            C#, TS, C, CPP, Lua - Code written in this language tends to be verbose
            JS, Lua, PHP - There is a lot of accidental complexity when writing code in this language
            C# - This language allows me to write programs where I know exactly what they are doing under the hood
            C#, TS, JS, Lua, PHP - This is a high level language
            GLSL, C, CPP - This is a low level language
            C#, GLSL, TS, C, CPP - This language has a strong static type system
            C#, TS, JS, C, CPP, Lua - This language is very flexible
            GLSL, PHP - This language has a very rigid idea of how things should be done
            GLSL, Lua - This language has a niche in which it is great
            GLSL, Lua, PHP - This language has a niche outside of which I would not use it
            C#, TS, C, CPP - I would use this language to write a command-line app
            C#, C, CPP - This language is good for scientific computing
            C#, TS, C, CPP, Lua - This language is expressive
            JS, C, CPP, Lua, PHP - Writing code in this language is a lot of work
            C#, GLSL, CPP, TS - I can imagine using this language in my day job
            C# - This language excels at symbolic manipulation
            C# - This language excels at text processing
            C# - This language excels at concurrency
            C#, GLSL - This language is well documented
            C#, GLSL - The resources for learning this language are of high quality
            C#, TS, GLSL, CPP - This language has a good community
            C#, TS - Third-party libraries are readily available, well-documented, and of high quality
            C#, TS, C, CPP - There is a wide variety of open source code written in this language
            C#, C, CPP, GLSL - When I run into problems my colleagues can provide me with immediate help with this language
            C#, C, CPP, TS, GLSL, Lua, PHP - Programs written in this language will usually work in future versions of the language
            C#, TS, GLSL - This language has a wide variety of agreed-upon conventions, which are generally adhered to reasonably well, and which increase my productivity
            C#, TS, GLSL - Code written in this language tends to be very reliable
            C#, TS - Code written in this language is very readable
            C# - This language is best for very large projects
            C#, TS - This language is best for very small projects
            C#, TS - This language is good for beginners
            Lua, PHP - This language is unusually bad for beginners
            C# - Learning this language improved my ability as a programmer
            I enjoy playing with this language but would never use it for "real code"
            C#, JS, C, CPP, GLSL, PHP - I use many applications written in this language
            C# - This language has a very dogmatic community
            C# - It is easy to debug programs written in this language when it goes wrong
            C# - There are many good tools for this language
            TS - There are many good open-source tools for this language
            C# - There are many good commercial tools for this language
            C - Programs written in this language tend to play well with others
            CPP - I would like to write more of this language than I currently do
            PHP - I would use this language for writing server programs
            C - I would use this language for writing embedded programs
            C# - This language is good for distributed computing
            C, CPP, C# - Programs written in this language tend to be efficient
            C# - I know many other people who use this language
            TS - I often write things in this language with the intent of rewriting them in something else later
            C#, C, CPP - This language is good for numeric computing
            C#, C, CPP, GLSL - This language is suitable for real-time applications
            JS, PHP - I am sometimes embarrassed to admit to my peers that I know this language
            JS, PHP - It is too easy to write code in this language that looks like it does one thing but actually does something else
            If this language didn't exist, I would have trouble finding a satisfactory replacement
            JS, TS, PHP (PYTHON!!!) - This language is frequently used for applications it isn't suitable for
            C# - This language has well-organized libraries with consistent, carefully thought-out interfaces
            C#, C, CPP, Lua, TS - I can imagine this will be a popular language in twenty years time
            JS - he thought that I may still be using this language in twenty years time fills me with dread
            C - I would use this language for writing programs for an embedded hardware platform
            Lua - I would use this language as a scripting language embedded inside a larger application
            C# - This language has unusual features that I often miss when using other languages
            C#, TS, C, CPP, JS - This language is likely to have a strong influence on future languages
            PHP (hopefully JS) - This language is likely to be a passing fad
            All of them - I would list this language on my resume
            JS - I am reluctant to admit to knowing this language
            Lua - Developers who primarily use this language often burn out after a few years
            C, C#, JS, CPP, GLSL - This language is likely to be around for a very long time
            C, CPP, C#, TS, GLSL, Lua - This language has a very coherent design
            C#, CPP, C - This language is built on a small core of orthogonal features
            GLSL - This language is minimal
            C#, CPP, C, TS, JS, PHP - This language is large
            C, CPP, JS - This language has many features which feel "tacked on"
            JS, C, CPP - I still discover new features of this language on a fairly regular basis
            C#, C, CPP, GLSL, TS - I use this language out of choice
            JS, Lua, PHP, C, CPP - This language makes it easy to shoot yourself in the foot
            C# - This language has a high quality implementation
            C#, JS, CPP - I learned this language early in my career as a programmer
            C# - This language encourages writing code that is easy to maintain.
            C#, CPP, TS - This language encourages writing reusable code.
            C# - Learning this language significantly changed how I use other languages.
            The semantics of this language are much different than other languages I know.
            C# - If my code in this language successfully compiles, there is a good chance my code is correct.
            C#, TS, JS - This language has a good library distribution mechanism.
            C# - Libraries in this language tend to be well documented.
            C#, TS, JS, GLSL - Code written in this language will usually run in all the major implementations if it runs in one of them.
            C#, C, CPP, Lua - This language matches it's problem domain particularly well.
            This language is easier to use for it's problem domain by removing unneeded expressiveness (such as not being Turing complete).
            C# - This language would be good for teaching children to write software
            C# - This language is well suited for an agile development approach using short iterations.
            C#, C, CPP - I would recommend most programmers learn this language, regardless of whether they have a specific need for it
            C#, C, CPP - This is a mainstream language
            Lua, PHP - This language has an annoying syntax
            I often feel like I am not smart enough to write this language
            C - I use a lot of code written in this language which I really don't want to have to make changes to
            JS!!!!!!!!!!!!!! - I often get angry when writing code in this language
         */
    }

    // Compare some language features from different programming languages like for-loop in C, Java and C#. How do they differ?
    [Fact]
    public void Exercise10()
    {
        // The basic programming syntax isn't different between a lot of higher level languages, like the for loop in C, Java and C#.
        // If you can read one of those languages you can likely read the rest of them without much difficulty.
    }
    
    // Compare foreach loops in Java, C# and Python (you don't need to know Python to look at the loop)
    [Fact]
    public void Exercise11()
    {
        // The foreach loop differs a bit between different languages, but still not a lot.
        // I'm easily able to read a for loop in python despite never having used one, and rarely looking at the python language.
        // This experience is similar to when i look at other foreign programming languages.
    }
}