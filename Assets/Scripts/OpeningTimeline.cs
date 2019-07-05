using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class OpeningTimeline : MonoBehaviour
{
    private PlayableDirector director;

    private void OnEnable()
    {
        director = GetComponent<PlayableDirector>();
        director.stopped += OnPlayableDirectorStopped;
    }

    private void OnPlayableDirectorStopped(PlayableDirector director)
    {
        GameManager.Instance.mUIManager.PushPanel(UIPanelType.HUD);
        GameController.Instance.StartGame();
    }

    private void OnDisable()
    {
        director.stopped -= OnPlayableDirectorStopped;
    }


}
