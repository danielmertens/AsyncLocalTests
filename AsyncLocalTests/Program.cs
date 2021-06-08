using AsyncLocalTests.Tests;
using System;
using System.Threading.Tasks;

namespace AsyncLocalTests
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("========== Run Test 1 ==========");
            await new Test1().Run();
            Console.WriteLine("\n\n========== Run Test 2 ==========");
            await new Test2().Run();
            Console.WriteLine("\n\n========== Run Test 3 ==========");
            await new Test3().Run();
            Console.WriteLine("\n\n========== Run Test 4 ==========");
            await new Test4().Run();
            Console.WriteLine("\n\n========== Run Test 5 ==========");
            await new Test5().Run();
            Console.WriteLine("\n\n========== Run Test 6 ==========");
            new Test6().Run();
            Console.WriteLine("\n\n========== Run Test 7 ==========");
            new Test6().Run();
            Console.WriteLine("\n\n========== Run Test 8 ==========");
            new Test7().Run();
        }
    }
}
