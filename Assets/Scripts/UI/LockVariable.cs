using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LockVariable : MonoBehaviour {
    public Action<bool> toggleLock;
    public Target linkedTarget;

    public void ToggleLock(bool locked) {
        toggleLock(locked);
    }
}