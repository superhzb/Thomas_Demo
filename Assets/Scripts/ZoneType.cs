using UnityEngine;

public class ZoneType : MonoBehaviour
{
    public Transition transition;


    //Condition to end SpecialMoveState
    private void OnTriggerExit(Collider other)
    {
        if (transition == Transition.InSpecialMoveZone)
        {
            Player player = other.GetComponent<Player>();
            if (player != null)
            {
                player.SetTransition(Transition.ReturnToNormal);
            }
        }
    }
}


