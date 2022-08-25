using System;

namespace GameKit
{
    public static partial class Utility
    {
        public static partial class Json
        {
            private static IJsonHelper s_JsonHelper = null;

            public static void SetJsonHelper(IJsonHelper jsonHelper)
            {
                s_JsonHelper = jsonHelper;
            }

            public static string ToJson(object obj)
            {
                if (s_JsonHelper == null)
                {
                    throw new GameKitException("JSON helper is invalid.");
                }

                try
                {
                    return s_JsonHelper.ToJson(obj);
                }
                catch (Exception exception)
                {
                    if (exception is GameKitException)
                    {
                        throw;
                    }

                    throw new GameKitException(Text.Format("Can not convert to JSON with exception '{0}'.", exception), exception);
                }
            }

            public static T ToObject<T>(string json)
            {
                if (s_JsonHelper == null)
                {
                    throw new GameKitException("JSON helper is invalid.");
                }

                try
                {
                    return s_JsonHelper.ToObject<T>(json);
                }
                catch (Exception exception)
                {
                    if (exception is GameKitException)
                    {
                        throw;
                    }

                    throw new GameKitException(Text.Format("Can not convert to object with exception '{0}'.", exception), exception);
                }
            }

            public static object ToObject(Type objectType, string json)
            {
                if (s_JsonHelper == null)
                {
                    throw new GameKitException("JSON helper is invalid.");
                }

                if (objectType == null)
                {
                    throw new GameKitException("Object type is invalid.");
                }

                try
                {
                    return s_JsonHelper.ToObject(objectType, json);
                }
                catch (Exception exception)
                {
                    if (exception is GameKitException)
                    {
                        throw;
                    }

                    throw new GameKitException(Text.Format("Can not convert to object with exception '{0}'.", exception), exception);
                }
            }
        }
    }
}
