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
    }

    private void OnDestroy() {
        mouseClickRef.action.canceled -= OnMouseUp;
    }

    private void OnMouseUp(InputAction.CallbackContext ctx) {
        Vector2 point = mousePosRef.action.ReadValue<Vector2>();

        var uiClicked = _getUIClicked(point);

        VariableUI varScript = null;
        if (uiClicked != null) {
            foreach (GameObject elem in uiClicked) {
                if (currSelectedThread != null) {
                    varScript = elem.GetComponent<VariableUI>();
                }
            }
        }

        // if varScript doesn't exist, it will delete the line renderer
        currSelectedThread.LinkThreadToVar(varScript);
    }

    public static void OnPlay() { Thread.Play = true; }
    public static void OnPause() { Thread.Play = false; }

    private List<GameObject> _getUIClicked(Vector3 screenpoint) {
        var _pointerEventData = new PointerEventData(eventSystem);
        _pointerEventData.position = screenpoint;
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
