using UnityEngine;
using System.Collections;

public class RhinoLitterGunAnimator : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	public void OnLitterGunAnimationCompletion()
	{
		transform.parent.gameObject.GetComponent<Rhino>().LitterGunAnimationComplete();
	}
}
