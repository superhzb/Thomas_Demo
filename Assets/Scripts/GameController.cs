using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UI;

/// <summary>
/// GameController is a Singleton which controls the game logic.
/// </summary>
public class GameController : MonoBehaviour
{
    public static GameController Instance { get; private set; }

    public Player player;
    public DropOffPlatform dropOffPlatform;
    public CinemachineVirtualCamera vcam_SpecialMove;
    public CinemachineVirtualCamera vcam_PickUp;
    public CinemachineVirtualCamera vcam_DropOff;
    public CinemachineVirtualCamera vcam_Timeline; //vcam for opening timeline.
    private PlayableDirector levelFinishDirector; //Timeline for level finished.
    private HUDPanel hud;

    public static event Action OnSpeedup = delegate { };
    public static event Action OnSuperSpeedup = delegate { };

    public StateID currentState;
    private float levelTime = 100f;
    private float levelRemainTimer = 100f;
    private int ballsDelivered = 0;

    public bool isGameover = false;
    public bool isPause = true;
    private bool isPickupTutorial = false;
    private bool isDropoffTutorial = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        InitLevel();

    }

    private void InitLevel()
    {
        levelRemainTimer = levelTime; //Reset level Timer
        isGameover = false;
        hud = GameManager.Instance.mUIManager.GetPanel(UIPanelType.HUD).GetComponent<HUDPanel>();
        levelFinishDirector = player.transform.Find("DollyCamera_LevelFinish").GetComponent<PlayableDirector>();
        isPause = true;
        currentState = StateID.Normal;
        ballsDelivered = 0;
        if (hud != null)
        {
            hud.UpdateBallCountOnHud(0);
        }
        GameManager.Instance.mAudioManager.PlayBGMusic("BGMusic02");
    }



    public void StartGame()
    {
        vcam_Timeline.Priority = 8;
        hud.gameObject.SetActive(true);
    }

    public void ResumeGame()
    {
        isPause = false;
    }

    public void OnSpeedupBtnClicked()
    {
        OnSpeedup();
    }

    public void OnSuperBtnClicked()
    {
        OnSuperSpeedup();
    }

    public void SetActiveSuperSpeedupBtn(bool isOn)
    {
        hud.SetActiveSuperSpeedupBtn(isOn);
    }

    public void SetActiveSpeedupBtn(bool isOn)
    {
        hud.SetActiveSpeedupBtn(isOn);
    }

    private void FixedUpdate()
    {
        LevelTimeCountDown();
    }

    public void LevelTimeCountDown()
    {
        if (isGameover || isPause)
            return;

        levelRemainTimer -= Time.fixedDeltaTime;
        if (levelRemainTimer <= 0)
        {
            LevelFinish();
            levelRemainTimer = levelTime;
            isGameover = true;
        }
        hud.UpdateSliderValue(levelRemainTimer, levelRemainTimer / levelTime);
    }

    public void LevelFinish()
    {
        levelFinishDirector.gameObject.SetActive(true);
        GameManager.Instance.mUIManager.PushPanel(UIPanelType.LevelFinishPanel);
        LevelFinishPanel levelFinishPanel = GameManager.Instance.mUIManager.GetPanel(UIPanelType.LevelFinishPanel) as LevelFinishPanel;
        levelFinishPanel.UpdateBallText(ballsDelivered);
    }

    //For tutorial
    public void StateChange(StateID state) 
    {
        currentState = state;

        //Pick up tutorial
        if (isPickupTutorial == false)
        {
            if (state == StateID.PickingUp)
            {
                hud.ShowPickupTutorial();
                isPickupTutorial = true;
            }
        }

        //Drop off tutorial
        if (isDropoffTutorial == false)
        {
            if (state == StateID.DroppingOff)
            {
                hud.ShowDropoffTutorial();
                isDropoffTutorial = true;
            }
        }
    }

    public void AddBalls(int count)
    {
        ballsDelivered += count;
        hud.UpdateBallCountOnHud(ballsDelivered);
    }

    /// <summary>
    /// Four methods to control which camera should be live.
    /// </summary>
    public void SpecialMoveCameraOnLive()
    {
        vcam_SpecialMove.Priority = 11;
        vcam_PickUp.Priority = 9;
        vcam_DropOff.Priority = 9;
    }

    public void NormalCameraOnLive()
    {
        vcam_SpecialMove.Priority = 9;
        vcam_PickUp.Priority = 9;
        vcam_DropOff.Priority = 9;
    }

    public void PickUpCameraOnLive()
    {
        vcam_SpecialMove.Priority = 9;
        vcam_PickUp.Priority = 11;
        vcam_DropOff.Priority = 9;
    }

    public void DropOffCameraOnLive()
    {
        vcam_SpecialMove.Priority = 9;
        vcam_PickUp.Priority = 9;
        vcam_DropOff.Priority = 11;
    }
}
