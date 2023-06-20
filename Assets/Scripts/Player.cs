using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Player : MonoBehaviour {
    
    [HideInInspector] public ThreadUI currSelectedThread;

    [SerializeField] private InputActionReference mousePosRef;
    [SerializeField] private InputActionReference mouseClickRef;

    [Tooltip("Main canvas object that is present in the scene")]
    [SerializeField] private GraphicRaycaster sceneCanvas;
    [SerializeField] private EventSystem eventSystem;

    private void Start() {
        currSelectedThread = null;

        mouseClickRef.action.canceled += OnMouseUp;
        mouseClickRef.action.performed += OnMouseDown;
    }

    private void OnDestroy() {
        mouseClickRef.action.canceled -= OnMouseUp;
        mouseClickRef.action.performed -= OnMouseDown;
    }

    private void OnMouseDown(InputAction.CallbackContext ctx) {
        var uiClicked = _getUIClicked();
        if (uiClicked != null) { return; } // UI Handles its own click stuff

        RaycastHit2D hit;
        Ray ray = Camera.main.ScreenPointToRay(mousePosRef.action.ReadValue<Vector2>());
        hit = Physics2D.Raycast(Camera.main.transform.position, mousePosRef.action.ReadValue<Vector2>());

        if (hit.collider == null) { return; }

        switch (hit.transform.tag) {
            case "Thread":
                hit.transform.GetComponent<Thread>().CreateLock();
                break;
        }
    }

    private void OnMouseUp(InputAction.CallbackContext ctx) {
        var uiClicked = _getUIClicked();

        VariableUI varScript = null;
        if (uiClicked != null) {
            foreach (GameObject elem in uiClicked) {
                if (currSelectedThread != null) {
                    varScript = elem.GetComponent<VariableUI>();
                }
            }
        }

        // if varScript doesn't exist, it will delete the line renderer
        if (currSelectedThread != null) currSelectedThread.LinkThreadToVar(varScript);
    }

    public static void OnPlay() { Thread.Play = true; }
    public static void OnPause() { Thread.Play = false; }

    private List<GameObject> _getUIClicked() {
        var _pointerEventData = new PointerEventData(eventSystem);
        _pointerEventData.position = mousePosRef.action.ReadValue<Vector2>();
        var results = new List<RaycastResult>();

        sceneCanvas.Raycast(_pointerEventData, results);

        // Won't every gameobject in results be a UI object?
        List<GameObject> UIElementsClicked = new List<GameObject>();
        foreach(RaycastResult result in results) {
            if (result.gameObject.layer == 5) { // Is on UI Layer
                UIElementsClicked.Add(result.gameObject);
            }
        }

        return (UIElementsClicked.Count > 0) ? UIElementsClicked : null;
    }
}
