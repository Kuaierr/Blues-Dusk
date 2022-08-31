using UnityEngine;
using GameKit;

namespace UnityGameKit.Runtime
{
    internal sealed class InventoryUIInitInfo : IReference
    {
        private KeyCode m_KeyCode;
        private object m_UserData;

        public InventoryUIInitInfo()
        {
            m_KeyCode = KeyCode.None;
            m_UserData = null;
        }

        public KeyCode ChangeDisplayKeyCode
        {
            get
            {
                return m_KeyCode;
            }
        }

        public object UserData
        {
            get
            {
                return m_UserData;
            }
        }

        public static InventoryUIInitInfo Create(KeyCode keyCode, object userData)
        {
            InventoryUIInitInfo initUIInfo = ReferencePool.Acquire<InventoryUIInitInfo>();
            initUIInfo.m_KeyCode = keyCode;
            initUIInfo.m_UserData = userData;
            return initUIInfo;
        }

        public void Clear()
        {
            m_KeyCode = KeyCode.None;
            m_UserData = null;
        }
    }
}
