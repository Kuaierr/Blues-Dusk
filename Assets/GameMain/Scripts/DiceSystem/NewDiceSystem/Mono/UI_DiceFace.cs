using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_DiceFace : MonoBehaviour
{
    public string Value { get; private set; } = "";

    public Vector3 finalRotation = new Vector3();

    private MeshRenderer _meshRenderer;
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
    }

    public void OnInit(UI_DiceFace_SO faceData)
    {
        Value = faceData.name;

        _meshRenderer = GetComponent<MeshRenderer>();
        _texture = CurrentTexture = faceData.icon.texture;
    }

    public string GetValue()
    {
        return Value;
    }

    public Vector3 GetFinalRotation()
    {
        return finalRotation;
    }
}