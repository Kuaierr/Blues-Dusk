using GameKit.Setting;
namespace GameKit.Element
{
    public interface IElement
    {
        string Name { get; }
        int DataId { get; }
        void OnInit();
        void Show();
        void Hide();
        void OnHighlightEnter();
        void OnHighlightExit();
        void OnInteract();
    }
}
