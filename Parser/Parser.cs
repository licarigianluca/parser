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

    public Environment eval<Z>(Z z, Environment env)
    {
        if (z == null) return env;

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
                if (assign.comcode == null)
                {
                    eval(assign.e, env);
                    env.updateFrame(assign.ID, env.pop());
                    break;
                }
                if (assign.comcode.value == "if")
                {
                    env = eval(assign.c, env);

                    if (env.pop() >= 0)
                    {
                        env = eval(assign.s, env);
                        break;
                    }
                    else
                    {
                        env = eval(assign.eif, env);
                        break;
                    }
                }
                if (assign.comcode.value == "while")
                {
                    env = eval(assign.c, env);
                    if (env.pop() >= 0)
                    {
                        env = eval(assign.s, env);
                        env = eval(assign, env);
                        break;
                    }
                    else break;
                }
                if (assign.comcode.value == "return")
                {
                    env = eval(assign.e, env);
                    break;
                }
                break;
                               
            case "EIF":
                EIF eif = (EIF)(Object)z;
                env = eval(eif.s, env);
                break;

            case "C":
                C c = (C)(Object)z;
                env = eval(c.e, env);
                env = eval(c.c1, env);
                break;

            case "C1":
                C1 c1 = (C1)(Object)z;
                env = eval(c1.e, env);
                consumeOP(c1.op, env);
                break;

            case "E":
                E e = (E)(Object)z;
                if (e.e1 == null)
                {
                    env = eval(e.t, env);
                }

                else
                {
                    env = eval(e.t, env);
                    env = eval(e.e1, env);
                }
                break;
            case "E1":
                E1 e1 = (E1)(Object)z;
                if (e1.e1 == null)
                {
                    env = eval(e1.t, env);
                    consumeOP(e1.op, env);
                }
                else
                {
                    env = eval(e1.t, env);
                    env = eval(e1.e1, env);
                    consumeOP(e1.op, env);
                }
                break;
            case "T":
                T t = (T)(Object)z;
                if (t.t1 == null)
                {
                    env = eval(t.f, env);
                }
                else
                {
                    env = eval(t.f, env);
                    env = eval(t.t1, env);
                }
                break;
            case "T1":
                T1 t1 = (T1)(Object)z;
                if (t1.t1 == null)
                {
                    env = eval(t1.f, env);
                    consumeOP(t1.op, env);
                }
                else
                {
                    env = eval(t1.f, env);
                    env = eval(t1.t1, env);
                    consumeOP(t1.op, env);
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
        if (lookahead.Type == (int)type.ID | lookahead.Type == (int)type.WHILE | lookahead.Type == (int)type.IF | lookahead.Type == (int)type.FUNCTION | lookahead.Type == (int)type.RETURN)
        {
            return new S(AS(), S1());
        }
        else return null;
    }
    //S1 -> ';' S
    S1 S1()
    {

        Match(type.SEMICOLON);
        return new S1(S());

    }
    //AS -> ID X
    //     'while' '(' C ')' '{' S '}'|
    //     'if' '(' C ')' 'then' '{' S'}' EIF|
    //     'function' ID(ID) '{' S  '}'
    //     'return' E 
    AS AS()
    {
        C c;
        S s;
        
        EIF eif;
        

        if (lookahead.Type == (int)type.ID)
        {
            String value = lookahead.Value;
            Match(type.ID);
            return new AS(X(), value);
        }

        else if (lookahead.Type == (int)type.WHILE)
        {
            Match(type.WHILE);
            Match(type.OPEN_PAR);
            c = C();
            Match(type.CLOSE_PAR);
            Match(type.OPEN_CURLY);
            s = S();
            Match(type.CLOSE_CURLY);
            return new AS(c, s);
        }
        else if (lookahead.Type == (int)type.FUNCTION)
        {
            Match(type.FUNCTION);
            String functionNAme = lookahead.Value;
            Match(type.ID);
            Match(type.OPEN_PAR);
            String argument = lookahead.Value;
            Match(type.ID);
            Match(type.CLOSE_PAR);
            Match(type.OPEN_CURLY);
            s = S();
            Match(type.CLOSE_CURLY);
            return new AS(functionNAme, argument, s);

        }
        else if (lookahead.Type == (int)type.IF)
        {
            Match(type.IF);
            Match(type.OPEN_PAR);
            c = C();
            Match(type.CLOSE_PAR);
            Match(type.THEN);
            Match(type.OPEN_CURLY);
            s = S();
            Match(type.CLOSE_CURLY);
            eif = EIF();
            return new AS(c, s, eif);

        }
        else
        {
            Match(type.RETURN);
            return new AS(E());
        }
    }
    //X -> ':=' E Y | (ID)
    
    X X()
    {
        
        if (lookahead.Type == (int)type.ASSIGN)
        {
            Match(type.ASSIGN);
            return new X(E(),Y());
        }
        else
        {
            Match(type.OPEN_PAR);
            String value = lookahead.Value;
            Match(type.ID);
            Match(type.CLOSE_PAR);
            return new X(value);
        }
    }
    //Y -> (ID) | epsilon
    Y Y()
    {
        if (lookahead.Type == (int)type.OPEN_PAR)
        {
            Match(type.OPEN_PAR);
            String value = lookahead.Value;
            Match(type.ID);
            Match(type.CLOSE_PAR);
            return new Y(value);
        }
        else return null;
    }
    
    //EIF -> 'else' '{' S '}' | epsilon
    EIF EIF()
    {
        S s;
        if (lookahead.Type == (int)type.ELSE)
        {
            Match(type.ELSE);
            Match(type.OPEN_CURLY);
            s = S();
            Match(type.CLOSE_CURLY);
            return new EIF(s);
        }
        else return null;
    }
    //C -> E C1
    C C()
    {
        return new C(E(), C1());
    }
    //C1 ->     '<'  E |
    //          '>'  E |
    //          '='  E |
    //          '>=' E | NO
    //          '<=' E | NO
    //          '!=' E |
    C1 C1()
    {
        if (lookahead.Type == (int)type.LT)
        {
            Match(type.LT);

            return new C1(new OP('<'), E());
        }
        else if (lookahead.Type == (int)type.GT)
        {
            Match(type.GT);
            return new C1(new OP('>'), E());
        }
        else if (lookahead.Type == (int)type.EQUAL)
        {
            Match(type.EQUAL);
            return new C1(new OP('='), E());
        }
        else
        {
            Match(type.DISEQUAL);
            return new C1(new OP("!="), E());
        }

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

            return new E1(T(), E1(), '+');
        }
        else if (lookahead.Type == (int)type.MINUS)
        {
            Match(type.MINUS);

            return new E1(T(), E1(), '-');
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
            return new T1(F(), T1(), '*');
        }
        else if (lookahead.Type == (int)type.OBELUS)
        {
            Match(type.OBELUS);
            return new T1(F(), T1(), '/');
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
       // Console.WriteLine((int)t);
        Debug.Assert(lookahead.Type == (int)t, "Syntax error");
        lookahead = this.t.nextToken();
    }
    protected void consumeOP(OP op, Environment env)
    {
        int op1, op2, result = 0;
        op1 = env.pop();
        op2 = env.pop();

        switch (op.getValue())
        {
            case '+':
                result = op1 + op2;
                break;
            case '-':
                result = op2 - op1;
                break;
            case '*':
                result = op1 * op2;
                break;
            case '/':
                result = op2 / op1;
                break;
            case '>':
                result = op2 - op1;
                break;
            case '<':
                result = op1 - op2;
                break;
            case '=':
                result = op2 - op1;
                break;
            default:
                break;
        }
        env.push(result);
    }
}




