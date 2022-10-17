using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_DiceFace : MonoBehaviour
{
    public Vector3 finalRotation = new Vector3();
    
    private UI_DiceFaceBase_SO _data;

    /*private MeshRenderer _meshRenderer;
    private Texture _texture;
    
    public Material CurrentMaterial
    {
        set
        {
            _meshRenderer.material = value;
            CurrentTexture = _texture;
        }
    }

    public Texture CurrentTexture
    {
        set => _meshRenderer.material.mainTexture = value;
    }*/

    public void OnInit(UI_DiceFaceBase_SO faceBaseData)
    {
        if (faceBaseData == null)
        {
            Debug.LogError("骰子花色未配置");
            return;
        }
        
        _data = faceBaseData;

        GetComponent<MeshRenderer>().material.mainTexture = faceBaseData.icon.texture;
        /*_meshRenderer = GetComponent<MeshRenderer>();
        _texture = CurrentTexture = faceBaseData.icon.texture;*/
    }

    public UI_DiceFaceBase_SO GetValue()
    {
        return _data;
    }
}