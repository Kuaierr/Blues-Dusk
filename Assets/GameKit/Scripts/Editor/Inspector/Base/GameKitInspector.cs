using UnityEditor;

namespace UnityGameKit.Editor
{
    public abstract class GameKitInspector : UnityEditor.Editor
    {
        private bool m_IsCompiling = false;

        public override void OnInspectorGUI()
        {
            if (m_IsCompiling && !EditorApplication.isCompiling)
            {
                m_IsCompiling = false;
                OnCompileComplete();
            }
            else if (!m_IsCompiling && EditorApplication.isCompiling)
            {
                m_IsCompiling = true;
                OnCompileStart();
            }
        }

        protected virtual void OnCompileStart()
        {

        }

        protected virtual void OnCompileComplete()
        {
            
        }

        protected bool IsPrefabInHierarchy(UnityEngine.Object obj)
        {
            if (obj == null)
            {
                return false;
            }
            return PrefabUtility.GetPrefabAssetType(obj) != PrefabAssetType.Regular;
        }
    }
}
