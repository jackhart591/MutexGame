using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {

    [HideInInspector] public Transform currSelectedInteractable { get; private set; }

    [SerializeField] private InputActionReference mousePosRef;
    [SerializeField] private Transform selectionCube;

    private Vector3 defaultSelectionCubePos;

    private void Start() {
        defaultSelectionCubePos = selectionCube.transform.position;
    }

    private void OnMouseClick() {
        Vector2 point = Camera.main.ScreenToWorldPoint(mousePosRef.action.ReadValue<Vector2>());
        Collider2D col = Physics2D.OverlapPoint(point);

        if (col != null) {
            if (col.transform.TryGetComponent(out Source _source)) {
                _source.CreateThread();
                OnInteract(col.transform);
            } else if (col.transform.TryGetComponent(out Thread _thread)) {
                _thread.CreateLock(point);
            } else if (col.transform.CompareTag("Interactable")) {
                OnInteract(col.transform);
            }
        } /* else { OnInteract(); } */
    }

    private void OnInteract(Transform obj=null) {

        if (obj == null) {
            currSelectedInteractable = null;
            selectionCube.SetParent(null);
            selectionCube.position = defaultSelectionCubePos;
            return;
        }

        if (obj.CompareTag("Interactable")) {
            currSelectedInteractable = obj;

            selectionCube.SetParent(currSelectedInteractable);
            selectionCube.localPosition = Vector3.zero;
        }
    }

    public static void OnPlay() { Thread.Play = true; }
    public static void OnPause() { Thread.Play = false; }
}
