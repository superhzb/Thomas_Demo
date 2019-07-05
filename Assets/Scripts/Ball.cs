using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private bool toPool = false;

    private void OnEnable()
    {
        toPool = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.SetParent(BallPool.Instance.transform);
    }

    private void Update()
    {
        if (toPool)
            return;

        //If the ball drop off on the ground, it should go back to pool.
        if (transform.position.y < 0.1f)
        {
            toPool = true;
            Invoke("ReturnToPool", 2f);
        }
    }

    public void ReturnToPoolInDelay(float delay)
    {
        Invoke("ReturnToPool", delay);
    }

    public void ReturnToPool()
    {
        BallPool.Instance.ReturnToPool(this);
    }

}
