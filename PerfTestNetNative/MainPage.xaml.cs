using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace PerfTestNetNative
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        const int N = 10000;

        public MainPage()
        {
            this.InitializeComponent();
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
            fixed (int* ptr = &m[0])
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

        private async void button_Click(object sender, RoutedEventArgs e)
        {
            button.IsEnabled = false;
            ArrayTest();
            UnsafeTest();
            ListTest();
            var m1 =  await Task.Run(() => Measure(ArrayTest));
            listView.Items.Add(string.Format("C# array bubble sort = {0}ms", m1));
            var m2 = await Task.Run(() => Measure(UnsafeTest));
            listView.Items.Add(string.Format("C-style (unsafe) array bubble sort = {0}ms", m2));
            var m3 = await Task.Run(() => Measure(ListTest));
            listView.Items.Add(string.Format("C# List bubble sort = {0}ms", m3));
            button.IsEnabled = true;
        }
    }
}
