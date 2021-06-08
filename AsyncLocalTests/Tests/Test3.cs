using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AsyncLocalTests.Tests
{
    class Test3
    {
        /*
         * Are context's seperate?
         */
        public async Task Run()
        {
            // Starting multiple threads
            var numbOfTasks = 5;
            var tasks = new List<Task>();

            for (int i = 0; i < numbOfTasks; i++)
            {
                tasks.Add(Counting());
            }

            Task.WaitAll(tasks.ToArray());
        }

        public async Task Counting()
        {
            Console.WriteLine("Creating context");
            ContextAccessor.Instance.Context = new Context();
            ContextAccessor.Instance.Context.Value1 = "Test 3";
            Console.WriteLine($"Initial value1 = {ContextAccessor.Instance.Context.Value1}");
            Console.WriteLine($"Initial count = {ContextAccessor.Instance.Context.Counter}");

            var outerCounter = 0;
            var random = new Random();

            for (int i = 0; i < 10; i++)
            {
                var numb = random.Next(0, 1000);
                outerCounter += numb;
                ContextAccessor.Instance.Context.Counter += numb;
                await Task.Delay(random.Next(100, 600));
            }

            // Result: true. Values are always the same
            if (outerCounter == ContextAccessor.Instance.Context.Counter)
            {
                Console.WriteLine("Counters are the same");
            }
            else
            {
                Console.WriteLine($"Counters are not the same {outerCounter} - {ContextAccessor.Instance.Context.Counter}");
            }
        }
    }
}
