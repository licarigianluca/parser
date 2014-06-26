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

    public AS(E e, String ID)
    {
        this.e = e;
        this.assign = new ASSIGN();
        this.ID = ID;
    }
}

