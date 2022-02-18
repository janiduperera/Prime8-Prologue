using UnityEngine;
using System.Collections;

public class HidingArea : MonoBehaviour {

	public bool IsHiding = false;
	Renderer rend;
	private Vector4 col;

	private AudioSource	m_HidingAreaAudioSource;
	public AudioClip YesAudioClip;
	private bool m_DidYesAudioClipPlayed = false;

	void Awake()
	{
		m_HidingAreaAudioSource = GetComponent<AudioSource>();
	}

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

	void OnDisable()
	{
		GetComponent<Renderer> ().material.SetVector ("_TintColor", new Vector4(1, 1, 1, 0));
	}


	void OnTriggerEnter(Collider other)
	{
		if(other.gameObject.name == "FPSController_Snake") // Snake
		{
			IsHiding = true;

			if(!m_DidYesAudioClipPlayed)
			{
				m_DidYesAudioClipPlayed = true;
				m_HidingAreaAudioSource.PlayOneShot(YesAudioClip);
			}
		}
	}

	void OnTriggerExit(Collider other)
	{
		if(other.gameObject.name == "FPSController_Snake") // Snake
		{
			IsHiding = false;

			m_DidYesAudioClipPlayed = false;
		}
	}
}
