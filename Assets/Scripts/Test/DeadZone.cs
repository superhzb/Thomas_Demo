using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        //Destroy(other.gameObject);
        SpherePool.Instance.ReturnToPool(other.transform);
        Debug.Log("Dead");
    }
}
