using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EntitiesPool", menuName = "GameMain/EntitiesPool", order = 0)]
public class EntitiesPool : ScriptableObject
{
    public List<Character> characters;

    public Character FindCharacter(string name)
    {
        foreach (var character in characters)
        {
            if(character.displayName == name)
            {
                return character;
            }
        }
        return null;
    }
}
