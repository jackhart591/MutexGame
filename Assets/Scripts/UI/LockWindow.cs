using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

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

    public void RemoveVar() {

        if (currSelectedLockVar == null) {
            currSelectedLockVar = VariableArea.GetChild(VariableArea.childCount-1).GetComponent<LockVariable>();
        }

        currSelectedLockVar.GetComponent<Toggle>().onValueChanged.RemoveAllListeners();

        if (currSelectedLockVar.linkedTarget != null)
            currSelectedLockVar.linkedTarget.RemoveListener();

        Destroy(currSelectedLockVar.gameObject);
        currSelectedLockVar = null;
    }

    public void SelectVar(Toggle toggledGameObject) {

        bool isOn = toggledGameObject.isOn;

        foreach(Transform child in VariableArea) {
            child.GetComponent<Toggle>().SetIsOnWithoutNotify(false);
        }

        toggledGameObject.SetIsOnWithoutNotify(isOn);
        currSelectedLockVar = (isOn) ? toggledGameObject.GetComponent<LockVariable>() : null;
    }

    public void LinkVar() {
        if (player.currSelectedInteractable.TryGetComponent(out Target target)) {
            target.SetListener(currSelectedLockVar.GetComponent<LockVariable>());
        } else { Debug.LogWarning("No Target Selected!!"); }
    }

    public void ApplyToLock() {
        if (player.currSelectedInteractable == null) {
            Debug.LogWarning("Nothing selected!!");
            return;
        }

        if (player.currSelectedInteractable.TryGetComponent(out Lock currLock)) {
            if (currSelectedLockVar != null) {
                try {
                    currLock.currentLockVar.toggleLock -= currLock.OnToggleLock;
                } catch (Exception _) {}

                currSelectedLockVar.toggleLock += currLock.OnToggleLock;
            }
        } else { Debug.LogWarning("No Lock Selected!!"); }

    }
}
