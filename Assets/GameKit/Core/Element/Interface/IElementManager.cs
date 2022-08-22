namespace GameKit
{
    public interface IElementManager
    {
        void RemoveElement(IElement element);
        void RegisterElement(IElement element);
        void HighlightAll();
        void StopHighlightAll();
    }
}