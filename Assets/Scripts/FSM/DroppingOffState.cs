using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppingOffState : FSMState
{
    private Player player;
    private float dropOffTime;
    private float dropoffParkingPos;

    public DroppingOffState(Player player,float dropOffTime, float dropoffParkingPos)
    {
        stateID = StateID.DroppingOff;
        this.player = player;
        this.dropOffTime = dropOffTime;
        this.dropoffParkingPos = dropoffParkingPos;
    }

    public override void DoBeforeEntering()
    {
        GameController.Instance.SetActiveSuperSpeedupBtn(false);
        GameController.Instance.SetActiveSpeedupBtn(false);
        GameController.Instance.DropOffCameraOnLive();

        player.SetCurrentSpeed(0);
        player.BeginDropOff();
        player.StartCoroutine(DropoffStateCoolDown(dropOffTime, () => { player.SetTransition(Transition.ReturnToNormal); }));
    }

    private IEnumerator DropoffStateCoolDown(float dropOffTime, Action OnComplete)
    {
        yield return new WaitForSeconds(dropOffTime);
        OnComplete();
    }

    //Executed in Player's FixedUpdate()
    public override void Act()
    {
        player.StopAtPlatform(dropoffParkingPos); 
    }
}
