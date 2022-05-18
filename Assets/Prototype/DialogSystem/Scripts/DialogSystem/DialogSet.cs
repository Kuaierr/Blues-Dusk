using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogSet", menuName = "GameMain/DialogSet", order = 0)]
public class DialogSet : ScriptableObject
{
    private string id;
    private List<string> characters;
    public string title;
    // [SerializeField] public List<DialogNode> dialogsPreview;  
    // public Tree dialogTree;


}
