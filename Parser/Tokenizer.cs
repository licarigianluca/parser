using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

public enum type { PLUS, TIMES, ID, INVALID_TOKEN, CLOSE_PAR, OPEN_PAR, EOF, NUM, MOD, DIV, OBELUS, MINUS, ASSIGN, SEMICOLON };

public class Tokenizer
{
    private int idx;
    private String s;
    private LinkedList<Token> tokenList;

    public Tokenizer(String expr)
    {
        this.s = expr;
        this.idx = 0;
        this.tokenList = new LinkedList<Token>();
    }

    public Token nextToken()
    {

        String lexeme;
        Token t;

        if (idx >= s.Length)
        {
            idx = 0;
            t = new Token("EOF".ToString(), (int)type.EOF);
        }
        else if (s[idx] == '+')
        {
            t = new Token(s[idx++].ToString(), (int)type.PLUS);
        }
        else if (s[idx] == '-')
        {
            t = new Token(s[idx++].ToString(), (int)type.MINUS);
        }
        else if (s[idx] == '*')
        {
            t = new Token(s[idx++].ToString(), (int)type.TIMES);
        }
        else if (s[idx] == '/')
        {
            t = new Token(s[idx++].ToString(), (int)type.OBELUS);
        }
        else if (s[idx] == '(')
        {
            t = new Token(s[idx++].ToString(), (int)type.OPEN_PAR);
        }
        else if (s[idx] == ')')
        {
            t = new Token(s[idx++].ToString(), (int)type.CLOSE_PAR);
        }
        else if (s[idx] == ';')
        {
            t = new Token(s[idx++].ToString(), (int)type.SEMICOLON);
        }
        else if (s[idx] == ':' && s[idx + 1] == '=')
        {
            string str = s[idx].ToString() + s[idx+1].ToString();
            t = new Token(str, (int)type.ASSIGN);
            idx += 2; ;
        }
        else if (isDigit(s[idx]))
        {
            lexeme = s[idx++].ToString();
            while (idx < s.Length && isDigit(s[idx]))
            {
                lexeme += s[idx++];
            }
            t = new Token(lexeme.ToString(), (int)type.NUM);
        }
        else if (isChar(s[idx]))
        {
            lexeme = s[idx++].ToString();
            while (idx < s.Length && isChar(s[idx]))
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

    public LinkedList<Token> getTokenList()
    {
        int i = 0;
        while (i <= this.s.Length)
        {
            this.tokenList.AddLast(nextToken());
            i++;
        }
        return this.tokenList;
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