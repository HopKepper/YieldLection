using System.Collections;

namespace YeildLection;
public class Programm
{
    static void Main()
    {
        //Step1-------------------------------------------------------
        IEnumerator intsEnumerator = GetInts(); // print nothing
        Console.WriteLine("...");                    // print "..."

        intsEnumerator.MoveNext();                   // print "first"
        Console.WriteLine(intsEnumerator.Current);   // print 1
        Console.ReadKey();
        //------------------------------------------------------------

        PrintFibonacci(5);
        Console.ReadKey();
    //return value = generator----------------------------------------
        IEnumerable enumerable = GetFibonacci(5);
        IEnumerator enumerator = enumerable.GetEnumerator();

        Console.WriteLine(enumerable == enumerator);
        Console.ReadKey();
    }

    #region IEnumerator 
    static IEnumerator GetInts()
    {
        Console.WriteLine("first");
        yield return 1;

        Console.WriteLine("second");
        yield return 2;
    }

    IEnumerator GenerateMultiplicationTable(int maxValue)
    {
        for (int i = 2; i <= 10; i++)
        {
            for (int j = 2; j <= 10; j++)
            {
                int result = i * j;

                if (result > maxValue)
                    yield break;

                yield return result;
            }
        }
    }
    #endregion

    #region IEnumerable
    static void PrintFibonacci(int value)
    {
        Console.WriteLine("Fibonacci numbers:");

        foreach (int number in GetFibonacci(value))
        {
            Console.WriteLine(number);
        }
    }

    static IEnumerable GetFibonacci(int maxValue)
    {
        int previous = 0;
        int current = 1;

        while (current <= maxValue)
        {
            yield return current;

            int newCurrent = previous + current;
            previous = current;
            current = newCurrent;
        }
    }

    //-----------------------------------------------
    int _rangeStart;
    int _rangeEnd;

    public void TestIEnumerableYield()
    {
        IEnumerable polymorphRange = GetRange();

        _rangeStart = 0;
        _rangeEnd = 3;

        Console.WriteLine(string.Join(' ', polymorphRange)); // 0 1 2 3

        _rangeStart = 5;
        _rangeEnd = 7;

        Console.WriteLine(string.Join(' ', polymorphRange)); // 5 6 7
    }

    IEnumerable GetRange()
    {
        for (int i = _rangeStart; i <= _rangeEnd; i++)
        {
            yield return i;
        }
    }
    #endregion
}
