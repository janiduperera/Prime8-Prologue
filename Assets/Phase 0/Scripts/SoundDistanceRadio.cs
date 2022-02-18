using UnityEngine;
using System.Collections;

public class SoundDistanceRadio : MonoBehaviour {

	public Transform panda;
	private AudioSource waterSound;
	private float volume = 0.4F;
	// Use this for initialization
	void Start () {
		waterSound = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		
		float distance = Vector3.Distance (transform.position, panda.position) / 20.0F;
		float clampDistance = Mathf.Clamp01 (distance);
		
		//Debug.Log (clampDistance);
		
		waterSound.volume = volume - clampDistance;

	}
}
