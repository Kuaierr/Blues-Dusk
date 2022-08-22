using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using GameKit.DataStructure;
#if PACKAGE_ADDRESSABLES
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
#endif
namespace GameKit
{
    internal partial class ResourceManager : GameKitModule, IResourceManager
    {
        public sealed class Asset : IReference
        {
            private string m_Name;
            private object m_Target;
            private bool m_Locked;
            private int m_Priority;
            private int m_ReferenceCount;

            public Asset()
            {
                m_Name = null;
                m_Target = null;
                m_Locked = false;
                m_Priority = 0;
            }

            public string Name
            {
                get
                {
                    return m_Name;
                }
                set
                {
                    m_Name = value;
                }
            }

            public object Target
            {
                get
                {
                    m_ReferenceCount++;
                    return m_Target;
                }
                set
                {
                    m_Target = value;
                }
            }

            public bool Locked
            {
                get
                {
                    return m_Locked;
                }
                set
                {
                    m_Locked = value;
                }
            }

            public int Priority
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

            public bool Available
            {
                get
                {
                    return ReferenceCount <= 0;
                }
            }

            public int ReferenceCount
            {
                get
                {
                    return m_ReferenceCount;
                }
                // private set;
            }

            public static Asset Create(string name, object target, bool locked = false, int priority = 0)
            {
                Asset asset = ReferencePool.Acquire<Asset>();
                asset.Name = name ?? string.Empty;
                asset.Target = target;
                asset.Locked = locked;
                asset.Priority = priority;
                return asset;
            }

            public void Clear()
            {
                m_Name = null;
                m_Target = null;
                m_Locked = false;
                m_Priority = 0;
            }

            public void Refresh()
            {
                if (Target == null)
                {
                    Clear();
                    Release();
                }
            }

            public void Release()
            {
                ReferencePool.Release(this);
            }
        }
    }
}
