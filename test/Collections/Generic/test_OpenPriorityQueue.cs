using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GLB.Collections.Generic;
using Microsoft.VisualBasic;

class test_OpenPriorityQueue : Test
{
    public OpenPriorityQueue<String, int>? NormalOpenPriorityQueue { get; set; } = null;
    public class Comparer : IComparer<int>
    {
        public int Compare(int lhs, int rhs) {
            return rhs - lhs;
        }
    }

    public test_OpenPriorityQueue(Action<String> printer, bool custom = false) : base(printer)
    {
        Title("test_OpenPriorityQueue");

        string? e;
        int p;

        Println("OpenPriorityQueue()");
        if (custom) NormalOpenPriorityQueue = new OpenPriorityQueue<String, int>(new Comparer());
        else NormalOpenPriorityQueue = new OpenPriorityQueue<String, int>();
        PrintTupleln(new object[] { "NormalOpenPriorityQueue != null", NormalOpenPriorityQueue != null, Expected(true) }, tab: 1);

        Println("IComparer<TPriority> Comparer");
        PrintTupleln(new object[] { "NormalOpenPriorityQueue.Comparer.Compare(-1, 1)", NormalOpenPriorityQueue.Comparer.Compare(-1, 1), Expected("-1|2") }, tab: 1);
        PrintTupleln(new object[] { "NormalOpenPriorityQueue.Comparer.Compare(0, 0)", NormalOpenPriorityQueue.Comparer.Compare(0, 0), Expected("0|0") }, tab: 1);
        PrintTupleln(new object[] { "NormalOpenPriorityQueue.Comparer.Compare(1, -1)", NormalOpenPriorityQueue.Comparer.Compare(1, -1), Expected("1|-2") }, tab: 1);

        Println("Test Input Data");
        List<(String, int)> testInputData = new(){
            ("E", 5), ("B", 2), ("H", 8),
            ("A", 1), ("G", 7), ("C", 3),
            ("J", 10), ("F", 6), ("D", 4),
            ("I", 9)
        };
        PrintTupleln(new object[] {
            ("E", 5), ("B", 2), ("H", 8),
            ("A", 1), ("G", 7), ("C", 3),
            ("J", 10), ("F", 6), ("D", 4),
            ("I", 9) },
            link: ", ", tab: 1
        );
        Println("Enqueue");
        foreach (var i in testInputData) NormalOpenPriorityQueue.Enqueue(i.Item1, i.Item2);

        PrintTupleln(new object[] { "NormalOpenPriorityQueue.Count", NormalOpenPriorityQueue.Count, Expected(10) }, tab: 1);
        Println("IReadOnlyList<(TElement element, TPriority priority)> OrderedItems + foreach");
        foreach (var i in NormalOpenPriorityQueue.OrderedItems)
        {
            PrintTupleln(new object[] { i.Item1, i.Item2 }, tab: 1);
        }
        Println();

        Println("Dequeue");
        PrintTupleln(new object[] { "NormalOpenPriorityQueue.Dequeue()", NormalOpenPriorityQueue.Dequeue(), Expected("A|J") }, tab: 1);
        PrintTupleln(new object[] { "NormalOpenPriorityQueue.Dequeue()", NormalOpenPriorityQueue.Dequeue(), Expected("B|I") }, tab: 1);
        PrintTupleln(new object[] { "NormalOpenPriorityQueue.Dequeue()", NormalOpenPriorityQueue.Dequeue(), Expected("C|H") }, tab: 1);
        Println("IReadOnlyList<(TElement element, TPriority priority)> OrderedItems + foreach");
        foreach (var i in NormalOpenPriorityQueue.OrderedItems)
        {
            PrintTupleln(new object[] { i.Item1, i.Item2 }, tab: 1);
        }
        Println();

        Println("NormalOpenPriorityQueue.RemoveAt(2)");
        NormalOpenPriorityQueue.RemoveAt(2);
        Println("NormalOpenPriorityQueue.RemoveAt(4)");
        NormalOpenPriorityQueue.RemoveAt(4);
        Println("NormalOpenPriorityQueue.RemoveAt(2)");
        NormalOpenPriorityQueue.RemoveAt(2);
        Println("IReadOnlyList<(TElement element, TPriority priority)> OrderedItems + foreach");
        foreach (var i in NormalOpenPriorityQueue.OrderedItems)
        {
            PrintTupleln(new object[] { i.Item1, i.Item2 }, tab: 1);
        }
        Println();

        PrintTupleln(new object[] { "NormalOpenPriorityQueue.Peek()", NormalOpenPriorityQueue.Peek(), Expected("D|G") });
        PrintTupleln(new object[] { "NormalOpenPriorityQueue.TryDequeue(out e, out p), e, p", NormalOpenPriorityQueue.TryDequeue(out e, out p), e, p });
        PrintTupleln(new object[] { "NormalOpenPriorityQueue.TryPeek(out e, out p), e, p", NormalOpenPriorityQueue.TryPeek(out e, out p), e, p });
        Println("NormalOpenPriorityQueue.Clear()");
        NormalOpenPriorityQueue.Clear();
        PrintTupleln(new object[] { "NormalOpenPriorityQueue.Count", NormalOpenPriorityQueue.Count, Expected(0) });
        PrintTupleln(new object[] { "NormalOpenPriorityQueue.TryDequeue(out e, out p), e, p", NormalOpenPriorityQueue.TryDequeue(out e, out p), e, p });
        PrintTupleln(new object[] { "NormalOpenPriorityQueue.TryPeek(out e, out p), e, p", NormalOpenPriorityQueue.TryPeek(out e, out p), e, p });
    }

}