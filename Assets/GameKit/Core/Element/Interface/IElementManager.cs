
namespace GameKit.Element
{
    public interface IElementManager
    {
        void RemoveElement(IElement element);
        void RegisterElement(IElement element);
        void HighlightAll();
        void StopHighlightAll();
        void SaveAll();
        void LoadAll();
    }
}