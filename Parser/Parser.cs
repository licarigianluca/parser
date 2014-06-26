using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Diagnostics;




public class Parser
{
    private Tokenizer t;
    Token lookahead;
    LinkedList<Token> tokenList;

    public Parser()
    {

    }

    public Environment eval<Z>(Z z, Environment env)
    {
        if (z == null) return env;
        
        int result = 0;
        Type type = typeof(Z);
        String typeStr = type.ToString();

        switch (typeStr)
        {
            case "P":
                P p = (P)(Object)z;
                
                env = eval(p.s, env);
                break;
            case "S":
                S s = (S)(Object)z;
                
                env = eval(s.assign, env);
                env = eval(s.s1, env);
                break;
            case "S1":
                S1 s1 = (S1)(Object)z;
                env = eval(s1.s, env);
                break;
            case "AS":
                AS assign = (AS)(Object)z;
                eval(assign.e, env);
                env.updateFrame(assign.ID, env.pop());
                break;


            case "E":
                E e = (E)(Object)z;
                if (e.e1 == null)
                {
                    env = eval(e.t,env);
                }

                else
                {
                    env = eval(e.t, env);
                }
                break;
            case "E1":
                E1 e1 = (E1)(Object)z;
                if (e1.e1 == null)
                {
                    env = eval(e1.t,env);
                }
                else
                {
                    env = eval(e1.t, env);
                }
                break;
            case "T":
                T t = (T)(Object)z;
                if (t.t1 == null)
                {
                    env = eval(t.f,env);
                }
                else
                {
                    env = eval(t.f,env);
                }
                break;
            case "T1":
                T1 t1 = (T1)(Object)z;
                if (t1.t1 == null)
                {
                    env = eval(t1.f,env);
                }
                else
                {
                    env = eval(t1.f,env);
                }
                break;
            case "F":
                F f = (F)(Object)z;

                if (f.e != null)
                {
                    env = eval(f.e, env);
                                      
                }
                else if (f.value != null)
                {
                    //ID
                    Debug.Assert(env.contains(f.value), "Identifier not already present in the store.");
                    env.push(env.getValue(f.value));

                }
                else
                {
                    env.push(f.intValue);
                }
                break;
            default: break;

        }

        //stack.push(result)
        result += result;
        return env;
    }



    public P parse(String s)
    {
        t = new Tokenizer(s);
        lookahead = t.nextToken();


        P p = P();
        Match(type.EOF);
        return p;

    }

    //P -> S 'EOF'
    P P()
    {
        return new P(S());
    }
    //S -> AS S1 | epsilon
    S S()
    {
        if (lookahead.Type == (int)type.ID)
        {
            return new S(AS(), S1());
        }

        return null;
    }
    //S1 -> ';' S
    S1 S1()
    {

        Match(type.SEMICOLON);
        return new S1(S());

    }
    //AS -> ID ':=' E 
    AS AS()
    {
        String value = lookahead.Value;
        Match(type.ID);
        Match(type.ASSIGN);
        return new AS(E(), value);

    }
    //E -> T E1
    E E()
    {
        return new E(T(), E1());
    }
    //E1 -> '+' T E1 | 
    //      '-' T E1 |
    //      epsilon
    E1 E1()
    {


        if (lookahead.Type == (int)type.PLUS)
        {
            Match(type.PLUS);

            return new E1(T(), E1(),'+');
        }
        else if (lookahead.Type == (int)type.MINUS)
        {
            Match(type.MINUS);

            return new E1(T(), E1(),'-');
        }
        else return null;
    }

    //T -> F T1
    T T()
    {
        return new T(F(), T1());
    }

    //T1 -> '*' F T1 |
    //      '/' F T1 |
    //      epsilon
    T1 T1()
    {

        if (lookahead.Type == (int)type.TIMES)
        {
            Match(type.TIMES);
            return new T1(F(), T1(),'*');
        }
        else if (lookahead.Type == (int)type.OBELUS)
        {
            Match(type.OBELUS);
            return new T1(F(), T1(),'/');
        }
        else return null;

    }

    //F -> '(' E ')' |
    //     ID        |
    //     NUM
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
        else if (lookahead.Type == (int)type.ID)
        {
            String value = lookahead.Value;
            Match(type.ID);
            return new F(value);
        }
        else
        {

            String value = lookahead.Value;
            Match(type.NUM);
            return new F(Convert.ToInt32(value));
        }
    }



    protected void Match(type t)
    {
        Debug.Assert(lookahead.Type == (int)t, "Syntax error");
        lookahead = this.t.nextToken();
    }


}