using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class ThreadClickDetector : 
    MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerMoveHandler, 
    IDragHandler, IBeginDragHandler, IEndDragHandler {

    public UnityEvent<Vector2> beginDragCallback;
    public UnityEvent<Vector2> dragCallback;
    public UnityEvent<Vector2> endDragCallback;
    
    public void OnBeginDrag(PointerEventData eventData) { beginDragCallback.Invoke(eventData.position); }

    public void OnDrag(PointerEventData eventData) { dragCallback.Invoke(eventData.position); }

    public void OnEndDrag(PointerEventData eventData) { endDragCallback.Invoke(eventData.position); }

    // DONT REMOVE -- These are implemented because they're required by the dragging events
    public void OnPointerDown(PointerEventData eventData) {}
    public void OnPointerMove(PointerEventData eventData) {}
    public void OnPointerUp(PointerEventData eventData) {}
}
