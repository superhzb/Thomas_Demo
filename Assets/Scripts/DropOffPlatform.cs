using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropOffPlatform : MonoBehaviour
{
    private Animator animDropoffPlatform;

    private void Awake()
    {
        animDropoffPlatform = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Ball ball = collision.gameObject.GetComponent<Ball>();
        if (ball != null)
        {
            animDropoffPlatform.SetTrigger("Hit");
            ball.transform.SetParent(BallPool.Instance.transform);
            GameController.Instance.AddBalls(1);
            ball.ReturnToPoolInDelay(10f);
        }


    }

}
