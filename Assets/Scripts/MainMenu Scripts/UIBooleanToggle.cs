using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof (UnityEngine.UI.Toggle))]
public class UIBooleanToggle : MonoBehaviour {

	bool isOn;

	void Start(){
		isOn = GetComponent<UnityEngine.UI.Toggle> ().isOn;
	}

	public void Toggle()
	{
		GetComponent<UnityEngine.UI.Toggle> ().isOn = !GetComponent<UnityEngine.UI.Toggle> ().isOn;
	}
}
