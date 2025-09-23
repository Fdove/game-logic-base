using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

public abstract class Test
{
    Action<String> printer { get; set; }
    public Test(Action<String> printer) => this.printer = printer;
    public virtual String Expected(object obj) => "EX:" + obj.ToString();
    public virtual void Print(object obj, int tab = 0)
    {
        for (int i = 0; i < tab; ++i) printer("\t");
        printer(obj.ToString());
    }
    public virtual void Println(object obj, int tab = 0) => Print(obj + "\n", tab);
    public virtual void Println() => Print("\n");
    public virtual void PrintTuple(object[] objs, String link = " : ", int tab = 0)
    {
        if (objs.Length == 0) return;
        Print(objs[0], tab);
        for (int i = 1; i < objs.Length; ++i)
        {
            Print(link + objs[i]);
        }
    }
    public virtual void PrintTupleln(object[] objs, String link = " : ", int tab = 0)
    {
        PrintTuple(objs, link, tab);
        Println("");
    }
    public virtual void Title(object obj)
    {
        Print("##### ");
        Print(obj);
        Println(" #####");
        Println();
    }
}