using UnityEngine;

public class UI_SpeakerName : UIFormChildBase
{
    public void Init(int parentDepth)
    {
        base.OnInit(parentDepth);
    }

    public void ToEason(bool status)
    {
        SetActive(status);
    }
}