using UnityEngine;
using UnityGameKit.Runtime;
using GameKit.Element;
using LubanConfig.DataTable;
[DisallowMultipleComponent]
[AddComponentMenu("BluesDusk/Collect Object")]
public class CollectElement : SceneElementBase
{
    [SerializeField] protected int m_DataId = 0;
    [Dialog] public string Dialog;
    public bool CanCollect = true;
    private Item m_configData;
    public Item Data
    {
        get
        {
            return m_configData;
        }
    }

    public int DataId
    {
        get
        {
            return m_DataId;
        }
    }

    public override void OnInit()
    {
        base.OnInit();
        GameKitCenter.Event.Subscribe(FinishDialogCompleteEventArgs.EventId, OnDialogFinish);
        m_configData = GameKitCenter.Data.ItemTable.Get(m_DataId);
        if (m_configData == null)
        {
            Log.Fail("Incorrect Data Id for {0}", gameObject.name);
            CanCollect = false;
            return;
        }
        gameObject.name = m_configData.Name;
    }

    public override void OnInteract()
    {
        base.OnInteract();
        if (Dialog != "<None>" && Dialog != string.Empty)
        {
            Vector3 middlePos = (Player.Current.transform.position + this.transform.position) / 2;
            QuickCinemachineCamera.current.SetFocus(middlePos);
            DialogSystem.current.StartDialog(Dialog);
        }
        else
        {
            OnCollect();
        }
    }

    public void OnCollect()
    {
        if (CanCollect)
        {
            PlayerBackpack.current.CollectToBackpack(m_configData);
            Player.Current.CollectItem(()=>{
                gameObject.SetActive(false);
            });
        }
    }

    private void OnDialogFinish(object sender, GameKit.Event.GameEventArgs e)
    {
        FinishDialogCompleteEventArgs eventArgs = (FinishDialogCompleteEventArgs)e;
        if (eventArgs.UserData == null)
            return;
        if (eventArgs.UserData.GetType() == typeof(DialogSystem))
        {
            if (eventArgs.AssetName == Dialog)
            {
                OnInteractAfter?.Invoke();
                OnCollect();
            }
        }
    }
}