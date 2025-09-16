using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class TooltipOpener : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] string landmarkName;

    public void OnPointerEnter(PointerEventData eventData)
    {
        TooltipInstance.instance.ShowTooltip(transform, landmarkName);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        TooltipInstance.instance.HideTooltip();
    }
}
