using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

public enum type
{
    PLUS, TIMES, ID, INVALID_TOKEN, CLOSE_PAR, OPEN_PAR, EOF, NUM,
    MOD, DIV, OBELUS, MINUS, ASSIGN, SEMICOLON, OPEN_CURLY, CLOSE_CURLY, ARROW,
    IF, THEN, ELSE, WHILE, GT, LT, EQUAL, DISEQUAL, FUNCTION, RETURN
};


public class Tokenizer
{
    private bool ignoreBlanks = true;
    private int idx;
    private String s;
    private LinkedList<Token> tokenList;
    private Dictionary<String, int> keyword = new Dictionary<string, int>(){
        {"if",(int)type.IF},
        {"then",(int)type.THEN},
        {"else",(int)type.ELSE},
        {"while",(int)type.WHILE},
        {"function",(int)type.FUNCTION},
        {"return", (int)type.RETURN}
    };

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
            if (idx < s.Length && s[idx + 1] == '>')
            {
                string str = s[idx].ToString() + s[idx + 1].ToString();
                t = new Token(str, (int)type.ARROW);
                idx += 2;
            }
            else
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
        else if (s[idx] == '}')
        {
            t = new Token(s[idx++].ToString(), (int)type.CLOSE_CURLY);
        }
        else if (s[idx] == '{')
        {
            t = new Token(s[idx++].ToString(), (int)type.OPEN_CURLY);
        }
        else if (s[idx] == '<')
        {
            t = new Token(s[idx++].ToString(), (int)type.LT);
        }
        else if (s[idx] == '>')
        {
            t = new Token(s[idx++].ToString(), (int)type.GT);
        }
        else if (s[idx] == '=')
        {
            t = new Token(s[idx++].ToString(), (int)type.EQUAL);
        }
        else if (s[idx] == '{')
        {
            t = new Token(s[idx++].ToString(), (int)type.OPEN_CURLY);
        }
        else if (s[idx] == '!' )
        {
            if (s[idx + 1] == '=')
            {
                string str = s[idx].ToString() + s[idx + 1].ToString();
                t = new Token(str, (int)type.DISEQUAL);
                idx += 2;
                ignoreBlanks = true;
            }
            else
            {
                ignoreBlanks = false;
                t = new Token(s[idx++].ToString(), (int)type.INVALID_TOKEN);
            }
        }
        else if (s[idx] == ':')
        {
            if (s[idx + 1] == '=')
            {
                string str = s[idx].ToString() + s[idx + 1].ToString();
                t = new Token(str, (int)type.ASSIGN);
                idx += 2;
                ignoreBlanks = true;
            }
            else
            {
                ignoreBlanks = false;
                t = new Token(s[idx++].ToString(), (int)type.INVALID_TOKEN);
            }
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
            if (keyword.ContainsKey(lexeme))
            {
                t = new Token(lexeme.ToString(), keyword[lexeme]);
            }
            else
            {
                t = new Token(lexeme.ToString(), (int)type.ID);
            }
        }
        else
        {
            if ((s[idx] == ' ' || s[idx] == '\n') && ignoreBlanks)
            {
                idx++;
                t = nextToken();
            }
            else
            {
                t = new Token(s[idx++].ToString(), (int)type.INVALID_TOKEN);
            }
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