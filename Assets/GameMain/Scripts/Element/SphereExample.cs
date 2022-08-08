using UnityEngine;
using GameKit;
using UnityEngine.Events;



public class SphereExample : InteractiveElement 
{    
    protected override void OnStart()
    {
        
    }

    public override void OnInteract()
    {
        base.OnInteract();
        if(CanCollect)
        {
            BackpackSystem.current.AddToBackpack(Data);
            Destroy(this.gameObject);
        }
    }
}