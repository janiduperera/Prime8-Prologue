using UnityEngine;
using System.Collections;

public class GlowEffectTwo : MonoBehaviour {

	Renderer rend;
	private Vector4 col;
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
		col = rend.material.GetVector ("_TintColor");
	}
	
	// Update is called once per frame
	void Update () {

		col.w = Mathf.Sin (Time.time * 4.0F) + 1.0F;
		rend.material.SetVector ("_TintColor",col);
	}
}
