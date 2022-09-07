
namespace GameKit.Element
{
    public interface IElementManager
    {
        int ElementCount { get; }
        void RemoveElement(IElement element);
        void RegisterElement(IElement element);
        IElement GetElement(string name);
        void HighlightAll();
        void StopHighlightAll();
        void SaveAll();
        void LoadAll();
    }
}