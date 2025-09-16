using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.UI;

public class Map : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Sprite[] mapSprites;

    [SerializeField] Image mapImage;
    [SerializeField] GameObject mapPointPrefab;
    [SerializeField] GameObject mapPointsContainer;
    [SerializeField] private PinManager pin;

    float timer = 0;
    float timeInterval = 0.03f;

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            RectTransform containerRect = mapPointsContainer.GetComponent<RectTransform>();
            Vector2 localClickPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(containerRect, eventData.position, eventData.pressEventCamera, out localClickPosition);

            GameObject newMapPointer = Instantiate(mapPointPrefab, mapPointsContainer.transform);
            Image mapPointImage = newMapPointer.GetComponent<Image>();
            if (mapPointImage != null)
            {
                mapPointImage.raycastTarget = false;
            }

            newMapPointer.transform.localPosition = localClickPosition + new Vector2(0, 400);
            
            newMapPointer.transform.DOLocalMoveY(localClickPosition.y, 1.5f).SetEase(Ease.OutBounce).OnUpdate(() => {
                timer += Time.deltaTime;
                if(timer >= timeInterval && newMapPointer.transform.localPosition.y > localClickPosition.y + 75)
                {
                    pin.CreateTrail(newMapPointer.transform.position, newMapPointer.transform.localScale, newMapPointer.GetComponent<Image>(), containerRect);
                    timer = 0;
                }
            }).OnComplete(() => {
                DOVirtual.DelayedCall(0.5f, pin.ClearAllTrails);
                if (mapPointImage != null) mapPointImage.raycastTarget = true;
            });
        }
    }

    public void ChangeMapImage(int index)
    {
        mapImage.sprite = mapSprites[index];
    }
}
