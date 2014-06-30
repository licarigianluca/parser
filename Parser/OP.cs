using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class OP
{
    private char value;
    private String strValue;

    public OP(char value)
    {
        this.value = value;
        this.strValue = null;
    }

    public OP(String strValue)
    {
        this.strValue = strValue;
    }

    public char getValue()
    {
        return this.value;
    }
};

