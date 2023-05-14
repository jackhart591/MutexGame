using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class LockWindow : MonoBehaviour {

    [HideInInspector] public LockVariable currSelectedLockVar { get; private set; }
    
    [SerializeField] private Transform VariableArea;
    [SerializeField] private GameObject VarPrefab;
    [SerializeField] private Player player;

    public void AddVar() {
        GameObject newVar = Instantiate(VarPrefab, Vector3.zero, Quaternion.identity, VariableArea);

        newVar.GetComponent<Toggle>().onValueChanged.AddListener(delegate{ SelectVar(newVar.GetComponent<Toggle>()); });
        newVar.GetComponentInChildren<Text>().text = $"Lock Variable {VariableArea.childCount}";
    }

    public void SelectVar(Toggle toggledGameObject) {

        bool isOn = toggledGameObject.isOn;

        foreach(Transform child in VariableArea) {
            child.GetComponent<Toggle>().SetIsOnWithoutNotify(false);
        }

        toggledGameObject.SetIsOnWithoutNotify(isOn);
        currSelectedLockVar = (isOn) ? toggledGameObject.GetComponent<LockVariable>() : null;
    }

    public void ApplyToLock() {
        Lock currLock = player.currSelectedLock;
        currLock.currentLockVar.OnToggleLock.RemoveListener(currLock.OnToggleLock);

        if (currSelectedLockVar != null)
            currSelectedLockVar.OnToggleLock.AddListener(currLock.OnToggleLock);
    }
}
