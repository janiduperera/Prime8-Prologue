using UnityEngine;
using System.Collections;

public class Starter : MonoBehaviour {
	public GameObject gameController;
	// Use this for initialization
	void Start () {
		Debug.Log ("Instantiating");
		GameObject.Instantiate (gameController);
	}

}
