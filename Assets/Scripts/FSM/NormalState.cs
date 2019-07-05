using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalState : FSMState
{
    private Player player;

    public NormalState(Player player)
    {
        stateID = StateID.Normal;
        this.player = player;
        this.player = player;
    }

    public override void DoBeforeEntering()
    {
        player.NormalStateInit();
    }

    //Executed in Player's FixedUpdate()
    public override void Act()
    {
        player.NormalStateOnUpdate();
    }

}
