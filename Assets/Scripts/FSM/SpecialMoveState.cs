public class SpecialMoveState : FSMState
{
    private Player player;

    public SpecialMoveState(Player player)
    {
        stateID = StateID.SpecialMove;
        this.player = player;
    }

    public override void DoBeforeEntering()
    {
        GameManager.Instance.mAudioManager.PlaySoundFX("SpecialMove");

        player.SuperSpeedup();
        GameController.Instance.SetActiveSuperSpeedupBtn(false);
        GameController.Instance.SetActiveSpeedupBtn(false);
        GameController.Instance.SpecialMoveCameraOnLive();
    }
   

}
