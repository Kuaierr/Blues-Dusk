namespace GameKit
{
    public static partial class Utility
    {
        public static partial class Text
        {
            public interface ITextHelper
            {
                string Format<T>(string format, T arg);

                string Format<T1, T2>(string format, T1 arg1, T2 arg2);

                string Format<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3);

                string Format<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4);

                string Format<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5);

                string Format(string format, params object[] args);
            }
        }
    }
}
