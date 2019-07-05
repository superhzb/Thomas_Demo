using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainPanel : BasePanel
{
    public void OnHomeBtnClicked()
    {
        GameManager.Instance.mAudioManager.PlayButtonAudioClip();
        OnExit();
    }

    public void OnConfirmBtnClicked()
    {
        GameManager.Instance.mAudioManager.PlayButtonAudioClip();
        mUIManager.PushPanel(UIPanelType.WorldMapPanel);
    }

    public void PlayPagingSoundFX()
    {
        GameManager.Instance.mAudioManager.PlayPageAudioClip();
    }
}
