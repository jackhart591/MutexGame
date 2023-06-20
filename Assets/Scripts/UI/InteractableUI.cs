using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableUI : MonoBehaviour {
    public Vector3 WorldPos {
        get {
            var pos = Camera.main.ScreenToWorldPoint(transform.position);

            return new Vector3(pos.x, pos.y, 0);
        }
    }

    public VariableColor color { get; protected set; }
}
