using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterPool", menuName = "GameMain/CharacterPool", order = 0)]
public class CharacterPool : ScriptableObject
{
    public List<CharacterSO> pool;
}
