using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using GameKit;

public class ModuleTesting : MonoBehaviour
{
    public DebugButton debugButtonPrototype;
    private DialogSystem dialogSystem;
    public List<DebugButton> moduleButtons;
    private List<DialogAsset> dialogAssets;
    private CanvasGroup canvasGroup;
    private bool isShown = false;
    private void Start()
    {
        dialogSystem = GameKitComponentCenter.GetComponent<DialogSystem>();
        canvasGroup = GetComponent<CanvasGroup>();
        ChangeDisplay(isShown);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            isShown = !isShown;
            ChangeDisplay(isShown);
        }
    }

    private void ChangeDisplay(bool state)
    {
        canvasGroup.alpha = state ? 1 : 0;
        canvasGroup.interactable = state;
        canvasGroup.blocksRaycasts = state;
        isShown = state;
        if (state)
        {
            ClearModuleUnits();
            ShowAllModules();
        }
        else
            HideAllModules();
    }

    public void DialogTest()
    {

        if (dialogAssets == null || dialogAssets.Count == 0)
        {

            AddressableManager.instance.GetAssetsAsyn<DialogAsset>(new List<string> { "DialogPack" }, callback: (IList<DialogAsset> assets) =>
            {
                dialogAssets = new List<DialogAsset>(assets);
                Debug.Log($"CreateModuleUnits");
                HideAllModules();
                for (int i = 0; i < dialogAssets.Count; i++)
                {
                    int index = i;
                    CreateModuleUnits(dialogAssets[index].title, () =>
                    {
                        dialogSystem.StartDialog(dialogAssets[index].title, dialogAssets[index].contents);
                        ChangeDisplay(false);
                    });
                }
            });
        }
        else
        {
            HideAllModules();
            for (int i = 0; i < dialogAssets.Count; i++)
            {
                int index = i;
                CreateModuleUnits(dialogAssets[index].title, () =>
                {
                    dialogSystem.StartDialog(dialogAssets[index].title, dialogAssets[index].contents);
                    ChangeDisplay(false);
                });
            }
        }
    }
    private void HideAllModules()
    {
        for (int i = 0; i < moduleButtons.Count; i++)
        {
            moduleButtons[i].gameObject.SetActive(false);
        }
    }

    private void ShowAllModules()
    {
        for (int i = 0; i < moduleButtons.Count; i++)
        {
            moduleButtons[i].gameObject.SetActive(true);
        }
    }

    private void ClearModuleUnits()
    {
        Button[] buttons = GetComponentsInChildren<Button>(false);
        for (int i = 0; i < buttons.Length; i++)
        {
            Destroy(buttons[i].gameObject);
        }
    }

    private void CreateModuleUnits(string name, UnityAction action)
    {
        Debug.Log($"Create Modules Units");
        DebugButton dButton = GameObject.Instantiate<DebugButton>(debugButtonPrototype, Vector3.zero, Quaternion.identity, this.transform);
        dButton.text.text = name;
        dButton.AddListener(action);
    }
}