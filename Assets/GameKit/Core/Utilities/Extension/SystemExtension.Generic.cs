using System.Text;
using System;
using System.Linq;
using System.Collections.Generic;

namespace GameKit
{
    public static partial class SystemExtension
    {
        private static StringBuilder m_CachedStringBuilder = new StringBuilder();
        public static void ForEach<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, Action<KeyValuePair<TKey, TValue>> action)
        {
            if (action == null || dictionary.Count == 0) return;
            for (int i = 0; i < dictionary.Count; i++)
            {
                var item = dictionary.ElementAt(i);
                action(item);
            }
        }

        public static string ToLog<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
        {
            Clear();
            m_CachedStringBuilder.Append("<b>Dictionary Content:</b>\n");
            foreach (var keyValuePair in dictionary)
                m_CachedStringBuilder.Append(string.Format("<b>{0}</b>: {1}\n", keyValuePair.Key, keyValuePair.Value));
            return m_CachedStringBuilder.ToString().RemoveLast();
        }

        public static string ToLog<T>(this List<T> list)
        {
            Clear();
            m_CachedStringBuilder.Append("<b>List Content:</b>\n");
            for (int i = 0; i < list.Count; i++)
                m_CachedStringBuilder.Append(string.Format("<b>{0}</b>\n", list[i]));
            return m_CachedStringBuilder.ToString().RemoveLast();
        }

        public static void Clear()
        {
            if (m_CachedStringBuilder == null)
                m_CachedStringBuilder = new StringBuilder();
            m_CachedStringBuilder.Clear();
        }
    }
}
