using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class VerificationPanel : BasePanel
{
    private TextMeshProUGUI[] codeTextArray;
    private string codeGenerated;
    private string codeInput;
    private TextMeshProUGUI txt_InputField;
    private Animator animInputFieldText;

    private void Awake()
    {
        codeTextArray = new TextMeshProUGUI[4];
        codeTextArray[0] = transform.Find("CodeGroup/Txt_01").GetComponent<TextMeshProUGUI>();
        codeTextArray[1] = transform.Find("CodeGroup/Txt_02").GetComponent<TextMeshProUGUI>();
        codeTextArray[2] = transform.Find("CodeGroup/Txt_03").GetComponent<TextMeshProUGUI>();
        codeTextArray[3] = transform.Find("CodeGroup/Txt_04").GetComponent<TextMeshProUGUI>();

        txt_InputField = transform.Find("Img_InputField/Txt_InputField").GetComponent<TextMeshProUGUI>();
        animInputFieldText = txt_InputField.GetComponent<Animator>();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        GenerateVerificationCode();
    }

    public void GenerateVerificationCode()
    {
        codeGenerated = "";
        codeInput = "";
        txt_InputField.text = codeInput;

        for (int i = 0; i < codeTextArray.Length; i++)
        {
            int randomNum = Random.Range(0, 10);
            codeGenerated += randomNum.ToString();
            codeTextArray[i].text = randomNum.ToWord();
        }
    }

    public void OnNumberClicked(string number)
    {
        GameManager.Instance.mAudioManager.PlayButtonAudioClip();
        if (codeInput.Length < 4)
        {
            codeInput += number;
            txt_InputField.text = codeInput;

            if (codeInput.Length >= 4)
            {
                CompareResult();
            }
        }
    }

    private void CompareResult()
    {
        if (codeInput == codeGenerated)
        {
            animInputFieldText.Play("GreenTextApprove", 0, 0);
        }
        else
        {
            animInputFieldText.Play("RedTextWarning", 0, 0);
        }
    }

    public void EnterSettingPanel()
    {
        GameManager.Instance.mAudioManager.PlayPageAudioClip();
        OnExit();
        mUIManager.PushPanel(UIPanelType.SettingPanel);
    }

    public void OnBackBtnClicked()
    {
        GameManager.Instance.mAudioManager.PlayButtonAudioClip();
        OnExit();
    }
}
