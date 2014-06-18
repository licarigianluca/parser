using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;



public class P
{
    public S stmts;
    public EOF eof;

    public P(S stmts)
    {
        this.stmts = stmts;
        this.eof = new EOF();
    }
}
