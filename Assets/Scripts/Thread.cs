using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Thread : MonoBehaviour {

    [SerializeField] private InputActionReference mousePosRef;
    [SerializeField] private InputActionReference mouseClickRef;
    [SerializeField] private GameObject dataNodePrefab;

    private Transform source;
    private Transform target;
    private float timeToCompletion;
    private LineRenderer lr;
    private Vector2 mousePos;
    private float _currTick;
    private GameObject _currDataNode;

    private void Awake() {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 2;
        target = null;
        _currTick = 0;

        mouseClickRef.action.canceled += OnRelease;
    }

    private void Destroy() {
        mouseClickRef.action.canceled -= OnRelease;
    }

    public void Initialize(Transform _source, float _timeToCompletion) {
        source = _source;
        timeToCompletion = _timeToCompletion;
    }

    private void Update() {
        mousePos = Camera.main.ScreenToWorldPoint(mousePosRef.action.ReadValue<Vector2>());

        lr.SetPosition(0, source.position);

        if (target != null) {
            lr.SetPosition(lr.positionCount-1, target.position);
            _updateDataNodePosition();
        } else {
            lr.SetPosition(lr.positionCount-1, mousePos);
        }

    }

    private void _updateDataNodePosition() {
        if (_currTick == timeToCompletion || _currTick == 0) {
            if (_currDataNode != null) Destroy(_currDataNode);
            // Put code here for handling target functions
            _currTick = Time.deltaTime;
            
            _currDataNode = Instantiate(dataNodePrefab, source.position, Quaternion.identity, transform);
        } else {
            Vector3 newPos = Vector3.Lerp(source.position, target.position, _currTick/timeToCompletion);
            _currDataNode.transform.position = newPos;
            _currTick += Time.deltaTime;
        }
    }

    private void OnRelease(InputAction.CallbackContext ctx) {
        RaycastHit hit;

        Vector3 newMousePos = new Vector3(mousePos.x, mousePos.y, -10); // Start on -10 so it doesn't start inside the object
        if (Physics.Raycast(newMousePos, Vector3.forward, out hit)) {
            if (hit.transform.CompareTag("Interactable")) {
                target = hit.transform;
            } else {
                Destroy(gameObject);
            }
        }
    }
}
