using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseDownTestBtn : MonoBehaviour
{
    public CinemachineVirtualCamera cam1, cam2;


    public void ChangeCamera(bool isOn)
    {
        if (isOn)
        {
            cam1.Priority = 20;
            cam2.Priority = 10;
        }
        else
        {
            cam1.Priority = 10;
            cam2.Priority = 20;
        }
    }
}
