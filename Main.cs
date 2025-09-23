using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            new test_OpenPriorityQueue((str)=>Console.Write(str), true);
        }
    }
}