using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PinManager : MonoBehaviour
{
    private List<GameObject> activeTrails = new List<GameObject>();

    // Instantiate trails
    public void CreateTrail(Vector3 position, Vector3 scale, Image mainImage, RectTransform parentRect)
    {
        GameObject trail = new GameObject("Trail");
        trail.transform.SetParent(parentRect);
        trail.transform.position = position;
        trail.transform.localScale = scale * 0.3f;

        Image trailImage = trail.AddComponent<Image>();
        trailImage.sprite = mainImage.sprite;
        trailImage.color = new Color(0,0,0,0.4f);
        trailImage.raycastTarget = false;   // disabled the click on the trail
        trail.transform.DOScale(scale * 0.6f, 0.25f).SetEase(Ease.InQuad);
        trailImage.DOFade(0, 0.25f).SetEase(Ease.InQuad).OnComplete(() =>
        {
            activeTrails.Remove(trail);
            Destroy(trail);
        });

        activeTrails.Add(trail);
    }


    // Clear trails
    public void ClearAllTrails()
    {
        foreach (var trail in activeTrails)
        {
            if (trail != null)
                Destroy(trail);
        }
        activeTrails.Clear();
    }
}