﻿using System;
using GameKit.DataStructure;

namespace GameKit.ObjectPool
{
    public abstract class ObjectPoolBase
    {
        private readonly string m_Name;

        public ObjectPoolBase() : this(null)
        {
            
        }

        public ObjectPoolBase(string name)
        {
            m_Name = name ?? string.Empty;
        }

        public string Name
        {
            get
            {
                return m_Name;
            }
        }

        public string FullName
        {
            get
            {
                return new TypeNamePair(ObjectType, m_Name).ToString();
            }
        }

        public abstract Type ObjectType
        {
            get;
        }

        public abstract int Count
        {
            get;
        }

        public abstract int CanReleaseCount
        {
            get;
        }

        public abstract bool AllowMultiSpawn
        {
            get;
        }

        public abstract float AutoReleaseInterval
        {
            get;
            set;
        }

        public abstract int Capacity
        {
            get;
            set;
        }

        public abstract float ExpireTime
        {
            get;
            set;
        }

        public abstract int Priority
        {
            get;
            set;
        }

        public abstract void Release();

        public abstract void Release(int toReleaseCount);

        public abstract void ReleaseAllUnused();

        public abstract ObjectInfo[] GetAllObjectInfos();

        internal abstract void Update(float elapseSeconds, float realElapseSeconds);

        internal abstract void Shutdown();
    }
}
