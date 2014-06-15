using System;
class Test
{
    static void Main(String[] args)
    {
        Console.WriteLine("Per adesso non fa niente vero");
        String expr = "25+3*5";
        Parser p = new Parser();
        p.parse(expr);
    }
}