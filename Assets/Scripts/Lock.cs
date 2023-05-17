using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lock : MonoBehaviour {

    public bool locked { get; private set; }

    [HideInInspector] public LockVariable currentLockVar;

    private Thread lockedObj;

    private void Update() {
        if (lockedObj != null && !locked) {
            lockedObj.LeftLock();
            lockedObj = null;
        }
    }

    public void OnToggleLock(bool _locked) {
        locked = _locked;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.transform.CompareTag("Data") && locked) {
            lockedObj = transform.GetComponentInParent<Thread>();
            lockedObj.InLock();
        }
    }
}