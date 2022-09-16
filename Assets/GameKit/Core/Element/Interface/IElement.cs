using GameKit.Setting;
namespace GameKit.Element
{
    public interface IElement
    {
        string Name { get; }
        void OnInit();
        void Show();
        void Hide();
        void OnHighlightEnter();
        void OnHighlightExit();
        void OnInteract();
    }
}
