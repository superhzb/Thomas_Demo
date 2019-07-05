using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOptionPanel : BasePanel
{
    private GameObject emp_NotReadyHint;
    private GameObject notReadyHint_RightUp;
    private GameObject notReadyHint_RightDown;
    private Transform btn_Shop;
    private Transform btn_Train;

    private void Awake()
    {
        emp_NotReadyHint = transform.Find("Emp_NotReady").gameObject;
        notReadyHint_RightUp = transform.Find("Emp_NotReady/Emp_RightUp").gameObject;
        notReadyHint_RightDown = transform.Find("Emp_NotReady/Emp_RightDown").gameObject;
        btn_Shop = transform.Find("Btn_Shop");
        btn_Train = transform.Find("Btn_Train");
    }

    public override void OnEnter()
    {
        base.OnEnter();
        GameManager.Instance.mAudioManager.PlayBGMusic("BGMusic");
    }

    public void OnShopBtnClicked()
    {
        GameManager.Instance.mAudioManager.PlayButtonAudioClip();
        emp_NotReadyHint.transform.position = btn_Shop.position + new Vector3(-100, 0, 0);
        notReadyHint_RightUp.SetActive(false);
        notReadyHint_RightDown.SetActive(true);
        emp_NotReadyHint.GetComponent<Animator>().Play(0);
    }

    public void OnTrainBtnClicked()
    {
        GameManager.Instance.mAudioManager.PlayButtonAudioClip();
        emp_NotReadyHint.transform.position = btn_Train.position + new Vector3(-100, 0, 0);
        notReadyHint_RightUp.SetActive(true);
        notReadyHint_RightDown.SetActive(false);
        emp_NotReadyHint.GetComponent<Animator>().Play(0);
    }

    public void OnSoloBtnClicked()
    {
        GameManager.Instance.mAudioManager.PlayButtonAudioClip();
        mUIManager.PushPanel(UIPanelType.TrainPanel);
    }

    public void OnSettingBtnClicked()
    {
        GameManager.Instance.mAudioManager.PlayButtonAudioClip();
        mUIManager.PushPanel(UIPanelType.VerificationPanel);
    }

    public void PlayButtonSoundFX() 
    {
        GameManager.Instance.mAudioManager.PlayButtonAudioClip();
    }
}
