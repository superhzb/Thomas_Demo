using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //SingleTon
    private static GameManager _instance; 
    public static GameManager Instance { get { return _instance; } }

    //Managers
    public UIManager mUIManager;
    public AudioFactory mAudioFactory;
    public AudioManager mAudioManager;
    

    private void Awake()
    {
        _instance = this;
        mUIManager = new UIManager();
        mAudioFactory = new AudioFactory();
        mAudioManager = new AudioManager();
    }

    private void Start()
    {
        mUIManager.Init();
        mUIManager.PushPanel(UIPanelType.StartLoadPanel);
    }

    public void EnterGameOptionScene()
    {
        SceneManager.LoadScene(1);
        mUIManager.EnterGameOptionScene();
    }

    public void EnterGameDemoScene()
    {
        SceneManager.LoadScene(2);
        mUIManager.EnterGameDemoScene();
    }
}
