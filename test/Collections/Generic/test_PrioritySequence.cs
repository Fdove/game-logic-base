using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using GLB.Collections.Generic;
using Microsoft.VisualBasic;

class test_PrioritySequence : Test
{
    public PrioritySequence<String, int>? NormalPrioritySequence { get; set; } = null;
    public class Comparer : IComparer<int>
    {
        public int Compare(int lhs, int rhs) {
            return rhs - lhs;
        }
    }

    public test_PrioritySequence(Action<String> printer, bool custom = false) : base(printer)
    {
        Title("test_PrioritySequence");

        string? e;
        int p;

        Println("PrioritySequence()");
        if (custom) NormalPrioritySequence = new PrioritySequence<String, int>(new Comparer());
        else NormalPrioritySequence = new PrioritySequence<String, int>();
        PrintTupleln(new object[] { "NormalPrioritySequence != null", NormalPrioritySequence != null, Expected(true) }, tab: 1);

        Println("IComparer<TPriority> Comparer");
        PrintTupleln(new object[] { "NormalPrioritySequence.Comparer.Compare(-1, 1)", NormalPrioritySequence.Comparer.Compare(-1, 1), Expected("-1|2") }, tab: 1);
        PrintTupleln(new object[] { "NormalPrioritySequence.Comparer.Compare(0, 0)", NormalPrioritySequence.Comparer.Compare(0, 0), Expected("0|0") }, tab: 1);
        PrintTupleln(new object[] { "NormalPrioritySequence.Comparer.Compare(1, -1)", NormalPrioritySequence.Comparer.Compare(1, -1), Expected("1|-2") }, tab: 1);

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
        foreach (var i in testInputData) NormalPrioritySequence.Enqueue(i.Item1, i.Item2);

        PrintTupleln(new object[] { "NormalPrioritySequence.Count", NormalPrioritySequence.Count, Expected(10) }, tab: 1);
        Println("IReadOnlyList<(TElement element, TPriority priority)> OrderedElements + foreach");
        foreach (var i in NormalPrioritySequence.OrderedElements)
        {
            PrintTupleln(new object[] { i.Item1, i.Item2 }, tab: 1);
        }
        Println();

        Println("Dequeue");
        PrintTupleln(new object[] { "NormalPrioritySequence.Dequeue()", NormalPrioritySequence.Dequeue(), Expected("A|J") }, tab: 1);
        PrintTupleln(new object[] { "NormalPrioritySequence.Dequeue()", NormalPrioritySequence.Dequeue(), Expected("B|I") }, tab: 1);
        PrintTupleln(new object[] { "NormalPrioritySequence.Dequeue()", NormalPrioritySequence.Dequeue(), Expected("C|H") }, tab: 1);
        Println("TElement this[int index]");
        for (var i = 0; i < NormalPrioritySequence.Count; ++i)
        {
            PrintTupleln(new object[] { NormalPrioritySequence[i] }, tab: 1);
        }
        Println();

        Println("NormalPrioritySequence.RemoveAt(2)");
        NormalPrioritySequence.RemoveAt(2);
        Println("NormalPrioritySequence.RemoveAt(4)");
        NormalPrioritySequence.RemoveAt(4);
        Println("NormalPrioritySequence.RemoveAt(2)");
        NormalPrioritySequence.RemoveAt(2);
        Println("IReadOnlyList<(TElement element, TPriority priority)> OrderedElements + foreach");
        foreach (var i in NormalPrioritySequence.OrderedElements)
        {
            PrintTupleln(new object[] { i.Item1, i.Item2 }, tab: 1);
        }
        Println();

        PrintTupleln(new object[] { "NormalPrioritySequence.Peek()", NormalPrioritySequence.Peek(), Expected("D|G") });
        PrintTupleln(new object[] { "NormalPrioritySequence.TryDequeue(out e, out p), e, p", NormalPrioritySequence.TryDequeue(out e, out p), e, p });
        PrintTupleln(new object[] { "NormalPrioritySequence.TryPeek(out e, out p), e, p", NormalPrioritySequence.TryPeek(out e, out p), e, p });
        Println("NormalPrioritySequence.Clear()");
        NormalPrioritySequence.Clear();
        PrintTupleln(new object[] { "NormalPrioritySequence.Count", NormalPrioritySequence.Count, Expected(0) });
        PrintTupleln(new object[] { "NormalPrioritySequence.TryDequeue(out e, out p), e, p", NormalPrioritySequence.TryDequeue(out e, out p), e, p });
        PrintTupleln(new object[] { "NormalPrioritySequence.TryPeek(out e, out p), e, p", NormalPrioritySequence.TryPeek(out e, out p), e, p });
    }

}