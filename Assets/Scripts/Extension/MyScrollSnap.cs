using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MyScrollSnap : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{

    public int itemLength;
    public int spacing;
    public int viewportHeight;

    public static event Action<int> OnTrainOptionChanged = delegate { };

    private ScrollRect scrollRect;
    private RectTransform contentRectTransform;

    private int itemCount;
    private int oneStepLength;
    private int currentIndex;
    private float onMouseBeginY;
    private float onMouseEndY;
    private float targetNormalizedPos;
    private float oneStepNormallizedLength;
    

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        contentRectTransform = scrollRect.content;

        itemCount = contentRectTransform.childCount;
        oneStepLength = itemLength + spacing;
        currentIndex = 0;
        targetNormalizedPos = 0.90f; //config this starting pos in order to make the first card in the center position.
        //SetContentLength();  //Wierd bug: Auto config content height will break it in the game(but not in test scene)
        scrollRect.verticalNormalizedPosition = targetNormalizedPos;
        oneStepNormallizedLength= oneStepLength / (contentRectTransform.sizeDelta.y - viewportHeight); 
    }

    private void SetContentLength()
    {
        float sizeDeltaY = (itemLength + spacing) * itemCount;
        contentRectTransform.sizeDelta = new Vector2(contentRectTransform.sizeDelta.x, sizeDeltaY);
        oneStepNormallizedLength = oneStepLength / (sizeDeltaY - viewportHeight);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        onMouseBeginY = Input.mousePosition.y;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        onMouseEndY = Input.mousePosition.y;

        float offset = onMouseEndY - onMouseBeginY;

        if (Mathf.Abs(offset) > oneStepLength)
        {
            targetNormalizedPos -= oneStepNormallizedLength * (int)(offset / oneStepLength);
            currentIndex += (int)(offset / oneStepLength);
            currentIndex = CorrectCurrentIndex(currentIndex);
        }
        else if (Mathf.Abs(offset) > 50)
        {
            targetNormalizedPos -= oneStepNormallizedLength * (offset / Mathf.Abs(offset));
            currentIndex += (int)(offset / Mathf.Abs(offset));
            currentIndex = CorrectCurrentIndex(currentIndex);
        }
        MoveToTarget();
    }

    public void OnUpClick()
    {
        targetNormalizedPos -= oneStepNormallizedLength;
        currentIndex++;
        currentIndex = CorrectCurrentIndex(currentIndex);
        MoveToTarget();
    }

    public void OnDownClick()
    {
        targetNormalizedPos += oneStepNormallizedLength;
        currentIndex--;
        currentIndex = CorrectCurrentIndex(currentIndex);
        MoveToTarget();
    }

    private void MoveToTarget()
    {
        OnTrainOptionChanged(currentIndex);
        DOTween.To(
                () => scrollRect.verticalNormalizedPosition, //The value we need to modify
                lerp => scrollRect.verticalNormalizedPosition = lerp, //lerp
                targetNormalizedPos, //Target value
                0.2f); //Duration
    }

    private int CorrectCurrentIndex(int index)
    {
        index %= 5;
        return index < 0 ? (index + 5) : index;
    }
}
