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
    [SerializeField] private List<UI_ScenePreview> m_ScenePreviews;
    private int m_CurrentIndex = 0;
    private int m_LastIndex = -1;
    private bool m_IsActive = false;
    [SerializeField] private List<UI_ScenePreview> m_ActiveScenePreviews;

    public void UpdateScenes(List<string> avaibleScenes)
    {
        if (avaibleScenes == null)
            return;
        
        for (int i = 0; i < avaibleScenes.Count; i++)
        {
            for (int j = 0; j < m_ScenePreviews.Count; j++)
            {
                // Log.Info(m_ScenePreviews[j].SceneAssetName.Correction() + " >> " + avaibleScenes[i].Correction());
                if (m_ScenePreviews[j].SceneAssetName.Correction() == avaibleScenes[i].Correction())
                {
                    m_ScenePreviews[j].Show();
                    m_ActiveScenePreviews.Add(m_ScenePreviews[j]);
                    continue;
                }
            }
        }

        if (m_ActiveScenePreviews.Count > 0)
            m_ActiveScenePreviews[0].Selected();
    }

    protected override void OnInit(object userData)
    {
        base.OnInit(userData);
        if (m_ActiveScenePreviews == null)
            m_ActiveScenePreviews = new List<UI_ScenePreview>();
    }

    protected override void OnOpen(object userData)
    {
        base.OnOpen(userData);
        m_IsActive = true;
        CursorSystem.current.Disable();
    }

    protected override void OnResume()
    {
        base.OnResume();
        Clear();
        m_IsActive = true;
        if (m_ActiveScenePreviews.Count > 0)
            m_ActiveScenePreviews[0].Selected();
        CursorSystem.current.Disable();
    }

    protected override void OnPause()
    {
        m_IsActive = false;
        Clear();
        base.OnPause();
        CursorSystem.current.Enable();
    }

    protected override void OnUpdate(float elapseSeconds, float realElapseSeconds)
    {
        if (!m_IsActive)
            return;
        
        base.OnUpdate(elapseSeconds, realElapseSeconds);
        
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            m_LastIndex = m_CurrentIndex;
            m_CurrentIndex = m_CurrentIndex - 1 <= -1 ? (m_ActiveScenePreviews.Count - 1) : (m_CurrentIndex - 1);
            SelectScene(m_CurrentIndex);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            m_LastIndex = m_CurrentIndex;
            m_CurrentIndex = (m_CurrentIndex + 1) % (m_ActiveScenePreviews.Count);
            SelectScene(m_CurrentIndex);
        }


        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameKitCenter.Procedure.ChangeSceneBySelect(m_ActiveScenePreviews[m_CurrentIndex].SceneAssetName);
            OnPause();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnPause();
        }
    }

    public void Clear()
    {
        m_LastIndex = 0;
        m_CurrentIndex = 0;
        m_ActiveScenePreviews.Clear();
    }

    private void SelectScene(int index)
    {
        Debug.Log("Debug: " + index);
        if (m_LastIndex >= 0)
            m_ActiveScenePreviews[m_LastIndex].UnSelected();
        if (index >= 0)
            m_ActiveScenePreviews[index].Selected();
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
