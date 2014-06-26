using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class Environment
{
    private Dictionary<String, int> env;
    private Stack<int> executionStack;
    
    public Environment()
    {
        this.env= new Dictionary<string,int>();
        this.executionStack = new Stack<int>();
    }

    public void push(int value)
    {
        this.executionStack.Push(value);
    }

    public int pop()
    {
        return this.executionStack.Pop();
    }

    public bool contains(String key)
    {
        return this.env.ContainsKey(key);
    }

    public void addFrame(String id, int value)
    {
        this.env.Add(id, value);
    }
    public void updateFrame(String key, int value)
    {
        this.env[key] = value;
    }
    public int getValue(String key)
    {
        return this.env[key];
    }
}

