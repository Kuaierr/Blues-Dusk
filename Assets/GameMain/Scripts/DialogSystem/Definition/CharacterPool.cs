using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameKit.Runtime;

[CreateAssetMenu(fileName = "CharacterPool", menuName = "GameMain/CharacterPool", order = 0)]
public class CharacterPool : PoolBase_SO
{
    public List<Character> characters;
    public override Character GetData<Character>(string name)
    {
        foreach (var character in characters)
        {
            if (character.displayName == name)
            {
                return character as Character;
            }
        }

        Log.Fail("Can not find data {0} for {1}.", name, this.name);
        return null;
    }

    public override Character GetData<Character>(int id)
    {
        Log.Fail("GetData by id for {0} is no implement yet.", this.name);
        return null;
    }
}
