using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextBlink : MonoBehaviour {

	private Color color;

	// Use this for initialization
	void Start () {
		color = GetComponent<Text> ().material.color;
	}
	
	// Update is called once per frame
	void Update () {
		color.a = Mathf.Sin (Time.time * 5f);
		GetComponent<Text> ().material.color = color;
	}
}
