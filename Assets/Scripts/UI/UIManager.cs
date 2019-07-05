using DG.Tweening;
using LitJson;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class UIManager
{

    private Dictionary<UIPanelType, string> uiPathDict; //Dictionary to store paths.
    private Dictionary<UIPanelType, BasePanel> PanelDict; //To all panel instance.
    private Stack<BasePanel> panelstack; //Stack to store all the active panels in display

    private Transform canvas;
    private GameObject maskPrefab;
    private Image maskImage;
    private Tween maskTween;

    public void Init()
    {
        //Init Attributes
        if (uiPathDict == null) { uiPathDict = new Dictionary<UIPanelType, string>(); }
        if (PanelDict == null) { PanelDict = new Dictionary<UIPanelType, BasePanel>(); }
        if (panelstack == null) { panelstack = new Stack<BasePanel>(); }
        canvas = GameObject.FindObjectOfType<Canvas>().transform;
        maskPrefab = Resources.Load<GameObject>("Prefabs/UI/Img_Mask");
        InitMask();

        //Load Json File using LitJson
        UIPanelPathList pathsList = LoadFromJson();

        //Save Json Loaded text to Dictionary
        foreach (var t in pathsList.panelPathList)
        {
            UIPanelType type = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), t.name);
            uiPathDict.Add(type, t.path);
        }

        //uiPathDict.Add(UIPanelType.StartLoadPanel, "Prefabs/UIPanel/StartLoadPanel");
    }

    private UIPanelPathList LoadFromJson()
    {
        UIPanelPathList pathList = new UIPanelPathList();
        string path = Application.dataPath + "/StreamingAssets/Json/UIPanelTypePath.json";
        if (File.Exists(path))
        {
            StreamReader sr = new StreamReader(path);
            string jsonStr = sr.ReadToEnd();
            sr.Close();
            pathList = JsonMapper.ToObject<UIPanelPathList>(jsonStr);
        }
        if (pathList == null)
        {
            Debug.LogWarning("Failed to load Json File, Path: " + path);
        }
        return pathList;
    }

    private void InitMask()
    {
        maskImage = GameObject.Instantiate(maskPrefab, canvas).GetComponent<Image>();
    }

    public void ShowMask(bool isPlayForward)
    {
        if (maskTween == null)
        {
            maskTween = DOTween.To(
                () => maskImage.color,
                toColor => maskImage.color = toColor,
                new Color(0, 0, 0, 1),
                1f);
        }
        else
        {
            if (isPlayForward)
                maskTween.PlayForward();
            else
                maskTween.PlayBackwards();
        }
    }

    public BasePanel GetPanel(UIPanelType panelType)
    {
        BasePanel panel = PanelDict.TryGet(panelType);

        if (panel == null)
        {
            string path = uiPathDict.TryGet(panelType);
            BasePanel panelPrefab = Resources.Load<BasePanel>(path);
            panel= GameObject.Instantiate(panelPrefab, canvas, false); //To do
            panel.mUIManager = this;
            PanelDict.Add(panelType, panel);
        }

        return panel;
    }

    public void PushPanel(UIPanelType panelType)
    {
        BasePanel panel = GetPanel(panelType);
        panel.OnEnter();
        if (panelstack.Count != 0)
        {
            panelstack.Peek().OnPause();
        }
        panelstack.Push(panel);
    }
    
    public void PopPanel()
    {
        if (panelstack.Count == 0) return;
        BasePanel panel = panelstack.Pop();
        if (panelstack.Count > 0)
        {
            panelstack.Peek().OnResume();
        }
    }

    public void ClearPanelStack()
    {
        while (panelstack.Count > 0)
        {
            BasePanel panel = panelstack.Peek();
            panel.OnExit();
        }
        panelstack.Clear();
    }

    //Prepare for the GameOptionScene
    public void EnterGameOptionScene()
    {
        ClearPanelStack();
        PushPanel(UIPanelType.GameOptionPanel);
    }

    //Prepare for the GameDemoScene
    public void EnterGameDemoScene()
    {
        ClearPanelStack();
    }
}
