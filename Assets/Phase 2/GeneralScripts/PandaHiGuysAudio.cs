using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class PandaHiGuysAudio : MonoBehaviour {

	private AudioSource m_PandaRandomSoundSource;
	private FirstPersonController m_PandaFps;


	public AudioClip[]				PandaAudioFiles;

	private bool? m_HiGuysAudioPlayed = null;

	void Start()
	{
		m_PandaRandomSoundSource = GetComponent<AudioSource> ();

		if (SaveDataStatic.StorySequence != "Litter") {

			Collider[] cl = GetComponents<Collider> ();

			foreach (Collider c in cl)
				c.enabled = false;
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if(TownController.GetInstance().GetActiveISelObj() != null && TownController.GetInstance().GetActiveISelObj().Name == "Panda" && m_HiGuysAudioPlayed == null)
		{
			m_HiGuysAudioPlayed = false;
		}
	}

	void OnTriggerExit(Collider col)
	{
		if(TownController.GetInstance().GetActiveISelObj() != null && TownController.GetInstance().GetActiveISelObj().Name == "Panda" && m_HiGuysAudioPlayed == false)
		{
			m_HiGuysAudioPlayed = true;
			//GetComponent<Collider>().enabled = false;

			Collider[] cl = GetComponents<Collider> ();

			foreach (Collider c in cl)
				c.enabled = false;
		}
	}

	void Update()
	{
		if (TownController.GetInstance ().IsSubtitleStillDisplaying) {

			if(m_PandaRandomSoundSource.isPlaying)
			{
				m_PandaRandomSoundSource.Stop ();
			}
			return;
		}

		if (m_PandaFps) {
			if (m_PandaFps.enabled && m_PandaFps.IsMoving && m_HiGuysAudioPlayed == null) {
				PlayRandomAudioWhileWandering ();
			} else if (m_PandaFps.enabled && m_PandaFps.IsMoving && m_HiGuysAudioPlayed == true) {
				m_HiGuysAudioPlayed = null;
				m_IsRandomSoundPlayed = true;
				m_PandaRandomSoundSource.clip = PandaAudioFiles [0];
				m_PandaRandomSoundSource.Play ();
				StartCoroutine (WaitAfterRandomSoundIsPlayed (0));
			}
		} else {
			if (TownController.GetInstance ().Panda) {
				m_PandaFps = TownController.GetInstance ().Panda.GetComponent<FirstPersonController> ();
			}
		}
	}


	bool m_IsRandomSoundPlayed = false;
	void PlayRandomAudioWhileWandering()
	{
		if(m_IsRandomSoundPlayed) return;

		m_IsRandomSoundPlayed = true;

		int m_RanNo = Random.Range(0, 2000);
		int m_Audioselected = 0;
		if(m_RanNo < 400)
		{
			m_Audioselected = 1;
		}
		else if(m_RanNo < 800)
		{
			m_Audioselected = 2;
		}
		else if(m_RanNo < 1200)
		{
			m_Audioselected = 3;
		}
		else if(m_RanNo < 1600)
		{
			m_Audioselected = 4;
		}
		else
		{
			m_Audioselected = 5;
		}	
		m_PandaRandomSoundSource.clip = PandaAudioFiles [m_Audioselected];
		m_PandaRandomSoundSource.Play();
		StartCoroutine(WaitAfterRandomSoundIsPlayed(m_Audioselected));
	}

	IEnumerator WaitAfterRandomSoundIsPlayed(int _audioFileSelected)
	{
		yield return new WaitForSeconds (PandaAudioFiles [_audioFileSelected].length);

		yield return new WaitForSeconds (3);
		m_IsRandomSoundPlayed = false;
	}
}
