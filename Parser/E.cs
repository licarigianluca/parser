using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class E
{
     public T t;
     public E1 e1;
     public EOF eof;

    

    public E(T t, E1 e1)
    {
        
        this.t = t;
        this.e1 = e1;
        this.eof=new EOF();
    }
}

