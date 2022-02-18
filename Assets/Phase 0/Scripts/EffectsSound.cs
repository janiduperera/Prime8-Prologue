using UnityEngine;
using System.Collections;

[RequireComponent(typeof(AudioSource))]
public class EffectsSound : MonoBehaviour
{
	void PlaySourceClip(AudioClip audioClip)
	{
		GetComponent<AudioSource> ().clip = audioClip;
		GetComponent<AudioSource> ().Play ();
	}

}
