using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;


public class Parser
{
    private Tokenizer t;
    Token lookahead;

    public Parser()
    {

    }

    public E parse(String s)
    {
        t = new Tokenizer(s);
        lookahead = t.nextToken();

        return E();
    }

    E E()
    {
        return new E(T(), E1());
    }
    E1 E1()
    {

        //first set
        if (lookahead.Type == (int)type.PLUS)
        {
            Match(type.PLUS);

            return new E1(T(), E1());
        }
        else if ((lookahead.Type == (int)type.CLOSE_PAR) || (lookahead.Type == (int)type.EOF))
        {
            return null;
        }
        else
        {
            Debug.Assert(false, "Syntax error");
            return null;
        }
    }


    T T()
    {
        return new T(F(), T1());
    }

    T1 T1()
    {


        if (lookahead.Type == (int)type.TIMES)
        {
            Match(type.TIMES);
            return new T1(F(), T1());
        }
        else if ((lookahead.Type == (int)type.CLOSE_PAR)
                    || (lookahead.Type == (int)type.PLUS)
                    || (lookahead.Type == (int)type.EOF))
            return null;
        else
        {
            Debug.Assert(false, "Syntax error");
            return null;
        }
    }

    F F()
    {
        E e;

        if (lookahead.Type == (int)type.OPEN_PAR)
        {
            Match(type.OPEN_PAR);
            e = E();
            Match(type.CLOSE_PAR);
            return new F(e);

        }

        String value = lookahead.Value;
        Match(type.ID);
        return new F(value);

    }



    protected void Match(type t)
    {
        Debug.Assert(lookahead.Type == (int)t, "Syntax error");
        lookahead = this.t.nextToken();
    }
}

