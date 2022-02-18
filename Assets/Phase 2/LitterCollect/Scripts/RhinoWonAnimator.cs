using UnityEngine;
using System.Collections;

public class RhinoWonAnimator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public void OnRhinoWinAnimationCompletion()
	{
		transform.parent.gameObject.GetComponent<Rhino>().OnRhinoWinAnimationComplete();
	}
}
