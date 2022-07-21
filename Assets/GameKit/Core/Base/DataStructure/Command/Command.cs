using UnityEngine.Events;
using GameKit;
namespace GameKit.DataStructure
{
    public class Command : ICommand, IReference
    {
        public string id;
        public UnityEvent Do;
        public UnityEvent Undo;
        public Command()
        {
            this.id = "Default ID";
        }
        public void Clear()
        {
            
        }
    }
}