using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : BasePanel
{
    public void OnResumeBtnClicked()
    {
        GameController.Instance.ResumeGame();
        OnExit();
        mUIManager.PopPanel();
    }

    public void OnRestartBtnClicked()
    {
        GameManager.Instance.EnterGameDemoScene();
    }

    public void OnHomeBtnClicked()
    {
        GameManager.Instance.EnterGameOptionScene();
    }
}
