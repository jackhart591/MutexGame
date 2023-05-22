using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ThreadClickDetector : MonoBehaviour, IBeginDragHandler, IEndDragHandler {

    public UnityEvent<Vector2> beginDragCallback;
    public UnityEvent<PointerEventData> dragCallback;
    public UnityEvent<PointerEventData> endDragCallback;
    
    public void OnBeginDrag(PointerEventData eventData) { beginDragCallback.Invoke(eventData.position); }

    public void OnDrag(PointerEventData eventData) { dragCallback.Invoke(eventData); }

    public void OnEndDrag(PointerEventData eventData) { endDragCallback.Invoke(eventData); }
}
