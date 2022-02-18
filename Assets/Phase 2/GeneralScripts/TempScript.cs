using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TempScript : MonoBehaviour {

	// Use this for initialization
	void Start () {


		if (SceneManager.GetActiveScene ().name == "FullTown 1")
			SceneManager.LoadSceneAsync ("FullTown");
		else {

			Debug.Log ("Start : " + System.DateTime.Now.ToString ());
			SceneManager.LoadSceneAsync ("Town");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
