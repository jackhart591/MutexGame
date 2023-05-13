using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Source : MonoBehaviour {

    [SerializeField] private GameObject threadPrefab;
    [SerializeField] private float timeToCompletion;

    public void CreateThread() {
        GameObject thread = Instantiate(threadPrefab, transform.position, Quaternion.identity, transform);
        thread.GetComponent<Thread>().Initialize(transform, timeToCompletion);
    }
}
