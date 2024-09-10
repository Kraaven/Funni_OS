using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TerminalApplication : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public RectTransform rectTransform;
    public Canvas canvas;
    public CanvasGroup canvasGroup;
    private Vector3 offset;

    private void Awake()
    {
        if (rectTransform == null)
            rectTransform = GetComponent<RectTransform>();
        if (canvas == null)
            canvas = GetComponentInParent<Canvas>();
        if (canvasGroup == null)
            canvasGroup = GetComponent<CanvasGroup>();

        if (canvas == null)
        {
            Debug.LogError("Canvas is required for drag operations.");
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out Vector3 worldPoint);
        offset = rectTransform.position - worldPoint;
        TerminalButton.GlobalCanvasGRP.blocksRaycasts = false;
        
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.pressEventCamera, out Vector3 worldPoint))
        {
            rectTransform.position = worldPoint + offset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        TerminalButton.GlobalCanvasGRP.blocksRaycasts = true;
    }
}