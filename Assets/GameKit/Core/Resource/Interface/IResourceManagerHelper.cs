using System.Collections.Generic;
namespace GameKit
{
    public interface IResourceManagerHelper
    {
        void LoadAsset(string name);
        void LoadAssetsByPath(string path);
        void LoadAssetsByLabels(IList<string> labels);
        void UnloadAsset(string name);
    }
}