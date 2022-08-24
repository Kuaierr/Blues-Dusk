using UnityEngine;

namespace UnityGameKit.Runtime
{
    public partial class GameKitCenter : MonoBehaviour
    {
        private void Start()
        {
            Log.Success("GameKitCenter Initialize.");
            InitComponents();
            InitCustomComponents();
        }
    }
}

