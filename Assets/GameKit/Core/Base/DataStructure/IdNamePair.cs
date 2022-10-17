using System;
using System.Runtime.InteropServices;


[StructLayout(LayoutKind.Auto)]
public struct IdNamePair : IEquatable<IdNamePair>
{
    private readonly int m_Id;
    private readonly string m_Name;

    public IdNamePair(int id)
        : this(id, string.Empty)
    {
    }

    public IdNamePair(int id, string name)
    {
        m_Id = id;
        m_Name = name ?? string.Empty;
    }

    public int Id
    {
        get
        {
            return m_Id;
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
        string idName = m_Id.ToString();
        return string.IsNullOrEmpty(m_Name) ? idName : string.Format("{0}.{1}", m_Name, idName);
    }

    public override int GetHashCode()
    {
        return m_Id.GetHashCode() ^ m_Name.GetHashCode();
    }

    public override bool Equals(object obj)
    {
        return obj is IdNamePair && Equals((IdNamePair)obj);
    }

    public bool Equals(IdNamePair value)
    {
        return m_Id == value.m_Id && m_Name == value.m_Name;
    }

    public static bool operator ==(IdNamePair a, IdNamePair b)
    {
        return a.Equals(b);
    }

    public static bool operator !=(IdNamePair a, IdNamePair b)
    {
        return !(a == b);
    }
}

