using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LockVariable : MonoBehaviour {
    public UnityEvent OnToggleLock;

    public void ToggleLock() {
        OnToggleLock.Invoke();
    }
}