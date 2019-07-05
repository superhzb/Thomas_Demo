using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingPanel : BasePanel
{
    public void OnBackBtnClicked()
    {
        GameManager.Instance.mAudioManager.PlayButtonAudioClip();
        OnExit();
    }
}
