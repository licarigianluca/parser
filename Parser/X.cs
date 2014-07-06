using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class X
{
    public E e;
    public ASSIGN assign;
    public String ID;
    public Y y;

    public X(E e ,Y y)
    {
        this.e = e;
        this.assign = new ASSIGN();
        this.ID = null;
        this.y = y;

    }
    public X(String ID)
    {
        this.e = null;
        this.assign = null;
        this.ID = ID;
    }
}

