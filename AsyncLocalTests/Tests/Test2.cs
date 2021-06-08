using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncLocalTests.Tests
{
    public class Test2
    {
        /*
         * Does the context survive a sleep operation?
         */
        public async Task Run()
        {
            Console.WriteLine("Creating context");
            ContextAccessor.Instance.Context = new Context();
            ContextAccessor.Instance.Context.Value1 = "Test 2";
            Console.WriteLine($"Initial value1 = {ContextAccessor.Instance.Context.Value1}");

            await Task.Delay(500);

            // Result: Value still the same.
            Console.WriteLine($"After delay value1 = {ContextAccessor.Instance.Context.Value1}");

            Thread.Sleep(500);

            // Result: Value still the same.
            Console.WriteLine($"After sleep value1 = {ContextAccessor.Instance.Context.Value1}");
        }
    }
}
