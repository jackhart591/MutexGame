using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    [Header("References -- Don't Change!!")]
    [SerializeField] private TMP_Text TitleText;
    [SerializeField] private Image colorObj;

    private void Awake() {
        colorObj.GetComponentInParent<ThreadClickDetector>().beginDragCallback.AddListener(OnBeginDrag);
    }

    private void OnBeginDrag(Vector2 mousePos) {
        // put line renderers here
    }
}
