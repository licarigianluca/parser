using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class AS
{
    public String ID;
    public E e;
    private ASSIGN assign;
    
    public C c;
    public S s;
    public EIF eif;

    public AS(E e, String ID)
    {
        this.e = e;
        this.assign = new ASSIGN();
        this.ID = ID;
    }

    public AS(C c, S s)
    {
        this.c = c;
        this.s = s;
        
    }

    public AS(C c, S s, EIF eif)
    {
        this.c = c;
        this.s = s;

    }
}

