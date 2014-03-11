namespace Ninject.Integration
{
    using Ninject.Extensions.Interception;

    public class CountInterceptor : SimpleInterceptor
    {
        public CountInterceptor()
            : this(0)
        {
        }

        public CountInterceptor(int initialValue)
        {
            Count = initialValue;
        }

        public static int Count { get; set; }

        public static void Reset()
        {
            Count = 0;
        }

        protected override void BeforeInvoke(IInvocation invocation)
        {
            Count++;
        }
    }
}