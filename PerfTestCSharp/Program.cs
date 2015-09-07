using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PerfTestCSharp
{
    class Program
    {
        const int N = 10000;

        static void Main(string[] args)
        {
            ArrayTest();
            UnsafeTest();
            ListTest();
            var m1 = Measure(ArrayTest);
            Console.WriteLine("C# array bubble sort = {0}ms", m1);
            var m2 = Measure(UnsafeTest);
            Console.WriteLine("C-style (unsafe) array bubble sort = {0}ms", m2);
            var m3 = Measure(ListTest);
            Console.WriteLine("C# List bubble sort = {0}ms", m3);
        }

        static long Measure(Action f, int n = 100)
        {
            var sw = System.Diagnostics.Stopwatch.StartNew();
            for (int i = 0; i < n; i++)
            {
                f();
            }
            return sw.ElapsedMilliseconds / n;
        }

        static void ArrayTest()
        {
            var m = new int[N];
            for (int i = 0; i < m.Length; i++)
            {
                m[i] = i;
            }
            ArraySort(m);

        }

        static void ArraySort(int[] m)
        {
            for (int i = 0; i < m.Length - 1; i++)
                for (int j = i + 1; j < m.Length; j++)
                {
                    if (m[i] < m[j])
                    {
                        int tmp = m[i];
                        m[i] = m[j];
                        m[j] = tmp;
                    }
                }
        }
        static void ListTest()
        {
            var m = new List<int>(N);
            for (int i = 0; i < N; i++)
            {
                m.Add(i);
            }
            ListSort(m);

        }

        static void ListSort(List<int> m)
        {
            var n = m.Count;
            for (int i = 0; i < n - 1; i++)
                for (int j = i + 1; j < n; j++)
                {
                    if (m[i] < m[j])
                    {
                        int tmp = m[i];
                        m[i] = m[j];
                        m[j] = tmp;
                    }
                }
        }

        static unsafe void UnsafeTest()
        {
            var m = new int[N];
            fixed(int* ptr = &m[0])
            {
                for (int i = 0; i < N; i++)
                {
                    ptr[i] = i;
                }
                UnsafeSort(ptr, N);
            }
        }

        static unsafe void UnsafeSort(int* m, int n)
        {
            for (int i = 0; i < n - 1; i++)
                for (int j = i + 1; j < n; j++)
                {
                    if (m[i] < m[j])
                    {
                        int tmp = m[i];
                        m[i] = m[j];
                        m[j] = tmp;
                    }
                }
        }


    }
}
