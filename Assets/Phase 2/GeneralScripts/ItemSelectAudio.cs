using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelectAudio : MonoBehaviour {

	#region Singleton
	private static ItemSelectAudio m_Instane;
	private ItemSelectAudio(){}
	public static ItemSelectAudio GetInstance()
	{
		return m_Instane;
	}

	void Awake()
	{
		m_Instane = this;
	}
	#endregion Singleton

	public AudioClip ItemPickAudio;
	public AudioClip LitterPickAudio;

	private AudioSource m_MyAudioSource;

	void Start()
	{
		m_MyAudioSource = GetComponent<AudioSource> ();
	}

	public void PlayAudio(bool _playItemSelect)
	{
		if (m_MyAudioSource.isPlaying)
			return;
		
		if (_playItemSelect) {
			m_MyAudioSource.clip = ItemPickAudio;
		} else {
			m_MyAudioSource.clip = LitterPickAudio;
		}

		m_MyAudioSource.Play ();
	}
}
