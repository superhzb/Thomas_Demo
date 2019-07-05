using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    private float nextSpawnTime;

    private Collider boxCollider;

    [SerializeField]
    private float spawnDelay = 0.2f;
    [SerializeField]
    private int maxCount = 50;

    public int count = 0;

    public List<Ball> ballList = new List<Ball>();

    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        InvokeRepeating("SpawnABall", 1f, spawnDelay);
    }

    private void SpawnABall()
    {
        Ball ball = BallPool.Instance.Get();
        ball.transform.position = boxCollider.PickARandomPoint();
        ball.gameObject.SetActive(true);
        ballList.Add(ball);
        count++;
        if (count >= maxCount)
        {
            CancelSpawnBalls();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Ball exitball = other.GetComponent<Ball>();
        if (exitball != null)
        {
            ballList.Remove(exitball);
            count--;
        }
        if(count < maxCount)
        {
            SpawnABall();
        }
    }

    public void StartSpawningBalls()
    {
        InvokeRepeating("SpawnABall", 1f, spawnDelay);
    }

    public void CancelSpawnBalls()
    {
        CancelInvoke();
    }

}
