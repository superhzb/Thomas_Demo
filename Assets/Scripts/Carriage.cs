using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carriage : MonoBehaviour
{
    private List<Ball> ballList = new List<Ball>();
    public bool canDropOff = false;
    private BoxCollider dropoffBoxCol;
    public float ForceMultiply;
    public Rigidbody attchPoint;

    private void Start()
    {
        dropoffBoxCol = GameController.Instance.dropOffPlatform.GetComponent<BoxCollider>();
    }

    public void DropABall()
    {
        if (canDropOff)
        {
            if (ballList.Count > 1)
            {
                int index = Random.Range(0, ballList.Count);
                Ball ball = ballList[index];
                ThrowABallTowardsPlatform(ball);
            }
        }
    }

    private void ThrowABallTowardsPlatform(Ball ball)
    {
        Vector3 targetPos = dropoffBoxCol.bounds.center;

        float magnitude = Vector3.Distance(targetPos, ball.transform.position);
        Vector3 horizontalVector = (targetPos - ball.transform.position).normalized;

        float xForce = horizontalVector.x * magnitude;
        float zForce = horizontalVector.z * magnitude;
        float yForce = 8f;

        Vector3 force = new Vector3(xForce, yForce, zForce);

        ball.GetComponent<Rigidbody>().AddForce(force * ForceMultiply);
    }

    private void OnTriggerEnter(Collider other)
    {
        Ball ball = other.GetComponent<Ball>();
        if (ball != null)
        {
            ball.transform.SetParent(attchPoint.transform, true);
            ballList.Add(ball);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Ball ball = other.GetComponent<Ball>();
        if (ball != null)
        {
            ball.transform.SetParent(BallPool.Instance.transform);
            ballList.Remove(ball);
        }
    }

    public void HoldBalls()
    {
        foreach (Ball ball in ballList)
        {
            ball.GetComponent<Rigidbody>().useGravity = false;
            ball.GetComponent<Rigidbody>().isKinematic = true;
        }
    }

    public void ReleaseBalls()
    {
        foreach (Ball ball in ballList)
        {
            ball.GetComponent<Rigidbody>().useGravity = true;
            ball.GetComponent<Rigidbody>().isKinematic = false;
        }
    }


}
