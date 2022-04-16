using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Mood
{
    public string Name;
    public Sprite avatar;
    public Sprite half;
}

[CreateAssetMenu(fileName = "Character", menuName = "GameMain/Character", order = 0)]
public class CharacterSO : ScriptableObject
{
    public string Name;
    [TextArea] public string introduction;
    public List<Mood> moods;
}
