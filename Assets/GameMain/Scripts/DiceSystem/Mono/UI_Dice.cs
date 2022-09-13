using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using GameKit;
using GameKit.QuickCode;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class UI_Dice : UIData/*, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler*/
{
    private UnityAction<UI_Dice> onClick;
    
    private UI_DiceData_SO diceData;

    [SerializeField]
    private Image _selectedMark;
    
    [SerializeField]
    private RectTransform diceCube;

    [SerializeField]
    private Material diceUIMaterial;

    [SerializeField]
    private Material diceMaskMaterial;

    [SerializeField]
    private Rigidbody _rb;
    
    [Space]
    public List<UI_DiceFace> faces;

    [Space(15)]
    public DiceFaceEvent_SO onDiceMouseEnter;

    public NoParameterEvent_SO onDiceMouseExit;

    public Dice_SuitType Result { get; private set; }

    public bool Stopped => _rb.velocity == Vector3.zero;
    private Vector3 _finalRotation;

    /// <summary>
    /// 用于确定在背包中的位置，是在所有骰子中的序号
    /// </summary>
    public int Index { get; private set; } = -1;

    private Sequence resetSequence;
    public bool IsComplete => !resetSequence.IsPlaying();
    
    public UI_Dice OnInit(UI_DiceData_SO data, int index, UnityAction<UI_Dice> onClickCallback)
    {
        if (data == null)
            Debug.LogError("DiceData is null");
        diceData = data;

        for (int i = 0; i < data.faceDatas.Count; i++)
            faces[i].OnInit(data.faceDatas[i]);

        Index = index;
        onClick += onClickCallback;
        
        OnDisSelected();

        return this;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        StartCoroutine("Rotate");
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        StopCoroutine("Rotate");
    }

    private IEnumerator Rotate()
    {
        while (true)
        {
            diceCube.Rotate(Vector3.up, 20f * Time.deltaTime);
            yield return 0;
        }
    }

    //获取这个骰子的结果
    public void ChangeToDiceMaskMaterial() => ChangeDiceMaterial(diceMaskMaterial);

    public void ChangeToDiceUIMaterial() => ChangeDiceMaterial(diceUIMaterial);

    public UI_DiceFaceBase_SO GetResult()
    {
        UI_DiceFace resFace = faces[0];
        foreach (UI_DiceFace face in faces)
        {
            if (face.transform.position.z < resFace.transform.position.z)
                resFace = face;
        }
        
        _finalRotation = resFace.finalRotation;
        
        Result = resFace.GetValue().Type;
        return resFace.GetValue();
    }

    public void Roll()
    {
        _rb.isKinematic = false;
        StopCoroutine("Rotate");

        //TODO FixRange
        Vector3 dir = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), 0);
        int force = Random.Range(25, 40);
        _rb.velocity = dir * force;
    }

    public void ResetTransform(RectTransform target)
    {
        _rb.isKinematic = true;
        
        var dice = _rb.transform;
        // 防止直接修改父物体导致的错位
        dice.SetParent(transform.parent);
        transform.SetParent(target);
        //保证骰子实体会被销毁
        //dice.SetParent(transform);
        
        LayoutRebuilder.ForceRebuildLayoutImmediate(target);
        resetSequence = DOTween.Sequence();
        resetSequence.Append(dice.DOMove(transform.position, 0.5f));
        resetSequence.Insert(0, dice.DORotate(_finalRotation, 0.5f));
        //this.enabled = false;
    }

    private void ChangeDiceMaterial(Material mat)
    {
        foreach (UI_DiceFace face in faces)
            face.CurrentMaterial = mat;

        //替换一下骰子本体的shader
        var diceMat = _rb.GetComponent<MeshRenderer>();
        Texture diceTex = diceMat.material.mainTexture;
        diceMat.material = mat;
        diceMat.material.mainTexture = diceTex;
    }

    #region DiceInfoDisplay

    public void OnSelected()
    {
        onDiceMouseEnter.Raise(diceData);
        _selectedMark.color = Color.white;
    }

    //Problem 选中一个之后，默认的选择指向下一个？
    public void OnConfirmed()
    {
        onClick.Invoke(this);
        onDiceMouseExit.Raise();
    }

    public void OnDisSelected()
    {
        onDiceMouseExit.Raise();
        _selectedMark.color=Color.clear;
    }

    #endregion

    //需要一个Image作为鼠标判定的范围
    /*public void OnPointerEnter(PointerEventData eventData)
    {
        OnSelected();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnConfirmed();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnDisSelected();
    }*/
}