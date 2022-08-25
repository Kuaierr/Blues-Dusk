using System;
using System.Text;

namespace GameKit
{
    public static partial class Utility
    {
        public static partial class Text
        {
            
            private static ITextHelper s_TextHelper = null;

            public static void SetTextHelper(ITextHelper textHelper)
            {
                s_TextHelper = textHelper;
            }

            public static string Format<T>(string format, T arg)
            {
                if (format == null)
                {
                    throw new GameKitException("Format is invalid.");
                }

                if (s_TextHelper == null)
                {
                    return string.Format(format, arg);
                }

                return s_TextHelper.Format(format, arg);
            }

            public static string Format<T1, T2>(string format, T1 arg1, T2 arg2)
            {
                if (format == null)
                {
                    throw new GameKitException("Format is invalid.");
                }

                if (s_TextHelper == null)
                {
                    return string.Format(format, arg1, arg2);
                }

                return s_TextHelper.Format(format, arg1, arg2);
            }

            public static string Format<T1, T2, T3>(string format, T1 arg1, T2 arg2, T3 arg3)
            {
                if (format == null)
                {
                    throw new GameKitException("Format is invalid.");
                }

                if (s_TextHelper == null)
                {
                    return string.Format(format, arg1, arg2, arg3);
                }

                return s_TextHelper.Format(format, arg1, arg2, arg3);
            }

            public static string Format<T1, T2, T3, T4>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4)
            {
                if (format == null)
                {
                    throw new GameKitException("Format is invalid.");
                }

                if (s_TextHelper == null)
                {
                    return string.Format(format, arg1, arg2, arg3, arg4);
                }

                return s_TextHelper.Format(format, arg1, arg2, arg3, arg4);
            }

            public static string Format<T1, T2, T3, T4, T5>(string format, T1 arg1, T2 arg2, T3 arg3, T4 arg4, T5 arg5)
            {
                if (format == null)
                {
                    throw new GameKitException("Format is invalid.");
                }

                if (s_TextHelper == null)
                {
                    return string.Format(format, arg1, arg2, arg3, arg4, arg5);
                }

                return s_TextHelper.Format(format, arg1, arg2, arg3, arg4, arg5);
            }

            public static string Format(string format, params object[] args)
            {
                if (format == null)
                {
                    throw new GameKitException("Format is invalid.");
                }

                if (args == null)
                {
                    throw new GameKitException("Args is invalid.");
                }

                if (s_TextHelper == null)
                {
                    return string.Format(format, args);
                }
                
                return s_TextHelper.Format(format, args);
            }
        }
    }
}
