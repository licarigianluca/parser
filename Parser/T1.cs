using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class T1
{
    public F f;
    public T1 t1;
    public OP op;

    public T1(F f, T1 t1)
    {
        this.op = new OP('*');
        this.f = f;
        this.t1 = t1;
    }
}



