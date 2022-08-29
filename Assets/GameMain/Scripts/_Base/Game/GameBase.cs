using GameKit.Event;
using UnityEngine;
using UnityGameKit.Runtime;


public abstract class GameBase
{
    public abstract GameMode GameMode
    {
        get;
    }

    public bool GameOver
    {
        get;
        protected set;
    }

    public virtual void Initialize()
    {
        GameOver = false;
    }

    public virtual void Shutdown()
    {

    }

    public virtual void Update(float elapseSeconds, float realElapseSeconds)
    {
        
    }

}

