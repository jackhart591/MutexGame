using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VariableUI : MonoBehaviour {
    
    [SerializeField] private VariableColor color;

    public void Start() {
        transform.GetChild(0).GetComponent<Image>().color = (Color)ProgramWindow.GetColorFromEnum(color);
    }

    public void LinkThreadToVar(ThreadUI thread) {
        if (!ThreadCanLink(thread)) { return; }

        // Do stuff in gameplay layer
    }

    public bool ThreadCanLink(ThreadUI thread) {
        if (thread.color == (Color)ProgramWindow.GetColorFromEnum(color)) {
            return true;
        } else { return false; }
    }
}
