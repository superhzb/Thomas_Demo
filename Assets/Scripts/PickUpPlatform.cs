using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpPlatform : MonoBehaviour
{
    private Animator animPlatform;
    private BallSpawner ballSpawner;
    private BoxCollider carriageBoxCol;
    public float ForceMultiply;

    private void Start()
    {
        animPlatform = GetComponent<Animator>();
        ballSpawner = GetComponentInChildren<BallSpawner>();
        carriageBoxCol = GameController.Instance.player.carriage.GetComponent<BoxCollider>();
    }

    private void OnMouseDown()
    {
        if (GameController.Instance.currentState == StateID.PickingUp)
        {
            GameManager.Instance.mAudioManager.PlaySoundFX("BallPickup");

            animPlatform.SetTrigger("Hit");
            if (ballSpawner.ballList.Count > 1)
            {
                int index = Random.Range(0, ballSpawner.ballList.Count);
                Ball ball = ballSpawner.ballList[index];
                ThrowABallTowardsTrain(ball);
            }
        }
    }

    private void ThrowABallTowardsTrain(Ball ball)
    {
        float magnitude = Vector3.Distance(carriageBoxCol.transform.position, ball.transform.position);
        Vector3 horizontalVector = (carriageBoxCol.transform.position - ball.transform.position).normalized;

        float xForce = horizontalVector.x * magnitude;
        float zForce = horizontalVector.z * magnitude;
        float yForce = 8f;

        Vector3 force = new Vector3(xForce, yForce, zForce);

        ball.GetComponent<Rigidbody>().AddForce(force * ForceMultiply);
    }
}
