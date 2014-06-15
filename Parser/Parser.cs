using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Parser
{
    private Tokenizer t;
    Token lookahead;

    public Parser()
    {

    }

    public void parse(String s)
    {
        t = new Tokenizer(s);
        do
        {
            lookahead = t.nextToken();
            Console.WriteLine(lookahead.Value);
        }
        while (lookahead.Type != (int)type.EOF);
        Console.WriteLine("OK");
    }
}

