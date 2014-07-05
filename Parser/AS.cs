using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class AS
{
    public String ID;
    public E e;
    public ASSIGN assign;
    public C c;
    public S s;
    public R r;
    public EIF eif;
    public COMCODE comcode;

    public AS(E e, String ID)
    {
        this.e = e;
        this.assign = new ASSIGN();
        this.ID = ID;
        this.c = null;
        this.s = null;
        this.eif = null;
        this.comcode = null;
        this.r = null;
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
        this.r = null;
        
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
        this.r = null;
    }
    public AS(String functionName, String argument, S s, R r)
    {
        this.e = null;
        this.assign = null;
        this.ID = null;
        this.c = null;
        this.s = s;
        this.eif = null;
        this.comcode = new COMCODE("function");
        this.r = r;
    }
}

