using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Variable))]
public class VariableUI : InteractableUI {
    
    public void Start() {
        transform.GetChild(0).GetComponent<Image>().color = (Color)VariableColorManager.GetColorFromEnum(color);
    }

    public void LinkThreadToVar(ThreadUI thread) {
        if (!ThreadCanLink(thread)) { return; }

        //thread.LinkThreadToVar(GetComponent<Variable>());

        // Do stuff in gameplay layer
    }

    public bool ThreadCanLink(ThreadUI thread) {
        if (thread.color == color) {
            return true;
        } else { return false; }
    }
}
