using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class SpeedCamera : MonoBehaviour {

	public GameObject SpeedDisplay;
	private bool m_HasDisplayed = false;
	public Animator	SpeedCamSplash;

	public AudioClip OhDearAudioClip;
	public AudioClip CameraFlashAudioClip;

	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerEnter(Collider other) {

		if (!m_HasDisplayed && other.gameObject.GetComponent<ISelectObject> () != null && other.gameObject.GetComponent<ISelectObject> ().Name == "Caterpillar") {
			m_HasDisplayed = true;
			SpeedCamSplash.enabled = true;

			TownController.GetInstance ().TownControllerAudio.clip = CameraFlashAudioClip;
			TownController.GetInstance ().TownControllerAudio.Play ();
			StartCoroutine(RemoveTheSpeedDisplay());
		}
	}

	IEnumerator RemoveTheSpeedDisplay()
	{
		yield return new WaitForSeconds (0.5f);
		SpeedCamSplash.gameObject.SetActive (false);
		SpeedDisplay.SetActive (true);
		TownController.GetInstance ().Caterpillar.GetComponent<FirstPersonController> ().enabled = false;
        yield return new WaitForSeconds(2.5f);
        //yield return new WaitForSeconds (5);
        //SpeedDisplay.SetActive (false);
        TownController.GetInstance ().CaterpillarPhotoImg.SetActive(true);

		yield return new WaitForSeconds (1);

		TownController.GetInstance ().TownControllerAudio.clip = OhDearAudioClip;
		TownController.GetInstance ().TownControllerAudio.Play ();

		yield return new WaitForSeconds(OhDearAudioClip.length);

		yield return  new WaitForSeconds (2);

		TownController.GetInstance ().CaterpillarPhotoImg.SetActive(false);
		TownController.GetInstance ().Caterpillar.GetComponent<FirstPersonController> ().enabled = true;
	}
}
