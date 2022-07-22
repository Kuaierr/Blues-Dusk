using UnityEngine;
using GameKit;
public class UI_DiceRollSystem : UIGroup
{
    public UI_DiceRollPack uI_DiceRollPack;
    public Camera ui_Camera;
    public RectTransform rollPanel;
    public DiceForRoll diceForRoll_Prototype;
    private bool isActive = false;

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    protected override void OnStart()
    {
        isActive = true;
    }
    
    /// <summary>
    /// Update is called every frame, if the MonoBehaviour is enabled.
    /// </summary>
    private void Update()
    {
        if(!isActive)
            return;
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 dicePos = uI_DiceRollPack.selector.transform.position;
            DiceForRoll newDiceRoll =  Instantiate(diceForRoll_Prototype, dicePos, Quaternion.identity, rollPanel);
            newDiceRoll.transform.position = new Vector3(newDiceRoll.transform.position.x, newDiceRoll.transform.position.y, diceForRoll_Prototype.transform.position.z);
            newDiceRoll.OnInit(new Dice());
            newDiceRoll.gameObject.SetActive(true);
        }
    }
}