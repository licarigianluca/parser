using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Token
{

    private String value;
    public String Value { get { return this.value; } }
    private int type;
    public int Type { get { return this.type; } }

    public Token(String value, int type)
    {
        this.value = value;
        this.type = type;
    }
}