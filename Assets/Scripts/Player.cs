using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {

    [SerializeField] private InputActionReference mousePosRef;

    private Vector3 mousePos;

    private void Update() {
        mousePos = Camera.main.ScreenToWorldPoint(mousePosRef.action.ReadValue<Vector2>());
    }

    private void OnMouseClick() {
        RaycastHit hit;

        Vector3 newMousePos = new Vector3(mousePos.x, mousePos.y, -10); // Start on -10 so it doesn't start inside the object
        if (Physics.Raycast(newMousePos, Vector3.forward, out hit)) {
            if (hit.transform.TryGetComponent(out Source source)) {
                source.CreateThread();
            }
        }
    }
}
