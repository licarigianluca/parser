﻿using System;
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

    public int eval<Z>(Z z)
    {
        int result = 0;
        Type type = typeof(Z);
        String typeStr = type.ToString();

        

        switch (typeStr)
        {
            case "E":
                E e = (E)(Object)z;
                if(e.e1 == null){
                    result = eval(e.t);
                }
                
                else{
                    result = eval(e.t)+eval(e.e1);
                }
                
                break;
            case "E1":
                E1 e1 = (E1)(Object)z;
                if (e1.e1 == null)
                {
                    result = eval(e1.t);
                }
                else
                {
                    result = eval(e1.t) + eval(e1.e1);
                }
                break;
            case "T":
                T t = (T)(Object)z;
                if (t.t1==null)
                {
                    result = eval(t.f);
                }
                else{
                    result = eval(t.f)*eval(t.t1);
                }
                break;
            case "T1":
                T1 t1 = (T1)(Object)z;
                if (t1.t1 == null)
                {
                    result = eval(t1.f);
                }
                else
                {
                    result = eval(t1.f) * eval(t1.t1);
                }
                break;
            case "F":
                F f = (F)(Object)z;
                if (f.value != null)
                {
                    result = Convert.ToInt32(f.value);
                }
                else
                {
                    result = eval(f.e);
                }
                break;
            default: return 0;

        }


        return result;
    }
    

   
    public P parse(String s)
    {
        t = new Tokenizer(s);
        lookahead = t.nextToken();

       // E e = E();
        P p = P();
        Match(type.EOF);
        return p;

    }

    P P()
    {
        return new P(S());
    }

    S S(){
        return new S(AS(),S1());
    }

    S1 S1(){
        if (lookahead.Type == (int)type.SEMICOLON)
        {
            Match(type.SEMICOLON);
            return new S1(S());
        }
        return null;
    }

    AS AS()
    {
        
        Match(type.ID);
        Match(type.ASSIGN);
        String value = lookahead.Value;
        return new AS(E(),value);
        
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

        return null;
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
        return null;

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