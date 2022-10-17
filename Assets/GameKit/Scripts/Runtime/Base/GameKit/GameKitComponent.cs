using UnityEngine;
namespace UnityGameKit.Runtime
{
    public abstract class GameKitComponent : MonoBehaviour
    {
        protected virtual void Awake()
        {
            GameKitComponentCenter.RegisterComponent(this);
        }
    }
}
