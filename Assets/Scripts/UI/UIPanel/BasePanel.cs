using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasePanel : MonoBehaviour
{
    public UIManager mUIManager;
    protected CanvasGroup canvasGroup;
   
    public virtual void OnEnter() {
        this.gameObject.SetActive(true);
        canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;
    }

    public virtual void OnPause()
    {
        this.gameObject.SetActive(false);
        canvasGroup.blocksRaycasts = false;
    }

    public virtual void OnResume()
    {
        this.gameObject.SetActive(true);
        canvasGroup.blocksRaycasts = true;
    }

    public virtual void OnExit() {
        canvasGroup.alpha = 0;
        canvasGroup.blocksRaycasts = false;
        this.gameObject.SetActive(false);
        mUIManager.PopPanel();
    }

}
