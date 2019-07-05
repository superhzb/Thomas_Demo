using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class TrainTween : MonoBehaviour
{
    private Tween tweenEnterMove;
    private Tween tweenEnterFade;
    private Tween tweenExitMove;
    private Tween tweenExitFade;
    private Image image;
    private float animTime = 0.2f;

    public Vector3 startingPos;
    public Vector3 centerPos;
    public Vector3 endPos;

    private void Awake()
    {
        image = GetComponent<Image>();

        tweenEnterMove = transform.DOLocalMove(centerPos, animTime);
        tweenEnterMove.SetAutoKill(false);
        tweenEnterMove.Pause();

        tweenEnterFade = image.DOFade(1f, animTime);
        tweenEnterFade.SetAutoKill(false);
        tweenEnterFade.Pause();

        tweenExitMove = transform.DOLocalMove(endPos, animTime);
        tweenExitMove.SetAutoKill(false);
        tweenExitMove.Pause();

        tweenExitFade= image.DOFade(0f, animTime);
        tweenExitFade.SetAutoKill(false);
        tweenExitFade.Pause();
    }

    public void OnEnter()
    {
        transform.localPosition = startingPos;
        tweenEnterMove.Restart();
        tweenEnterFade.Restart();
    }

    public void OnExit(TweenCallback OnExitComplete)
    {
        tweenExitMove.Restart();
        tweenExitFade.Restart();
        tweenExitMove.OnComplete(OnExitComplete);
    }

}
