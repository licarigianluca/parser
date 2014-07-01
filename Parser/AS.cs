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
    public EIF eif;

    public AS(E e, String ID)
    {
        this.e = e;
        this.assign = new ASSIGN();
        this.ID = ID;
        this.c = null;
        this.s = null;
        this.eif = null;
    }

    public AS(C c, S s)
    {
        this.e = null;
        this.assign = null;
        this.ID = null;
        this.c = c;
        this.s = s;
        this.eif = null;
        
    }

    public AS(C c, S s, EIF eif)
    {
        this.e = null;
        this.assign = null;
        this.ID = null;
        this.c = c;
        this.s = s;
        this.eif = eif;
    }
}

