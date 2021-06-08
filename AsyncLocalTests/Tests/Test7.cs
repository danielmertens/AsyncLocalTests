using System;

namespace AsyncLocalTests.Tests
{
    class Test7
    {
        /*
         * Same as Test 1 but without any async stuff.
         */
        public async void Run()
        {
            Child1();

            // Result: false. If not in an async context bubble up can occur.
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

        public void Child1()
        {
            // create context
            Console.WriteLine("Creating context");
            ContextAccessor.Instance.Context = new Context();
            ContextAccessor.Instance.Context.Value1 = "Test 7";
            Console.WriteLine($"Initial value1 = {ContextAccessor.Instance.Context.Value1}");

            Child2();
        }

        public void Child2()
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
        }
    }
}
