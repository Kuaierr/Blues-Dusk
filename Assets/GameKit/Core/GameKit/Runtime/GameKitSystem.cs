using UnityEngine;
namespace GameKit
{
    public abstract class GameKitSystem : MonoBehaviour
    {
        protected virtual void Awake()
        {
            GameKitSystemCenter.RegisterSystem(this);
        }
    }
}
