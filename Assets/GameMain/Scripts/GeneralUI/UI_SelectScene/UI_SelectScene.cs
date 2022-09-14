using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using GameKit;
using GameKit.UI;
using GameKit.Event;
using System.Collections.Generic;
using Febucci.UI;
using UnityEngine.Events;
using UnityGameKit.Runtime;


public class UI_SelectScene : UIFormBase
{
    public List<UI_ScenePreview> uI_ScenePreviews;
    private int m_CurrentIndex = 0;
    private int m_LastIndex = -1;
    private bool m_IsActive = false;
    public int ScenesCount => uI_ScenePreviews.Count;
    public void UpdateScenes(List<string> avaibleScenes)
    {
        if (avaibleScenes == null)
            return;

        for (int i = 0; i < avaibleScenes.Count; i++)
        {
            for (int j = 0; j < uI_ScenePreviews.Count; j++)
            {
                if (uI_ScenePreviews[j].SceneName == avaibleScenes[i])
                {
                    uI_ScenePreviews[j].Show();
                    continue;
                }
            }
        }
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        m_IsActive = true;
        uI_ScenePreviews[0].ForceSelected();
    }

    protected override void OnResume()
    {
        base.OnResume();
        m_IsActive = true;
        uI_ScenePreviews[0].ForceSelected();
    }

    protected override void OnPause()
    {
        m_IsActive = false;
        base.OnPause();
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        if (!m_IsActive)
            return;

        base.OnUpdate(elapseSeconds, realElapseSeconds);
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            m_LastIndex = m_CurrentIndex;
            m_CurrentIndex += (m_CurrentIndex + 1) % ScenesCount;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            m_LastIndex = m_CurrentIndex;
            m_CurrentIndex = m_CurrentIndex - 1 == -1 ? ScenesCount - 1 : m_CurrentIndex - 1;
        }
        SelectScene(m_CurrentIndex);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameKitCenter.Procedure.ChangeSceneBySelect(uI_ScenePreviews[m_CurrentIndex].SceneAssetName);
            Clear();
            OnPause();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPause();
        }
    }

    public void Clear()
    {
        m_LastIndex = -1;
        m_CurrentIndex = 0;
    }

    private void SelectScene(int index)
    {
        if (m_LastIndex >= 0)
            uI_ScenePreviews[m_LastIndex].UnSelected();
        if (index >= 0)
            uI_ScenePreviews[index].Selected();
    }

    public void Show()
    {
        OnResume();
    }

    public void Hide()
    {
        OnPause();
    }
}
