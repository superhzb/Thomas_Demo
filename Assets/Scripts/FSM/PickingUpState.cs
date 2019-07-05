using System;
using System.Collections;
using UnityEngine;

public class PickingUpState : FSMState
{
    private Player player;
    private float pickUpTime;
    private float pickupParkingPos;

    public PickingUpState(Player player, float pickUpTime, float pickupParkingPos)
    {
        stateID = StateID.PickingUp;
        this.player = player;
        this.pickUpTime = pickUpTime;
        this.pickupParkingPos = pickupParkingPos;
    }

    public override void DoBeforeEntering()
    {
        GameController.Instance.SetActiveSuperSpeedupBtn(false);
        GameController.Instance.SetActiveSpeedupBtn(false);
        GameController.Instance.PickUpCameraOnLive();
        player.SetCurrentSpeed(0);
        player.StartCoroutine(PickUpStateCoolDown(pickUpTime,() => { player.SetTransition(Transition.ReturnToNormal); }));
    }

    public override void DoBeforeLeaving()
    {
        player.HoldBallsInCarriage();
    }

    private IEnumerator PickUpStateCoolDown(float pickUpTime, Action OnComplete)
    {
        yield return new WaitForSeconds(pickUpTime);
        OnComplete();
    }

    //Executed in Player's FixedUpdate()
    public override void Act()
    {
        player.StopAtPlatform(pickupParkingPos); 
    }

}
