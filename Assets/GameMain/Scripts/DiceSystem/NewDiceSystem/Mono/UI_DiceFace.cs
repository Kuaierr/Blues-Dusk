using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_DiceFace : MonoBehaviour
{
    private UI_DiceFaceBase_SO _data;
    
    private MeshRenderer _meshRenderer;
    private Texture _texture;
    
    public Vector3 finalRotation = new Vector3();

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
    }

    public void OnInit(UI_DiceFaceBase_SO faceBaseData)
    {
        _data = faceBaseData;

        _meshRenderer = GetComponent<MeshRenderer>();
        _texture = CurrentTexture = faceBaseData.icon.texture;
    }

    public UI_DiceFaceBase_SO GetValue()
    {
        return _data;
    }

    public Vector3 GetFinalRotation()
    {
        return finalRotation;
    }
}