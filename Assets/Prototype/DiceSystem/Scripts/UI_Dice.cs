using UnityEngine;
using GameKit;

public class UI_Dice : UIForm
{
    public RectTransform diceCube;
    
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        diceCube.Rotate(Vector3.up, 20f * Time.deltaTime);
    }
}