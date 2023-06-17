using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ThreadUI : MonoBehaviour {

    public string Title {
        get {
            return TitleText.text;
        }

        set {
            TitleText.text = value;
        }
    }
    public Color color {
        get {
            return colorObj.color;
        }

        set {
            colorObj.color = value;
        }
    }

    [HideInInspector] public Player player; // Set by the ProgramWindow script

    [Header("References -- Don't Change!!")]
    [SerializeField] private TMP_Text TitleText;
    [SerializeField] private Image colorObj;
    [SerializeField] private GameObject threadPrefab;

    private LineRenderer currentlyHeldThread;

    private void Awake() {
        colorObj.GetComponentInParent<ThreadClickDetector>().beginDragCallback.AddListener(OnBeginDrag);
        colorObj.GetComponentInParent<ThreadClickDetector>().dragCallback.AddListener(OnDrag);
        colorObj.GetComponentInParent<ThreadClickDetector>().endDragCallback.AddListener(OnEndDrag);

        currentlyHeldThread = null;
    }

    public void LinkThreadToVar(VariableUI @var) {

        if (currentlyHeldThread == null) { return; }

        if (var == null || ProgramWindow.GetColorFromEnum(var.color) != color) {
            Destroy(currentlyHeldThread.gameObject);
        } else {
            Vector3 newPos = Camera.main.ScreenToWorldPoint(var.transform.position);
            newPos = new Vector3(newPos.x, newPos.y, 0);

            currentlyHeldThread.SetPosition(currentlyHeldThread.positionCount-1, newPos);
        }
    }

    private void OnBeginDrag(Vector2 mousePos) {
        GameObject newThread = Instantiate(threadPrefab);
        currentlyHeldThread = newThread.GetComponent<LineRenderer>();
        player.currSelectedThread = this;

        Vector3 threadPos = Camera.main.ScreenToWorldPoint(transform.GetChild(0).position);
        threadPos = new Vector3(threadPos.x, threadPos.y, 0);

        currentlyHeldThread.SetPosition(0, threadPos);
    }

    private void OnDrag(Vector2 mousePos) {
        Vector3 newMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        newMousePos = new Vector3(newMousePos.x, newMousePos.y, 0);
        currentlyHeldThread.SetPosition(1, newMousePos);
    }

    private void OnEndDrag(Vector2 mousePos) {
        Vector3 newMousePos = Camera.main.ScreenToWorldPoint(mousePos);
        newMousePos = new Vector3(newMousePos.x, newMousePos.y, 0);

        currentlyHeldThread = null;
    }
}
