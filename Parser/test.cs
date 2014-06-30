using System;
using System.Diagnostics;
using System.Collections.Generic;

class Test
{
    static void Main(String[] args)
    {
        Console.WriteLine("Start parsing...");
        //String expr = Console.ReadLine();
        String expr = "while(x<0){x:=5;};";
        showToken(expr);
        parseExpr(expr);


    }

    private static void parseExpr(String expr)
    {

        Parser p = new Parser();
        P program = p.parse(expr);
        Environment env = new Environment();
        env = p.eval(program,env);
        //Console.WriteLine("Result of evaluation:\t" + result);
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

        /*
        LinkedList<Token> tokenList = t.getTokenList();
        int countList = 0;
        foreach (Token token in tokenList)
        {
            Console.WriteLine(token.Value + '\t' + converter(token.Type));
            countList++;
        }
        Console.WriteLine("countList" + '\t' + countList);
         * */


    }


    public static String converter(int type)
    {

        switch (type)
        {
            case 0: return "PLUS";
            case 1: return "TIMES";
            case 2: return "ID";
            case 3: return "INVALID_TOKEN";
            case 4: return "CLOSE_PAR";
            case 5: return "OPEN_PAR";
            case 6: return "EOF";
            case 7: return "NUM";
            case 8: return "MOD";
            case 9: return "DIV";
            case 10: return "OBELUS";
            case 11: return "MINUS";
            case 12: return "ASSIGN";
            case 13: return "SEMICOLON";
            case 14: return "OPEN_CURLY";
            case 15: return "CLOSE_CURLY";
            case 16: return "ARROW";
            case 17: return "IF";
            case 18: return "THEN";
            case 19: return "ELSE";
            case 20: return "WHILE";
            case 21: return "GT";
            case 22: return "LT";
            case 23: return "EQUAL";
            case 24: return "DISEQUAL";
            default: return null;
        }
        
    }
}