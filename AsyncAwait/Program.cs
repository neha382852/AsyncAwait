using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncAwait
{
    class Program
    {
        static void Main(string[] args)
        {
            CallingMethod();
            Console.ReadKey();
        }

        public static async void CallingMethod()
        {
            Task<int> task = Method1();
            int count = await task; //wait till method1 executes
            Method1(); //Method2&3 can execute irrespective of Method1 completion
            Method2();
            Method3(count);
        }

        private static async Task<int> Method1()
        {
            int count = 0;
            await Task.Run(() => {
                for (int i = 0; i < 5; i++)
                {
                    Console.WriteLine("Method 1: " + i);
                    Thread.Sleep(1000);
                    count++;
                }
            });
            return count;
        }

        private static void Method2()
        {
            for (int i = 0; i < 5; i++)
                Console.WriteLine("Method 2: " + i);
        }

        private static void Method3(int Count)
        {
            Console.WriteLine("Count is " + Count);
        }
    }
}

//Flow: Task completes first
//Method1 starts execution, waits for 1 second per loop iteration. Until then, Method2 and 3 completes its task.
//Try doing Thread.Sleep(1) and see changes in output.