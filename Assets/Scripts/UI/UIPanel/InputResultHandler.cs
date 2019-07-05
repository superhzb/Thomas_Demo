using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputResultHandler : MonoBehaviour
{
    private VerificationPanel verificationPanel;

    private void Awake()
    {
        verificationPanel = GetComponentInParent<VerificationPanel>();
    }


    public void InputResultEvent(int isPassed)
    {
        if (isPassed == 0)
        {
            verificationPanel.GenerateVerificationCode();
        }
        else
        {
            verificationPanel.EnterSettingPanel();
        }
    }
}
