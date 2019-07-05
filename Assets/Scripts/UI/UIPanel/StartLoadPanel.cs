using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartLoadPanel : BasePanel
{
    private Image img_popup;

    private void Awake()
    {
        img_popup = transform.Find("Img_Popup").GetComponent<Image>();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        GameManager.Instance.mAudioManager.PlayPageAudioClip();
        mUIManager.ShowMask(true);
    }

    public void OnEnterClicked()
    {
        GameManager.Instance.mAudioManager.PlayButtonAudioClip();
        img_popup.GetComponent<Animator>().Play("PopupExit");
        Invoke("EnterGameOptionScene", 2f);

    }

    private void EnterGameOptionScene()
    {
        GameManager.Instance.EnterGameOptionScene();
    }

}
