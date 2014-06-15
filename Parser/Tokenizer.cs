using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

public enum type { PLUS, TIMES, ID, INVALID_TOKEN, CLOSE_PAR, OPEN_PAR, EOF };

public class Tokenizer
{
    private int idx;
    private String s;

    public Tokenizer(String expr)
    {
        this.s = expr;
        this.idx = 0;
    }

    public Token nextToken()
    {

        String lexeme;
        Token t;

        if (idx >= s.Length)
        {
            t = new Token("EOF".ToString(), (int)type.EOF);
        }
        else if (s[idx] == '+')
        {
            t = new Token(s[idx++].ToString(), (int)type.PLUS);
        }
        else if (s[idx] == '*')
        {
            t = new Token(s[idx++].ToString(), (int)type.TIMES);
        }
        else if (s[idx] == '(')
        {
            t = new Token(s[idx++].ToString(), (int)type.OPEN_PAR);
        }
        else if (s[idx] == ')')
        {
            t = new Token(s[idx++].ToString(), (int)type.CLOSE_PAR);
        }
        else if (isDigit(s[idx]))
        {
            lexeme = s[idx++].ToString();
            while (idx < s.Length && isDigit(s[idx]))
            {
                lexeme += s[idx++];
            }
            t = new Token(lexeme.ToString(), (int)type.ID);
        }
        else
        {
            t = new Token(s[idx++].ToString(), (int)type.INVALID_TOKEN);
        }

        return t;
    }

    private bool isDigit(char c)
    {
        return (c >= '0' && c <= '9');
    }

    private bool isChar(char c)
    {
        return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
    }

}

    



