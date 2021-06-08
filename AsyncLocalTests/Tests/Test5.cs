using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncLocalTests.Tests
{
    class Test5
    {
        /*
         * How is the interaction with the ThreadPool?
         */
        public async Task Run()
        {
            Console.WriteLine("Creating context");
            ContextAccessor.Instance.Context = new Context();
            ContextAccessor.Instance.Context.Value1 = "Test 5";
            Console.WriteLine($"Initial value1 = {ContextAccessor.Instance.Context.Value1}");

            ThreadPool.QueueUserWorkItem(ThreadPoolProc);
            await Task.Delay(1000);

            // Result: Value is changed from proc.
            Console.WriteLine($"After Proc value1 = {ContextAccessor.Instance.Context.Value1}");
        }

        void ThreadPoolProc(Object stateInfo)
        {
            Thread.Sleep(200);

            // Result: False. Contexts persists through ThreadPool.
            if (ContextAccessor.Instance.Context == null)
            {
                Console.WriteLine("Context is null. It doesn't persist through the ThreadPool.");
            }
            else
            {
                Console.WriteLine($"In Proc value1 = {ContextAccessor.Instance.Context.Value1}");
                ContextAccessor.Instance.Context.Value1 = "Test 5 - From proc";
            }
        }
    }
}
