using UnityEngine;
using System.Collections;

public class AudioDistance : MonoBehaviour {

	public Transform panda;
	private AudioSource waterSound;
	private float volume = 0.05F;
	// Use this for initialization
	void Start () {
		waterSound = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		float distance = Vector3.Distance (transform.position, panda.position) / 170.0F;
		float clampDistance = Mathf.Clamp01 (distance);
		
	    
		
		waterSound.volume = volume - clampDistance;
	
	}
}
