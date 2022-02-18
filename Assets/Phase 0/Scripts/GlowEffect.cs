using UnityEngine;
using System.Collections;

public class GlowEffect : MonoBehaviour {

	Renderer rend;
	private Vector4 col;
	// Use this for initialization
	void Start () {
		rend = GetComponent<Renderer> ();
		col = rend.material.GetVector ("_TintColor");
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0; i < rend.materials.Length; i++) {
			col.w = Mathf.Sin (Time.time * 4.0F) + 1.0F;
			rend.materials[i].SetVector ("_TintColor", col);
		}
		//rend.material.SetFloat ("_InvFade", (Mathf.Sin(Time.time * 2f) + 1f) * 3f);
	}
}
