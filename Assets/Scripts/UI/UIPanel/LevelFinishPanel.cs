using TMPro;

public class LevelFinishPanel : BasePanel
{
    private TextMeshProUGUI ballText;

    private void Awake()
    {
        ballText = transform.Find("Img_Banner/Txt_BallText").GetComponent<TextMeshProUGUI>();
    }

    public override void OnEnter()
    {
        base.OnEnter();
        GameManager.Instance.mAudioManager.PlaySoundFX("LevelFinish");
    }

    public void OnRestartBtnClicked()
    {
        GameManager.Instance.mAudioManager.PlayButtonAudioClip();
        GameManager.Instance.EnterGameDemoScene();
    }

    public void OnHomeBtnClicked()
    {
        GameManager.Instance.mAudioManager.PlayButtonAudioClip();
        GameManager.Instance.EnterGameOptionScene();
    }

    public void UpdateBallText(int ballCount)
    {
        ballText.text = ballCount.ToString();
    }
}
