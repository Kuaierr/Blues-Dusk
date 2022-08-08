namespace GameKit
{
    public interface IElement
    {
        int DataId { get; }
        void OnDestory();
        void OnRefresh();
        void Show();
        void Hide();
        void OnHighlightEnter();
        void OnHighlightExit();
        void OnInteract();
        void SetManager(IElementManager manager);
    }
}   
