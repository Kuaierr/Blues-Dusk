﻿using System;
using System.Collections.Generic;
using GameKit.DataStructure;


namespace GameKit.ObjectPool
{
    internal sealed partial class ObjectPoolManager : GameKitModule, IObjectPoolManager
    {
        private sealed class ObjectPool<T> : ObjectPoolBase, IObjectPool<T> where T : ObjectBase
        {
            private readonly GameKitMultiDictionary<string, Object<T>> m_Objects;
            private readonly Dictionary<object, Object<T>> m_ObjectMap;
            private readonly ReleaseObjectFilterCallback<T> m_DefaultReleaseObjectFilterCallback;
            private readonly List<T> m_CachedCanReleaseObjects;
            private readonly List<T> m_CachedToReleaseObjects;
            private readonly bool m_AllowMultiSpawn;
            private float m_AutoReleaseInterval;
            private int m_Capacity;
            private float m_ExpireTime;
            private int m_Priority;
            private float m_AutoReleaseTime;

            public ObjectPool(string name, bool allowMultiSpawn, float autoReleaseInterval, int capacity, float expireTime, int priority)
                : base(name)
            {
                m_Objects = new GameKitMultiDictionary<string, Object<T>>();
                m_ObjectMap = new Dictionary<object, Object<T>>();
                m_DefaultReleaseObjectFilterCallback = DefaultReleaseObjectFilterCallback;
                m_CachedCanReleaseObjects = new List<T>();
                m_CachedToReleaseObjects = new List<T>();
                m_AllowMultiSpawn = allowMultiSpawn;
                m_AutoReleaseInterval = autoReleaseInterval;
                Capacity = capacity;
                ExpireTime = expireTime;
                m_Priority = priority;
                m_AutoReleaseTime = 0f;
            }

            public override Type ObjectType
            {
                get
                {
                    return typeof(T);
                }
            }

            public override int Count
            {
                get
                {
                    return m_ObjectMap.Count;
                }
            }

            public override int CanReleaseCount
            {
                get
                {
                    GetCanReleaseObjects(m_CachedCanReleaseObjects);
                    return m_CachedCanReleaseObjects.Count;
                }
            }

            public override bool AllowMultiSpawn
            {
                get
                {
                    return m_AllowMultiSpawn;
                }
            }

            public override float AutoReleaseInterval
            {
                get
                {
                    return m_AutoReleaseInterval;
                }
                set
                {
                    m_AutoReleaseInterval = value;
                }
            }

            public override int Capacity
            {
                get
                {
                    return m_Capacity;
                }
                set
                {
                    if (value < 0)
                    {
                        throw new GameKitException("Capacity is invalid.");
                    }

                    if (m_Capacity == value)
                    {
                        return;
                    }

                    m_Capacity = value;
                    Release();
                }
            }

            public override float ExpireTime
            {
                get
                {
                    return m_ExpireTime;
                }

                set
                {
                    if (value < 0f)
                    {
                        throw new GameKitException("ExpireTime is invalid.");
                    }

                    if (ExpireTime == value)
                    {
                        return;
                    }

                    m_ExpireTime = value;
                    Release();
                }
            }

            public override int Priority
            {
                get
                {
                    return m_Priority;
                }
                set
                {
                    m_Priority = value;
                }
            }

            public void Register(T obj, bool spawned)
            {
                if (obj == null)
                {
                    throw new GameKitException("Object is invalid.");
                }

                Object<T> internalObject = Object<T>.Create(obj, spawned);
                m_Objects.Add(obj.Name, internalObject);
                m_ObjectMap.Add(obj.Target, internalObject);

                if (Count > m_Capacity)
                {
                    Release();
                }
            }

            public bool CanSpawn()
            {
                return CanSpawn(string.Empty);
            }

            public bool CanSpawn(string name)
            {
                if (name == null)
                {
                    throw new GameKitException("Name is invalid.");
                }

                GameKitLinkedListRange<Object<T>> objectRange = default(GameKitLinkedListRange<Object<T>>);
                if (m_Objects.TryGetValue(name, out objectRange))
                {
                    foreach (Object<T> internalObject in objectRange)
                    {
                        if (m_AllowMultiSpawn || !internalObject.IsInUse)
                        {
                            return true;
                        }
                    }
                }

                return false;
            }

            public T Spawn()
            {
                return Spawn(string.Empty);
            }

            public T Spawn(string name)
            {
                if (name == null)
                {
                    throw new GameKitException("Name is invalid.");
                }

                GameKitLinkedListRange<Object<T>> objectRange = default(GameKitLinkedListRange<Object<T>>);
                if (m_Objects.TryGetValue(name, out objectRange))
                {
                    foreach (Object<T> internalObject in objectRange)
                    {
                        if (m_AllowMultiSpawn || !internalObject.IsInUse)
                        {
                            return internalObject.Spawn();
                        }
                    }
                }

                return null;
            }

            public void Unspawn(T obj)
            {
                if (obj == null)
                {
                    throw new GameKitException("Object is invalid.");
                }

                Unspawn(obj.Target);
            }

            public void Unspawn(object target)
            {
                if (target == null)
                {
                    throw new GameKitException("Target is invalid.");
                }

                Object<T> internalObject = GetObject(target);
                if (internalObject != null)
                {
                    internalObject.Unspawn();
                    if (Count > m_Capacity && internalObject.SpawnCount <= 0)
                    {
                        Release();
                    }
                }
                else
                {
                    throw new GameKitException(Utility.Text.Format("Can not find target in object pool '{0}', target type is '{1}', target value is '{2}'.", new TypeNamePair(typeof(T), Name), target.GetType().FullName, target));
                }
            }

            public void SetLocked(T obj, bool locked)
            {
                if (obj == null)
                {
                    throw new GameKitException("Object is invalid.");
                }

                SetLocked(obj.Target, locked);
            }

            public void SetLocked(object target, bool locked)
            {
                if (target == null)
                {
                    throw new GameKitException("Target is invalid.");
                }

                Object<T> internalObject = GetObject(target);
                if (internalObject != null)
                {
                    internalObject.Locked = locked;
                }
                else
                {
                    throw new GameKitException(Utility.Text.Format("Can not find target in object pool '{0}', target type is '{1}', target value is '{2}'.", new TypeNamePair(typeof(T), Name), target.GetType().FullName, target));
                }
            }

            public void SetPriority(T obj, int priority)
            {
                if (obj == null)
                {
                    throw new GameKitException("Object is invalid.");
                }

                SetPriority(obj.Target, priority);
            }

            public void SetPriority(object target, int priority)
            {
                if (target == null)
                {
                    throw new GameKitException("Target is invalid.");
                }

                Object<T> internalObject = GetObject(target);
                if (internalObject != null)
                {
                    internalObject.Priority = priority;
                }
                else
                {
                    throw new GameKitException(Utility.Text.Format("Can not find target in object pool '{0}', target type is '{1}', target value is '{2}'.", new TypeNamePair(typeof(T), Name), target.GetType().FullName, target));
                }
            }

            public bool ReleaseObject(T obj)
            {
                if (obj == null)
                {
                    throw new GameKitException("Object is invalid.");
                }

                return ReleaseObject(obj.Target);
            }

            public bool ReleaseObject(object target)
            {
                if (target == null)
                {
                    throw new GameKitException("Target is invalid.");
                }

                Object<T> internalObject = GetObject(target);
                if (internalObject == null)
                {
                    return false;
                }

                if (internalObject.IsInUse || internalObject.Locked || !internalObject.CustomCanReleaseFlag)
                {
                    return false;
                }

                m_Objects.Remove(internalObject.Name, internalObject);
                m_ObjectMap.Remove(internalObject.Peek().Target);

                internalObject.Release(false);
                ReferencePool.Release(internalObject);
                return true;
            }

            public override void Release()
            {
                Release(Count - m_Capacity, m_DefaultReleaseObjectFilterCallback);
            }

            public override void Release(int toReleaseCount)
            {
                Release(toReleaseCount, m_DefaultReleaseObjectFilterCallback);
            }

            public void Release(ReleaseObjectFilterCallback<T> releaseObjectFilterCallback)
            {
                Release(Count - m_Capacity, releaseObjectFilterCallback);
            }

            public void Release(int toReleaseCount, ReleaseObjectFilterCallback<T> releaseObjectFilterCallback)
            {
                if (releaseObjectFilterCallback == null)
                {
                    throw new GameKitException("Release object filter callback is invalid.");
                }

                if (toReleaseCount < 0)
                {
                    toReleaseCount = 0;
                }

                DateTime expireTime = DateTime.MinValue;
                if (m_ExpireTime < float.MaxValue)
                {
                    expireTime = DateTime.UtcNow.AddSeconds(-m_ExpireTime);
                }

                m_AutoReleaseTime = 0f;
                GetCanReleaseObjects(m_CachedCanReleaseObjects);
                List<T> toReleaseObjects = releaseObjectFilterCallback(m_CachedCanReleaseObjects, toReleaseCount, expireTime);
                if (toReleaseObjects == null || toReleaseObjects.Count <= 0)
                {
                    return;
                }

                foreach (T toReleaseObject in toReleaseObjects)
                {
                    ReleaseObject(toReleaseObject);
                }
            }

            public override void ReleaseAllUnused()
            {
                m_AutoReleaseTime = 0f;
                GetCanReleaseObjects(m_CachedCanReleaseObjects);
                foreach (T toReleaseObject in m_CachedCanReleaseObjects)
                {
                    ReleaseObject(toReleaseObject);
                }
            }

            public override ObjectInfo[] GetAllObjectInfos()
            {
                List<ObjectInfo> results = new List<ObjectInfo>();
                foreach (KeyValuePair<string, GameKitLinkedListRange<Object<T>>> objectRanges in m_Objects)
                {
                    foreach (Object<T> internalObject in objectRanges.Value)
                    {
                        results.Add(new ObjectInfo(internalObject.Name, internalObject.Locked, internalObject.CustomCanReleaseFlag, internalObject.Priority, internalObject.LastUseTime, internalObject.SpawnCount));
                    }
                }

                return results.ToArray();
            }

            internal override void Update(float elapseSeconds, float realElapseSeconds)
            {
                m_AutoReleaseTime += realElapseSeconds;
                if (m_AutoReleaseTime < m_AutoReleaseInterval)
                {
                    return;
                }

                Release();
            }

            internal override void Shutdown()
            {
                foreach (KeyValuePair<object, Object<T>> objectInMap in m_ObjectMap)
                {
                    objectInMap.Value.Release(true);
                    ReferencePool.Release(objectInMap.Value);
                }

                m_Objects.Clear();
                m_ObjectMap.Clear();
                m_CachedCanReleaseObjects.Clear();
                m_CachedToReleaseObjects.Clear();
            }

            private Object<T> GetObject(object target)
            {
                if (target == null)
                {
                    throw new GameKitException("Target is invalid.");
                }

                Object<T> internalObject = null;
                if (m_ObjectMap.TryGetValue(target, out internalObject))
                {
                    return internalObject;
                }

                return null;
            }

            private void GetCanReleaseObjects(List<T> results)
            {
                if (results == null)
                {
                    throw new GameKitException("Results is invalid.");
                }

                results.Clear();
                foreach (KeyValuePair<object, Object<T>> objectInMap in m_ObjectMap)
                {
                    Object<T> internalObject = objectInMap.Value;
                    if (internalObject.IsInUse || internalObject.Locked || !internalObject.CustomCanReleaseFlag)
                    {
                        continue;
                    }

                    results.Add(internalObject.Peek());
                }
            }

            private List<T> DefaultReleaseObjectFilterCallback(List<T> candidateObjects, int toReleaseCount, DateTime expireTime)
            {
                m_CachedToReleaseObjects.Clear();

                if (expireTime > DateTime.MinValue)
                {
                    for (int i = candidateObjects.Count - 1; i >= 0; i--)
                    {
                        if (candidateObjects[i].LastUseTime <= expireTime)
                        {
                            m_CachedToReleaseObjects.Add(candidateObjects[i]);
                            candidateObjects.RemoveAt(i);
                            continue;
                        }
                    }

                    toReleaseCount -= m_CachedToReleaseObjects.Count;
                }

                for (int i = 0; toReleaseCount > 0 && i < candidateObjects.Count; i++)
                {
                    for (int j = i + 1; j < candidateObjects.Count; j++)
                    {
                        if (candidateObjects[i].Priority > candidateObjects[j].Priority
                            || candidateObjects[i].Priority == candidateObjects[j].Priority && candidateObjects[i].LastUseTime > candidateObjects[j].LastUseTime)
                        {
                            T temp = candidateObjects[i];
                            candidateObjects[i] = candidateObjects[j];
                            candidateObjects[j] = temp;
                        }
                    }

                    m_CachedToReleaseObjects.Add(candidateObjects[i]);
                    toReleaseCount--;
                }

                return m_CachedToReleaseObjects;
            }
        }
    }
}
