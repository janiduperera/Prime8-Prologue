using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutAsRefOnTownCnt : MonoBehaviour {

	public string MyName;
	// Use this for initialization
	void Start () {

		if (MyName == "trash bin closed") {
			name = MyName;
			TownController.GetInstance ().LitterBin = gameObject;
		}
		else if (MyName == "Prime8SuperRemover") {
			name = MyName;
			TownController.GetInstance ().Prime8SuperRemover = gameObject;
		}
		else if (MyName == "Cloth") {
			name = MyName;
			TownController.GetInstance ().Cloth = gameObject;
		}
	}

}
