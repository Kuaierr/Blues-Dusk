using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameKit.Runtime;

[CreateAssetMenu(fileName = "New Dice Pool", menuName = "GameMain/DiceData/DicePool")]
public class UI_DiceDataPool_SO : PoolBase_SO
{
    public List<UI_DiceData_SO> diceData = new List<UI_DiceData_SO>();

    public override UI_DiceData_SO GetData<UI_DiceData_SO>(int id)
    {
        Log.Fail("GetData by id for {0} is no implement yet.", this.name);
        return null;
    }

    public override UI_DiceData_SO GetData<UI_DiceData_SO>(string name)
    {
        // Log.Fail("GetData by name for {0} is no implement yet.", this.name);
        // return null;
        for (int i = 0; i < diceData.Count; i++)
        {
            if (diceData[i].DiceName == name)
            {
                return diceData[i] as UI_DiceData_SO;
            }
        }
        Log.Fail("Can not find data {0} for {1}.", name, this.name);
        return null;
    }
}
