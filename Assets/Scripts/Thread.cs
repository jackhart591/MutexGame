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
    private VariableColor color;
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
    }

    public void Initialize(ThreadUI _source) {
        mousePos = Camera.main.ScreenToWorldPoint(mousePosRef.action.ReadValue<Vector2>());
        SetSource(_source);

        lr.SetPosition(0, source.GetComponent<ThreadUI>().WorldPos);
        lr.SetPosition(lr.positionCount-1, mousePos);
    }

    private void Update() {
        mousePos = Camera.main.ScreenToWorldPoint(mousePosRef.action.ReadValue<Vector2>());

        lr.SetPosition(0, source.GetComponent<ThreadUI>().WorldPos);

        if (target != null) {
            lr.SetPosition(lr.positionCount-1, target.GetComponent<VariableUI>().WorldPos);

            if (transform.childCount == 0 || !transform.GetChild(0).TryGetComponent(out EdgeCollider2D edge)) {
                GetComponent<EdgeCollider2D>().SetPoints(new List<Vector2>() { transform.InverseTransformPoint(lr.GetPosition(0)), transform.InverseTransformPoint(lr.GetPosition(lr.positionCount-1)) });
                GetComponent<EdgeCollider2D>().edgeRadius = 0.1f;
            }
            
            if (startOffset <= 0 && Play)
                _updateDataNodePosition();
            else if (Play)
                startOffset -= Time.deltaTime;
        } else {
            lr.SetPosition(lr.positionCount-1, mousePos);
        }

    }

    public void CreateLock() {
        GameObject newLock = Instantiate(lockPrefab, Vector3.zero, Quaternion.identity, transform);
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

    public void SetSource(ThreadUI thread) {
        source = thread.transform;
        SetColor(thread.color);
    }

    public bool SetTarget(Variable @var) {
        if (color == var.color) {
            target = var.transform;
            return true;
        }

        return false;
    }

    public void SetColor(Color col) {
        var newCol = VariableColorManager.GetEnumFromColor(col);

        if (newCol != null) { color = (VariableColor)newCol; }
    }

    public void SetColor(VariableColor col) {
        color = col;
    }

    public void InLock() { _dataLocked = true; }

    public void LeftLock() { _dataLocked = false; }
}
