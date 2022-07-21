using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using GameKit;

public enum DiceType
{
    OneShot,
    Permenent,
    Limited
}

[System.Serializable]
public class DiceFacet
{
    public bool IsFunctional;
    public string name;
    public int value;
    public UnityAction action;

    public DiceFacet(string name, int number = 1)
    {
        this.name = name;
        this.value = number;
        this.IsFunctional = false;
        this.action = null;
    }
}
[System.Serializable]
public class Dice
{
    public DiceType diceType;
    public readonly List<DiceFacet> facets;

    public Dice()
    {
        facets = new List<DiceFacet>();
        facets.Add(new DiceFacet("Up", 1));
        facets.Add(new DiceFacet("Down", 2));
        facets.Add(new DiceFacet("Back", 3));
        facets.Add(new DiceFacet("Front", 4));
        facets.Add(new DiceFacet("Left", 5));
        facets.Add(new DiceFacet("Right", 6));
    }

    public DiceFacet GetFacet(string name)
    {
        Debug.Log(name);
        for (int i = 0; i < facets.Count; i++)
        {
            if (facets[i].name.Correction() == name.Correction())
                return facets[i];
        }
        return null;
    }

}