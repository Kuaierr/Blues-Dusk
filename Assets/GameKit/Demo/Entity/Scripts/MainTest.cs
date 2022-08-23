using UnityEngine;
using GameKit;
using UnityGameKit.Runtime;
using System.Collections;
namespace UnityGameKit.Demo
{
    public class MainTest : MonoBehaviour
    {
        private const string DEMO_ENTITY_PATH = "Assets/GameKit/Entity/Demo/Reousrce/Prefab/{0}.prefab";
        private EntityComponent entityComponent;
        private void Start()
        {
            StartCoroutine(ExecuteNextFrame());
        }

        IEnumerator ExecuteNextFrame()
        {
            yield return null;
            entityComponent = GameKitComponentCenter.GetComponent<EntityComponent>();
            EntityTestData data = new EntityTestData(2022, 0727);
            entityComponent.ShowEntity(data.Id, typeof(EntityTest), GetEntityAssetDemo("Cube"), "Geometry", 30, data);
        }
        public static string GetEntityAssetDemo(string assetName) => Utility.Text.Format(DEMO_ENTITY_PATH, assetName);
    }
}