using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HUDPanel : BasePanel
{
    private Slider slider;
    private TextMeshProUGUI timer;
    private Button speedupBtn;
    private Button superSpdupBtn;
    private Animator anim_StartCountDown;
    private GameObject tutorial_pickup;
    private GameObject tutorial_dropoff;
    private TextMeshProUGUI ballText;

    public override void OnEnter()
    {
        base.OnEnter();
    
        anim_StartCountDown.gameObject.SetActive(true);
        anim_StartCountDown.Play(0);
        tutorial_pickup.SetActive(false);
        tutorial_dropoff.SetActive(false);
        SetActiveSpeedupBtn(true);
        SetActiveSuperSpeedupBtn(false);

        InvokeRepeating("PlayCountDownAudio", 0f, 1f);
        Invoke("StartGame", 3f);
    }

    private void PlayCountDownAudio()
    {
        GameManager.Instance.mAudioManager.PlaySoundFX("CountDown");
    }

    private void Awake()
    {
        slider = transform.Find("Slider_Progress").GetComponent<Slider>();
        timer = transform.Find("Slider_Progress/Txt_Timer").GetComponent<TextMeshProUGUI>();
        speedupBtn = transform.Find("Btn_Speedup").GetComponent<Button>();
        superSpdupBtn = transform.Find("Btn_Super").GetComponent<Button>();
        anim_StartCountDown = transform.Find("Img_StartCountDown").GetComponent<Animator>();
        tutorial_pickup = transform.Find("Tutorial_pickup").gameObject;
        tutorial_dropoff = transform.Find("Tutorial_dropoff").gameObject;
        ballText = transform.Find("Img_Ball/Txt_BallDelivered").GetComponent<TextMeshProUGUI>();
    }

    private void StartGame()
    {
        GameManager.Instance.mAudioManager.PlaySoundFX("GO");
        CancelInvoke();
        GameController.Instance.ResumeGame();
    }

    public void OnSpeedupBtnClicked()
    {
        GameManager.Instance.mAudioManager.PlayButtonAudioClip();
        GameController.Instance.OnSpeedupBtnClicked();
    }

    public void OnSuperSpeedupBtnClicked()
    {
        GameManager.Instance.mAudioManager.PlaySoundFX("SuperSpeedup");
        GameController.Instance.OnSuperBtnClicked();
    }

    public void OnPauseBtnClicked()
    {
        GameManager.Instance.mAudioManager.PlayButtonAudioClip();
        mUIManager.PushPanel(UIPanelType.PausePanel);
        GameController.Instance.isPause = true;
    }

    public void UpdateSliderValue(float remainTime, float value)
    {
        timer.text = ((int)remainTime).ToString();
        slider.value = value;
    }

    public void SetActiveSuperSpeedupBtn(bool isOn)
    {
        superSpdupBtn.gameObject.SetActive(isOn);
    }

    public void SetActiveSpeedupBtn(bool isOn)
    {
        speedupBtn.gameObject.SetActive(isOn);
    }

    public void ShowPickupTutorial()
    {
        tutorial_pickup.SetActive(true);
    }

    public void ShowDropoffTutorial()
    {
        tutorial_dropoff.SetActive(true);
    }

    public void UpdateBallCountOnHud(int count)
    {
        GameManager.Instance.mAudioManager.PlaySoundFX("BallDropoff");
        if (ballText != null)
        {
            ballText.text = count.ToString();
        }
    }
}
