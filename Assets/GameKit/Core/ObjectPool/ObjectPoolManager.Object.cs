using System;


namespace GameKit.ObjectPool
{
    internal sealed partial class ObjectPoolManager : GameKitModule, IObjectPoolManager
    {
        private sealed class Object<T> : IReference where T : ObjectBase
        {
            private T m_Object;
            private int m_SpawnCount;

            public Object()
            {
                m_Object = null;
                m_SpawnCount = 0;
            }

            public string Name
            {
                get
                {
                    return m_Object.Name;
                }
            }

            public bool Locked
            {
                get
                {
                    return m_Object.Locked;
                }
                internal set
                {
                    m_Object.Locked = value;
                }
            }

            public int Priority
            {
                get
                {
                    return m_Object.Priority;
                }
                internal set
                {
                    m_Object.Priority = value;
                }
            }

            public bool CustomCanReleaseFlag
            {
                get
                {
                    return m_Object.CustomCanReleaseFlag;
                }
            }

            public DateTime LastUseTime
            {
                get
                {
                    return m_Object.LastUseTime;
                }
            }

            public bool IsInUse
            {
                get
                {
                    return m_SpawnCount > 0;
                }
            }

            public int SpawnCount
            {
                get
                {
                    return m_SpawnCount;
                }
            }

            public static Object<T> Create(T obj, bool spawned)
            {
                if (obj == null)
                {
                    throw new GameKitException("Object is invalid.");
                }

                Object<T> internalObject = ReferencePool.Acquire<Object<T>>();
                internalObject.m_Object = obj;
                internalObject.m_SpawnCount = spawned ? 1 : 0;
                if (spawned)
                {
                    obj.OnSpawn();
                }

                return internalObject;
            }

            public void Clear()
            {
                m_Object = null;
                m_SpawnCount = 0;
            }

            public T Peek()
            {
                return m_Object;
            }

            public T Spawn()
            {
                m_SpawnCount++;
                m_Object.LastUseTime = DateTime.UtcNow;
                m_Object.OnSpawn();
                return m_Object;
            }

            public void Unspawn()
            {
                m_Object.OnUnspawn();
                m_Object.LastUseTime = DateTime.UtcNow;
                m_SpawnCount--;
                if (m_SpawnCount < 0)
                {
                    throw new GameKitException(Utility.Text.Format("Object '{0}' spawn count is less than 0.", Name));
                }
            }

            public void Release(bool isShutdown)
            {
                m_Object.Release(isShutdown);
                ReferencePool.Release(m_Object);
            }
        }
    }
}
