using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldMapPanel : BasePanel
{
    private Animator anim_emp_SpecialLevelHint;

    private void Start()
    {
        ShowHint();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        ShowHint();
    }

    private void ShowHint()
    {
        anim_emp_SpecialLevelHint = transform.Find("Emp_SpecialLevel").GetComponent<Animator>();
        anim_emp_SpecialLevelHint.Play(0);
    }

    public void OnBackBtnClicked()
    {
        GameManager.Instance.mAudioManager.PlayButtonAudioClip();
        OnExit();
    }

    public void OnMapBtnClicked()
    {
        GameManager.Instance.mAudioManager.PlayButtonAudioClip();
        GameManager.Instance.EnterGameDemoScene();
    }
}
