using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ThreadUI : InteractableUI {

    public string Title {
        get {
            return TitleText.text;
        }

        set {
            TitleText.text = value;
        }
    }

    [HideInInspector] public Player player; // Set by the ProgramWindow script

    [Header("References -- Don't Change!!")]
    [SerializeField] private TMP_Text TitleText;
    [SerializeField] private Image colorObj;
    [SerializeField] private GameObject threadPrefab;

    private Thread currentlyHeldThread;

    private void Awake() {
        GetComponent<ThreadClickDetector>().beginDragCallback.AddListener(OnBeginDrag);
        //colorObj.GetComponentInParent<ThreadClickDetector>().dragCallback.AddListener(OnDrag);
        GetComponent<ThreadClickDetector>().endDragCallback.AddListener(OnEndDrag);

        currentlyHeldThread = null;
    }

    public void LinkThreadToVar(VariableUI @var) {

        if (currentlyHeldThread == null) { return; }

        if (var == null) {
            Destroy(currentlyHeldThread.gameObject);
        } else {
            Vector3 newPos = Camera.main.ScreenToWorldPoint(var.transform.position);
            newPos = new Vector3(newPos.x, newPos.y, 0);

            Variable varScript = var.GetComponent<Variable>();
            if(!currentlyHeldThread.GetComponent<Thread>().SetTarget(varScript)) {
                Destroy(var.gameObject);
            }
        }
    }

    public void SetColor(VariableColor col) {
        color = col;
        colorObj.color = (Color)VariableColorManager.GetColorFromEnum(col);
    }

    public void SetColor(Color col) {
        var newCol = VariableColorManager.GetEnumFromColor(col);

        if (newCol != null) { 
            color = (VariableColor)newCol; 
            colorObj.color = col;
        }
    }

    private void OnBeginDrag(Vector2 mousePos) {
        GameObject newThread = Instantiate(threadPrefab);
        currentlyHeldThread = newThread.GetComponentInChildren<Thread>();
        player.currSelectedThread = this;
        currentlyHeldThread.Initialize(this);
    }

    private void OnEndDrag(Vector2 mousePos) {
        currentlyHeldThread = null;
    }
}
