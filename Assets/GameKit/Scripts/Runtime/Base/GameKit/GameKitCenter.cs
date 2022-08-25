using UnityEngine;

namespace UnityGameKit.Runtime
{
    public partial class GameKitCenter : MonoBehaviour
    {
        private void Start()
        {
            InitComponents();
            InitCustomComponents();
            Log.Success("GameKitCenter Initialized.");
        }
    }
}

