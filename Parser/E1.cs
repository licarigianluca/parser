﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class E1
{
    public T t;
    public E1 e1;
    public OP op;

    public E1(T t, E1 e1,char op)
    {
        this.op = new OP(op);
        this.t = t;
        this.e1 = e1;
    }
}