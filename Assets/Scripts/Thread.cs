using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Thread : MonoBehaviour {

    [SerializeField] private InputActionReference mousePosRef;
    [SerializeField] private InputActionReference mouseClickRef;
    [SerializeField] private GameObject dataNodePrefab;
    [SerializeField] private GameObject lockPrefab;

    private Transform source;
    private Transform target;
    private float timeToCompletion;
    private float startOffset;
    private LineRenderer lr;
    private Vector2 mousePos;
    private bool _dataLocked;
    private float _currTick;
    private GameObject _currDataNode;

    public static bool Play = false;

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

    public void Initialize(Transform _source, float _timeToCompletion, float _startOffset) {
        source = _source;
        timeToCompletion = _timeToCompletion;
        startOffset = _startOffset;
    }

    private void Update() {
        /*mousePos = Camera.main.ScreenToWorldPoint(mousePosRef.action.ReadValue<Vector2>());

        lr.SetPosition(0, source.position);

        if (target != null) {
            lr.SetPosition(lr.positionCount-1, target.position);

            if (transform.childCount == 0 || !transform.GetChild(0).TryGetComponent(out EdgeCollider2D edge)) {
                GetComponent<EdgeCollider2D>().SetPoints(new List<Vector2>() { transform.InverseTransformPoint(source.position), transform.InverseTransformPoint(target.position) });
                GetComponent<EdgeCollider2D>().edgeRadius = 0.1f;
            }
            
            if (startOffset <= 0 && Play)
                _updateDataNodePosition();
            else if (Play)
                startOffset -= Time.deltaTime;
        } else {
            lr.SetPosition(lr.positionCount-1, mousePos);
        }*/

    }

    public void CreateLock(Vector3 pos) {
        pos = new Vector2(pos.x, transform.position.y);
        GameObject newLock = Instantiate(lockPrefab, pos, Quaternion.identity, transform);
    }

    private void _updateDataNodePosition() {
        if (_currTick >= timeToCompletion || _currTick == 0) {
            if (_currDataNode != null) {
                Destroy(_currDataNode);
                //target.GetComponent<Target>().BeginProcessing(_currDataNode);
            }

            _currTick = Time.deltaTime;
            
            _currDataNode = Instantiate(dataNodePrefab, source.position, Quaternion.identity, transform);
        } else {

            if (_dataLocked) return;

            Vector3 newPos = Vector3.Lerp(source.position, target.position, _currTick/timeToCompletion);
            _currDataNode.transform.position = newPos;
            _currTick += Time.deltaTime;
        }
    }

    private void OnRelease(InputAction.CallbackContext ctx) {
        if (target != null) { return; }

        Vector2 point = Camera.main.ScreenToWorldPoint(mousePosRef.action.ReadValue<Vector2>());
        Collider2D col = Physics2D.OverlapPoint(point);

        if (col != null) {
            if (col.transform.CompareTag("Interactable")) {
                target = col.transform;
            } else {
                Destroy(gameObject);
            }
        }
    }

    public void InLock() { _dataLocked = true; }

    public void LeftLock() { _dataLocked = false; }
}
