using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class F
{
    public E e;
    public String value;
    public PAR par1, par2;

    public F(E e)
    {
        this.par1 = new PAR('(');
        this.e = e;
        this.par1 = new PAR(')');
        this.value = null;
    }

    public F(String value)
    {
        this.value = value;
        this.e = null;
        this.par1 = null;
        this.par2 = null;
    }

}