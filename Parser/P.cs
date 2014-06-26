using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



public class P
{
    public S s;
    public EOF eof;

    public P(S s)
    {
        this.s = s;
        this.eof = new EOF();
    }
}
