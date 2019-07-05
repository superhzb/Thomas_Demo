using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITrainChoosing : MonoBehaviour
{
    public List<TrainTween> trainList;
    private TrainTween lastTrain;
    private TrainTween nextTrain;
    private int nextIndex;

    private void Awake()
    {
        MyScrollSnap.OnTrainOptionChanged += OnTrainChanged;
        nextTrain = trainList[0];
    }

    private void Start()
    {
        nextTrain.OnEnter();
    }

    private void OnTrainChanged(int index)
    {
        lastTrain = nextTrain;
        nextTrain = trainList[index];
        
        lastTrain.OnExit(nextTrain.OnEnter);
    }

    

}
