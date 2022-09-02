using UnityEngine;

[CreateAssetMenu(fileName = "Moon",menuName = "GameMain/DiceData/DiceFace/NothingWillHappen")]
public class DiceFace_NotingWillHappen_SO : UI_DiceFaceBase_SO
{
    public override int Priority => 0;
    public override Dice_SuitType Type => Dice_SuitType.SPECIAL;

    public override void Effect(Dice_Result result)
    {
        result.BreakOut();
    }
}