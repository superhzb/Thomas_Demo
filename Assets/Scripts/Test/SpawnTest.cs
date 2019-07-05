using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTest : MonoBehaviour
{
    public int count = 0;
    public GameObject spherePrefab;
    public float spawndelay = 0.2f;
    private BoxCollider box;

    private void Awake()
    {
        box = GetComponent<BoxCollider>();
        InvokeRepeating("SpawnASphere", 1f, 0.2f);
    }

    public void SpawnASphere()
    {
        Vector3 pos = box.PickARandomPoint();
        //Instantiate(spherePrefab, pos, transform.rotation);
        Transform sphere = SpherePool.Instance.Get();
        sphere.position = pos;
        sphere.GetComponent<Rigidbody>().velocity = Vector3.zero;
        sphere.gameObject.SetActive(true);
        count++;
        Debug.Log("Spawn");
    }

}
