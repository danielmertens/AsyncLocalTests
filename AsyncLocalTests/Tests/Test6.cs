using System;
using System.Threading;

namespace AsyncLocalTests.Tests
{
    // Taken from: https://stackoverflow.com/a/49232776
    class Test6
    {
        private static readonly AsyncLocal<string> AsyncLocalContext = new AsyncLocal<string>();

        public async void Run()
        {
            Child();

            // Result: True. Context is null.
            if (AsyncLocalContext.Value == null)
            {
                Console.WriteLine("Context is null");
            }
            else
            {
                Console.WriteLine("Context in Run: " + AsyncLocalContext.Value);
            }
        }

        private async void Child()
        {
            AsyncLocalContext.Value = "No surprise";
            WrapperAsync("surprise!");
            Console.WriteLine("Child: " + AsyncLocalContext.Value);
        }

        private async void WrapperAsync(string text)
        {
            Console.WriteLine("WrapperAsync before: " + AsyncLocalContext.Value);
            AsyncLocalContext.Value = text;
            Console.WriteLine("WrapperAsync after: " + AsyncLocalContext.Value);
        }
    }
}
