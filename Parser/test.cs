using System;
using System.Diagnostics;
class Test
{
    static void Main(String[] args)
    {
        Console.WriteLine("Start parsing...");
        String expr = "25+3*5";
        Parser p = new Parser();
        E e =p.parse(expr);
        Debug.Assert(e is E, "Malformed Expression");
        Console.WriteLine("Finish parsing...");
    }
}