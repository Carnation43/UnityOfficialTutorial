using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using DG.Tweening;

public class TooltipInstance : MonoBehaviour
{
    public static TooltipInstance instance;

    [SerializeField] TMP_Text text;

    Tween animationTween;

    int animationId;
    private void Awake()
    {
        instance = this;
        transform.localScale = Vector3.zero;
    }

    public void ShowTooltip(Transform sourceTransform, string landmarkName)
    {
        transform.position = sourceTransform.position;
        text.text = landmarkName;
        if(animationTween != null)
        {
            animationTween.Kill();
        }
        animationTween = transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutExpo);
    }

    public void HideTooltip()
    {
        if (animationTween != null)
        {
            animationTween.Kill();
        }
        animationTween = transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.OutExpo);
    }
}
