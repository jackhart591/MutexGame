using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {

    [HideInInspector] public Lock currSelectedLock { get; private set; }

    [SerializeField] private InputActionReference mousePosRef;

    private void OnMouseClick() {
        Vector2 point = Camera.main.ScreenToWorldPoint(mousePosRef.action.ReadValue<Vector2>());
        Collider2D col = Physics2D.OverlapPoint(point);

        if (col != null) {
            if (col.transform.TryGetComponent(out Source source)) {
                source.CreateThread();
            } else if (col.transform.TryGetComponent(out Thread thread)) {
                thread.CreateLock(point);
            }
        }
    }
}
