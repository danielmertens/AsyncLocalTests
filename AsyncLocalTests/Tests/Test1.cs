using System;
using System.Threading.Tasks;

namespace AsyncLocalTests.Tests
{
    class Test1
    {
        /*
         * For this test I want to figure out the scope of async local.
         * Does the object bubble down when a new Task is entered?
         * Does the object bubble up when exiting the Task it was created in?
         */
        public async Task Run()
        {
            await Child1();

            // Result: true. Bubble up does not work
            if (ContextAccessor.Instance.Context == null)
            {
                Console.WriteLine("The context is null. It did not bubble up.");
            }
            else
            {
                Console.WriteLine("The context is NOT null. It did bubble up.");
                Console.WriteLine($"Value1 = {ContextAccessor.Instance.Context.Value1}");
            }
        }

        public async Task Child1()
        {
            // create context
            Console.WriteLine("Creating context");
            ContextAccessor.Instance.Context = new Context();
            ContextAccessor.Instance.Context.Value1 = "Test 1";
            Console.WriteLine($"Initial value1 = {ContextAccessor.Instance.Context.Value1}");

            await Child2();
            await RecursiveChild(0, 10);
        }

        public async Task Child2()
        {
            // Result: false. Bubble down works
            if (ContextAccessor.Instance.Context == null)
            {
                Console.WriteLine("The context is null. It did not bubble down.");
            }
            else
            {
                Console.WriteLine("The context is NOT null. It did bubble down.");
                Console.WriteLine($"Value1 = {ContextAccessor.Instance.Context.Value1}");
            }
            await Task.Delay(100);
        }

        // This test should not be necessary but good to check.
        public async Task RecursiveChild(int current, int max)
        {
            if (current == max)
            {
                if (ContextAccessor.Instance.Context == null)
                {
                    Console.WriteLine("Context is null");
                }
                else
                {
                    Console.WriteLine($"Recursive value1 = {ContextAccessor.Instance.Context.Value1}");
                }
            }
            else
            {
                await (RecursiveChild(++current, max));
            }
        }
    }
}
