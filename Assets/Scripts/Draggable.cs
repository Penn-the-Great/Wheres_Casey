using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Vector2 offset;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = rectTransform.anchoredPosition - eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition = eventData.position + offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {

    }

    private void ClmapToBounds()
    {
        RectTransform canvasRect = rectTransform.parent as RectTransform;

        if (canvasRect == null) return;

        Vector3[] canvasCorners = new Vector3[4];
        canvasRect.GetWorldCorners(canvasCorners);

        Vector3[] itemCorners = new Vector3[4];
        rectTransform.GetWorldCorners(itemCorners);

        if (itemCorners[0].x < canvasCorners[0].x)
        {
            rectTransform.position += Vector3.right * (canvasCorners[0].x - itemCorners[0].x);
        }

        if (itemCorners[2].x > canvasCorners[2].x)
        {
            rectTransform.position += Vector3.left * (itemCorners[2].x - canvasCorners[2].x);
        }

        if (itemCorners[0].y < canvasCorners[0].y)
        {
            rectTransform.position += Vector3.up * (canvasCorners[0].y - itemCorners[0].y);
        }

        if (itemCorners[2].y > canvasCorners[2].y)
        {
            rectTransform.position += Vector3.down * (itemCorners[2].y - canvasCorners[2].y);
        }
    }

}
