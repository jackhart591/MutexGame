using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour {

    public bool locked { get; private set; }

    [HideInInspector] public LockVariable currentLockVar;

    public void OnToggleLock() {
        locked = !locked;
    }
}