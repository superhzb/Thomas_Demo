using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDetect : MonoBehaviour
{
    public int enterCount;
    public int exitCount;

    private void OnTriggerEnter(Collider other)
    {
        enterCount++;
        Debug.Log("Enter");
    }

    private void OnTriggerExit(Collider other)
    {
        exitCount++;
        Debug.Log("Exit");
    }
}
