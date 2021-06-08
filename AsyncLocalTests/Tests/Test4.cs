using System;
using System.Threading.Tasks;

namespace AsyncLocalTests.Tests
{
    class Test4
    {
        /*
         * What happens if context is set in child?
         */
        public async Task Run()
        {
            Console.WriteLine("Creating context");
            ContextAccessor.Instance.Context = new Context();
            ContextAccessor.Instance.Context.Value1 = "Test 4 parent";
            Console.WriteLine($"Initial value1 = {ContextAccessor.Instance.Context.Value1}");

            Child1();

            // Result: Value1 was changed.
            Console.WriteLine($"After child1 value1 = {ContextAccessor.Instance.Context.Value1}");

            Child2();

            // Result: true. Context is null!
            if (ContextAccessor.Instance.Context == null)
            {
                Console.WriteLine("Context is null. Set in child destroyed the original context and is unretrievable");
            }
            else
            {
                Console.WriteLine($"After child2 value1 = {ContextAccessor.Instance.Context.Value1}");
            }
        }

        public async void Child1()
        {
            ContextAccessor.Instance.Context.Value1 = "Test 4 Child1";
            Console.WriteLine($"In child1 value1 = {ContextAccessor.Instance.Context.Value1}");
        }

        public async void Child2()
        {
            Console.WriteLine("Creating context");
            ContextAccessor.Instance.Context = new Context();
            ContextAccessor.Instance.Context.Value1 = "Test 4 Child2";
        }
    }
}
