//------------------------------------------------------------
// Game Framework
// Copyright © 2013-2021 Jiang Yin. All rights reserved.
// Homepage: https://gameframework.cn/
// Feedback: mailto:ellan@gameframework.cn
//------------------------------------------------------------

using System;

namespace GameKit
{
    public static partial class Utility
    {
        public static partial class Json
        {
            public interface IJsonHelper
            {
                string ToJson(object obj);

                T ToObject<T>(string json);

                object ToObject(Type objectType, string json);
            }
        }
    }
}
