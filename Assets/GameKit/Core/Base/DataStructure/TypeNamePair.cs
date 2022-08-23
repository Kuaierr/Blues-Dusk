using System;
using System.Runtime.InteropServices;

namespace GameKit.DataStructure
{
    [StructLayout(LayoutKind.Auto)]
    internal struct TypeNamePair : IEquatable<TypeNamePair>
    {
        private readonly Type m_Type;
        private readonly string m_Name;

        public TypeNamePair(Type type) : this(type, string.Empty)
        {
            
        }

        public TypeNamePair(Type type, string name)
        {
            if (type == null)
            {
                throw new GameKitException("Type is invalid.");
            }

            m_Type = type;
            m_Name = name ?? string.Empty;
        }

        public Type Type
        {
            get
            {
                return m_Type;
            }
        }

        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        public override string ToString()
        {
            if (m_Type == null)
            {
                throw new GameKitException("Type is invalid.");
            }

            string typeName = m_Type.FullName;
            return string.IsNullOrEmpty(m_Name) ? typeName : string.Format("{0}.{1}",typeName, m_Name);
            // return string.IsNullOrEmpty(m_Name) ? typeName : Utility.Text.Format("{0}.{1}", typeName, m_Name);
        }

        public override int GetHashCode()
        {
            return m_Type.GetHashCode() ^ m_Name.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return obj is TypeNamePair && Equals((TypeNamePair)obj);
        }

        public bool Equals(TypeNamePair value)
        {
            return m_Type == value.m_Type && m_Name == value.m_Name;
        }

        public static bool operator ==(TypeNamePair a, TypeNamePair b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(TypeNamePair a, TypeNamePair b)
        {
            return !(a == b);
        }
    }
}
