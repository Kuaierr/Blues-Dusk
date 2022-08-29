using System;

namespace GameKit
{
    public abstract class Variable<T> : Variable
    {
        private T m_Value;

        public Variable()
        {
            m_Value = default(T);
        }

        public override Type Type
        {
            get
            {
                return typeof(T);
            }
        }

        public T Value
        {
            get
            {
                return m_Value;
            }
            set
            {
                m_Value = value;
            }
        }

        public override object GetValue()
        {
            return m_Value;
        }

        public override void SetValue(object value)
        {
            m_Value = (T)value;
        }

        public override void Clear()
        {
            m_Value = default(T);
        }

        public override string ToString()
        {
            return (m_Value != null) ? m_Value.ToString() : "<Null>";
        }
    }
}
