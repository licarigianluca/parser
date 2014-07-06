using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class AS
{
    public String ID;
    public E e;
    public X x;
    public ASSIGN assign;
    public C c;
    public S s;
    public EIF eif;
    public COMCODE comcode;
    public String functionName;
    public String argument;

    public AS(X x, String ID)
    {
        
        this.e = null;
        this.assign = null;
        this.ID = ID;
        this.c = null;
        this.s = null;
        this.eif = null;
        this.comcode = null;
        
        this.functionName = null;
        this.argument = null;
        this.x = x;
    }

    public AS(C c, S s)
    {
        this.e = null;
        this.assign = null;
        this.ID = null;
        this.c = c;
        this.s = s;
        this.eif = null;
        this.comcode = new COMCODE("while");
        
        this.functionName = null;
        this.argument = null;
        this.x = null;
    }

    public AS(C c, S s, EIF eif)
    {
        this.e = null;
        this.assign = null;
        this.ID = null;
        this.c = c;
        this.s = s;
        this.eif = eif;
        this.comcode = new COMCODE("if");
        
        this.functionName = null;
        this.argument = null;
        this.x = null;
    }
    public AS(String functionName, String argument, S s)
    {
        this.e = null;
        this.assign = null;
        this.ID = null;
        this.c = null;
        this.s = s;
        this.eif = null;
        this.comcode = new COMCODE("function");
        
        this.functionName = functionName;
        this.argument = argument;
        this.x = null;
    }
    public AS(E e)
    {
        this.e = e;
        this.comcode = new COMCODE("return");
    }
}

