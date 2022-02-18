using UnityEngine;
using System.Collections;

public class SimpleSkyRotator : MonoBehaviour {

    public float speed = 20f;

	// Use this for initialization
	void Start () {
  
	}

	// Update is called once per frame
	void FixedUpdate () {
        transform.Rotate(new Vector3(0f,Time.deltaTime * speed,0f));
	}
}
