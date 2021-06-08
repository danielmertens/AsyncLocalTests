using System.Threading;

namespace AsyncLocalTests
{
    // Adapted from HttpContextAccessor in AspNetCore
    // https://github.com/aspnet/HttpAbstractions/blob/master/src/Microsoft.AspNetCore.Http/HttpContextAccessor.cs
    class ContextAccessor
    {
        private static AsyncLocal<ContextHolder> _contextCurrent 
            = new AsyncLocal<ContextHolder>();

        private static ContextAccessor _instance;

        public static ContextAccessor Instance
        {
            get {
                if (_instance == null) _instance = new ContextAccessor();
                return _instance;
            }
        }

        public Context Context
        {
            get
            {
                return _contextCurrent.Value?.Context;
            }
            set
            {
                var holder = _contextCurrent.Value;
                if (holder != null)
                {
                    // Clear current HttpContext trapped in the AsyncLocals, as its done.
                    holder.Context = null;
                }

                if (value != null)
                {
                    // Use an object indirection to hold the Context in the AsyncLocal,
                    // so it can be cleared in all ExecutionContexts when its cleared.
                    _contextCurrent.Value = new ContextHolder { Context = value };
                }
            }
        }

        private class ContextHolder
        {
            public Context Context;
        }
    }
}
