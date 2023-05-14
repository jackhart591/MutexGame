using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour {
    
    public bool isProcessing { 
        get => _isProcessing; 
        private set {
            _isProcessing = value;
            GetComponent<SpriteRenderer>().color = (_isProcessing) ? Color.red : Color.blue;
        }
    }

    [SerializeField] private float processingTime;

    private float _currProcessTime;
    private bool _isProcessing;

    private void Awake() {
        _isProcessing = false;
    }

    private void Start() {
        GetComponent<SpriteRenderer>().color = Color.blue;
    }

    private void Update() {
        if (isProcessing) {
            if (_currProcessTime <= processingTime) {
                _currProcessTime += Time.deltaTime;
            } else {
                EndProcessing();               
            }
        }
    }

    public void BeginProcessing(GameObject dataNode) {
        if (isProcessing) { 
            DataCollision();
            return;
        }

        isProcessing = true;
        _currProcessTime = 0f;
    }

    private void EndProcessing() {
        isProcessing = false;
    }

    private void DataCollision() {
        Debug.Log("Game Over!");
    }
}
