using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public enum VariableColor { Blue, Red, Green, Yellow };

public class ProgramWindow : MonoBehaviour {


    public string Title;
    public VariableColor[] threads;

    [Header("References -- Don't change!!")]
    [SerializeField] private Player player;
    [SerializeField] private TMP_Text titleText;
    [SerializeField] private Transform threadParent;
    [SerializeField] private GameObject threadPrefab;

    private float _threadHeight;
    
    private void Awake() {
        titleText.text = Title;
        _threadHeight = threadPrefab.GetComponent<RectTransform>().sizeDelta.y;
    }

    private void Start() {
        Vector2 sizeDelta = GetComponent<RectTransform>().sizeDelta;
        GetComponent<RectTransform>().sizeDelta = new Vector2 (
            sizeDelta.x,
            sizeDelta.y + (_threadHeight * threads.Length) + 5
        ); // Extra 5 is for bottom padding
    
        for (int i = 0; i < threads.Length; i++) {
            GameObject newThread = Instantiate(threadPrefab, Vector3.zero, Quaternion.identity, threadParent);
            ThreadUI script = newThread.GetComponent<ThreadUI>();

            script.Title = $"Thread #{i+1}";
            script.color = (Color)GetColorFromEnum(threads[i]);
            script.player = player;
        }
    }

    public static Color? GetColorFromEnum(VariableColor col) {
        switch (col) {
            case VariableColor.Blue:
                return Color.blue;
            case VariableColor.Red:
                return Color.red;
            case VariableColor.Green:
                return Color.green;
            case VariableColor.Yellow:
                return Color.yellow;
            default:
                Debug.LogError("Color not implemented!!");
                return null;
        }
    }
}
