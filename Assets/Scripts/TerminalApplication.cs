using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TerminalApplication : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler, IPointerClickHandler
{
    [Header("Settings")]
    public RectTransform rectTransform;
    public Canvas canvas;
    public CanvasGroup canvasGroup;
    private Vector3 offset;
    [Header("InteractionReferences")]
    public TMP_InputField TerminalInput;
    public TerminalInterface Interface;

    public static TerminalApplication ActiveTerminal;

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
        
        TerminalApplication.ActiveTerminal = this;
        TerminalApplication.ActiveTerminal.TerminalInput.ActivateInputField();
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            TerminalApplication.ActiveTerminal = this;
            TerminalApplication.ActiveTerminal.TerminalInput.ActivateInputField();
        }
    }
}