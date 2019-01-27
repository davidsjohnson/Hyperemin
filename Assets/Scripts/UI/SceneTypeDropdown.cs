using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneTypeDropdown : MonoBehaviour {

    private Dropdown dropdown;

	private void Start ()
    {
        dropdown = GetComponent<Dropdown>();
        dropdown.onValueChanged.AddListener(delegate { OnValueChangedHandler(dropdown); });

        PlayerCtrlOLD.Control.SceneType = (SceneType)dropdown.value;
    }

    private void OnValueChangedHandler(Dropdown changed)
    {
        PlayerCtrlOLD.Control.SceneType = (SceneType)changed.value;
    }
}