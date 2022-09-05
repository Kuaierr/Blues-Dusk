using GameKit;
using UnityEngine;

public class PlotGame : GameBase
{
    private float m_ElapseSeconds = 0f;

    public override GameMode GameMode
    {
        get
        {
            return GameMode.PlotMode;
        }
    }

    public override void Update(float elapseSeconds, float realElapseSeconds)
    {
        base.Update(elapseSeconds, realElapseSeconds);
        m_ElapseSeconds += elapseSeconds;
    }
}

