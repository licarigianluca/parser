using System;
using System.Diagnostics;

class Test
{
    static void Main(String[] args)
    {
        Console.WriteLine("Start parsing...");
        String expr =  Console.ReadLine();
        showToken(expr);
        parseExpr(expr);
        
    
    }

    private static void parseExpr(String expr)
    {
        
        Parser p = new Parser();
        E e = p.parse(expr);
        //Debug.Assert(e is E, "Malformed Expression");
        int result =p.eval(e);
        Console.WriteLine("Result of evaluation:\t" + result);
        Console.WriteLine("Finish parsing...");
    }

    private static void showToken(String expr)
    {
        Tokenizer t = new Tokenizer(expr);
        Token lookahead = t.nextToken();
        while (lookahead.Type != (int)type.EOF)
        {
            Console.WriteLine(lookahead.Value + "\t" + converter(lookahead.Type));
            lookahead = t.nextToken();
        }
        Console.WriteLine(lookahead.Value + "\t" + converter(lookahead.Type));
    }

    
    public static String converter(int type){

        switch (type)
        {
            case 0: return "PLUS";
            case 1: return "TIMES";
            case 2: return "ID";
            case 3: return "INVALID_TOKEN";
            case 4: return "CLOSE_PAR";
            case 5: return "OPEN_PAR";
            case 6: return "EOF";
            default: return null;
        }
        
    }
}