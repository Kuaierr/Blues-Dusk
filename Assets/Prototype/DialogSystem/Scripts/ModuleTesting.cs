using UnityEngine;

public class ModuleTesting : MonoBehaviour
{
    private CanvasGroup canvasGroup;
    private bool isShown = false;
    private void Start()
    {
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
    }
}